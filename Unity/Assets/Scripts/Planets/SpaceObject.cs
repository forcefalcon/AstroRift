using UnityEngine;
using System.Collections;

abstract public class SpaceObject: MonoBehaviour {
	//private static float SIZE_UNIT = 149597887;
	protected static float ASTRONOMICAL_UNITS = 149597887;
	protected static float SIZE_UNIT = ASTRONOMICAL_UNITS;
	public static float DISTANCE_UNIT = 0.25f;
	protected float mDistance;
	public double previousTime = 0;

	protected virtual void Awake () {
		transform.localScale =
			GetTransformedSize(
				(float)GetDiameterKm());
	}

	public void UpdatePosition(Vector3 vector)
	{
		transform.position = GetTransformedPosition (vector);
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