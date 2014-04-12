using UnityEngine;
using System;

public class Asteroid : SpaceObject {
	private float mDistance;
	private float speed = 2;
	private float angle = 0;
	private Vector3 axis = Vector3.up;
	Quaternion quaternion = Quaternion.identity;
	public void SetDistance(double distance)
	{
		mDistance = (float)distance;
		transform.position = GetTransformedPosition();
	}

	// TODO Remove this since UpdatePosition(Vector) is going to be called from SpaceManager
	public void UpdatePosition () {
		angle += speed * Time.deltaTime;
		quaternion = quaternion * Quaternion.AngleAxis (Time.deltaTime * speed, axis);
		UpdatePosition(quaternion * Vector3.forward * mDistance);
		//transform.position = quaternion * GetTransformedPosition();
	}

	public void UpdatePosition(Vector3 vector)
	{
		transform.position = GetTransformedPosition (vector);
	}

	override public double GetDiameterKm()
	{
		return 7;
	}
	
	override public double GetDistanceKm()
	{
		return mDistance;
	}
}


