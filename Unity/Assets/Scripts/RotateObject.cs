using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
	public Vector3 Axis = Vector3.up;
	public float AngularSpeed = 60.0f;
	
	void Awake()
	{
		Axis.Normalize();
	
		transform.localRotation = Quaternion.FromToRotation(Vector3.up, Axis);
	}
	
	void Update()
	{
		transform.localRotation *= Quaternion.AngleAxis(AngularSpeed * Time.deltaTime, Axis);
	}
}
