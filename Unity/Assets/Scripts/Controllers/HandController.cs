using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HandController : MonoBehaviour
{
	public SkeletonWrapper sw;
	
	public HandControl Hand_Left;
	public HandControl Hand_Right;
	
	public float scale = 1.0f;
	
	[Serializable]
	public class ButtonCube
	{
		public GameObject Cube = null;
		public string Command = "";
	}
	
	public List<ButtonCube> Buttons = new List<ButtonCube>();
	public Color InactiveColor = Color.white;
	public Color ActiveColor = Color.green;
	
	public float MoveSensitivity = 1F;
	public float RotationSensitivityX = 15F;
	public float RotationSensitivityY = 15F;
	
	private Vector3 _handLeftPos;
	private Vector3 _handRightPos;
	
	private GUIText _leftHandText;
	private GUIText _rightHandText;
	
	void OnEnable()
	{
		GameObject HUD = GameObject.FindGameObjectWithTag("HUD");
		
		if (HUD != null)
		{
			Transform trans = HUD.transform.FindChild("HandLeft");
			_leftHandText = trans.GetComponent<GUIText>();
			
			trans = HUD.transform.FindChild("HandRight");
			_rightHandText = trans.GetComponent<GUIText>();
		}
	}
	
	void Update()
	{
		if (sw.pollSkeleton())
		{
			Vector3 headPos = new Vector3( 	sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.Head].x,
			                              sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.Head].y,
			                              sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.Head].z);
			headPos *= scale;
			
			_handLeftPos = new Vector3( sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.HandLeft].x,
			                               sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.HandLeft].y,
			                               sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.HandLeft].z);
			_handLeftPos *= scale;
			
			_handRightPos = new Vector3( sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.HandRight].x,
			                                sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.HandRight].y,
			                                sw.bonePos[0, (int)Kinect.NuiSkeletonPositionIndex.HandRight].z);
			_handRightPos *= scale;
			
			_handLeftPos = _handLeftPos - headPos;
			_handRightPos = _handRightPos - headPos;
			
			Hand_Left.transform.localPosition = _handLeftPos;
			Hand_Right.transform.localPosition = _handRightPos;
			
			UpdateButtons();
			UpdateHUD();
		}
	}
	
	void UpdateButtons()
	{
		foreach (var button in Buttons)
		{
			UpdateButtonColor(button.Cube);
		}
		
		Transform transToMove = transform.parent;
		
		float rotInput = CheckCommand("LeftLeft") ? -1.0f : 0.0f;
		rotInput += CheckCommand("RightLeft") ? 1.0f : 0.0f;
		
		Quaternion rotX = Quaternion.AngleAxis(rotInput * RotationSensitivityX * Time.deltaTime, Vector3.up);
		
		rotInput = CheckCommand("TopLeft") ? 1.0f : 0.0f;
		rotInput += CheckCommand("BottomLeft") ? -1.0f : 0.0f;
		Quaternion rotY = Quaternion.AngleAxis(rotInput * RotationSensitivityY * Time.deltaTime, Vector3.left);
		
		transToMove.localRotation = transToMove.localRotation * rotX * rotY;
		
		float transInput = CheckCommand("ForwardRight") ? 1.0f : 0.0f;
		transInput += CheckCommand("BackRight") ? -1.0f : 0.0f;
		
		transToMove.Translate(
			new Vector3(0.0f, 0.0f, transInput * MoveSensitivity * Time.deltaTime), Space.Self);
	}
	
	void UpdateButtonColor(GameObject button)
	{
		if (CheckButton(button))
		{
			button.GetComponent<Wireframe>().lineColor = ActiveColor;
		}
		else
		{
			button.GetComponent<Wireframe>().lineColor = InactiveColor;
		}
	}
	
	bool CheckCommand(string command)
	{
		foreach (var button in Buttons)
		{
			if (button.Command == command)
			{
				return CheckButton(button.Cube);
			}
		}
		
		return false;
	}
	
	bool CheckButton(GameObject button)
	{
		return (Hand_Left.IsCollidingWith(button.collider) && IsLeftTracked()) || (Hand_Right.IsCollidingWith(button.collider) && IsRightTracked());
	}
	
	bool IsLeftTracked()
	{
		return sw.boneState[0, (int)Kinect.NuiSkeletonPositionIndex.HandLeft] == Kinect.NuiSkeletonPositionTrackingState.Tracked;
	}
	
	bool IsRightTracked()
	{
		return sw.boneState[0, (int)Kinect.NuiSkeletonPositionIndex.HandLeft] == Kinect.NuiSkeletonPositionTrackingState.Tracked;
	}
	
	void UpdateHUD()
	{
		if (_leftHandText != null)
		{
			int boneID = (int)Kinect.NuiSkeletonPositionIndex.HandLeft;
			_leftHandText.text = string.Format("Left Hand: {0}\nPos - {1:F}, {2:F}, {3:F}\nRot - {4:F}, {5:F}, {6:F}",
			                                   IsLeftTracked() ? "Tracked" : "Not Tracked",
			                                   _handLeftPos.x,
			                                   _handLeftPos.y,
			                                   _handLeftPos.z,
			                                   sw.boneLocalOrientation[0, boneID].eulerAngles.x,
			                                   sw.boneLocalOrientation[0, boneID].eulerAngles.y,
			                                   sw.boneLocalOrientation[0, boneID].eulerAngles.z);
		}
		if (_rightHandText != null)
		{
			int boneID = (int)Kinect.NuiSkeletonPositionIndex.HandRight;
			_rightHandText.text = string.Format("Right Hand: {0}\nPos - {1:F}, {2:F}, {3:F}\nRot - {4:F}, {5:F}, {6:F}",
			                                   	IsRightTracked() ? "Tracked" : "Not Tracked",
			                                    _handRightPos.x,
			                                    _handRightPos.y,
			                                    _handRightPos.z,
			             						sw.boneLocalOrientation[0, boneID].eulerAngles.x,
			             						sw.boneLocalOrientation[0, boneID].eulerAngles.y,
			             						sw.boneLocalOrientation[0, boneID].eulerAngles.z);
		}
	}
}
