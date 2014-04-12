using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	/*
	 * Rotation
	 */	
	public float RotationSensitivityX = 15F;
	public float RotationSensitivityY = 15F;
	public float RotationSensitivityZ = 15F;
	
	/*
	 * Movement
	 */
	public float MovementSensitivityX = 1F;
	public float MovementSensitivityY = 1F;
	public float MovementSensitivityZ = 1F;
	
	void Update()
	{
		UpdateRotation();
		UpdateMovement();
	}
	
	void UpdateRotation()
	{
		Quaternion rotX = Quaternion.AngleAxis(Input.GetAxis("Right X") * RotationSensitivityX * Time.deltaTime, Vector3.up);
		Quaternion rotY = Quaternion.AngleAxis(Input.GetAxis("Right Y") * RotationSensitivityY * Time.deltaTime, Vector3.left);
		
		float angZ = Input.GetButton("Left Bumper") ? RotationSensitivityZ : 0.0f;
		angZ -= Input.GetButton("Right Bumper") ? RotationSensitivityZ : 0.0f;
		angZ *= Time.deltaTime;
		Quaternion rotZ = Quaternion.AngleAxis(angZ, Vector3.forward);
		
		transform.localRotation = transform.localRotation * rotX * rotY * rotZ;
	}
	
	void UpdateMovement()
	{
		transform.Translate(new Vector3(Input.GetAxis("Left X") * MovementSensitivityX * Time.deltaTime, -1.0f * Input.GetAxis("Triggers") * MovementSensitivityZ * Time.deltaTime, -1.0f * Input.GetAxis("Left Y") * MovementSensitivityY * Time.deltaTime), Space.Self);
	}
}
