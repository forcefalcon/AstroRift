using UnityEngine;
using System.Collections;

abstract public class SpaceObject: MonoBehaviour {
	//private static float SIZE_UNIT = 149597887;
	private static float ASTRONOMICAL_UNITS = 149597887;
	private static float SIZE_UNIT = ASTRONOMICAL_UNITS;
	public static float DISTANCE_UNIT = ASTRONOMICAL_UNITS/10;
	protected float mDistance;
	public double previousTime;
	private Vector3 mInitialPosition;

	Quaternion quaternion = Quaternion.identity;
	private float speed = 0.1f;
	private Vector3 axis = Vector3.up;

	protected virtual void Awake () {
		transform.localScale =
			GetTransformedSize(
				(float)GetDiameterKm());
		mInitialPosition = GetTransformedPosition();
		transform.position = mInitialPosition;			
	}

	public void SetDistance(double distance)
	{
		mDistance = (float)distance;
		transform.position = GetTransformedPosition();
	}


	// TODO Remove this since UpdatePosition(Vector) is going to be called from SpaceManager
	public void UpdatePosition (float deltaTime) {
		quaternion = quaternion * Quaternion.AngleAxis (deltaTime * speed, axis);
		UpdatePosition(quaternion * Vector3.forward * mDistance);
		//transform.position = quaternion * GetTransformedPosition();
	}
	
	public void UpdatePosition(Vector3 vector)
	{
		transform.position = GetTransformedPosition (vector);
	}
	void Update()
	{
		if (previousTime == 0) {
			previousTime = TimeManager.Instance.CurrentTime;
			return;
		}
		float deltaTime = (float)(TimeManager.Instance.CurrentTime - previousTime);
		UpdatePosition(deltaTime);
		previousTime = TimeManager.Instance.CurrentTime;
		
		
	}
	Vector3 GetTransformedSize (float size) {
		float logSize = Mathf.Log (size, SIZE_UNIT);
		return new Vector3 (logSize, logSize, logSize);
	}

	protected Vector3 GetTransformedPosition () {
		return GetTransformedPosition(new Vector3 (0, 0, (float)GetDistanceKm()));
	}

	protected Vector3 GetTransformedPosition (Vector3 vector) {
		return vector / DISTANCE_UNIT;
	}

	abstract public double GetDiameterKm();

	abstract public double GetDistanceKm();


}