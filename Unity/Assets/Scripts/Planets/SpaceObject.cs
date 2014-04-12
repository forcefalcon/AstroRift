using UnityEngine;
using System.Collections;

abstract public class SpaceObject: MonoBehaviour {
	//private static float SIZE_UNIT = 149597887;
	private static float ASTRONOMICAL_UNITS = 149597887;
	private static float SIZE_UNIT = ASTRONOMICAL_UNITS/10;
	private static float DISTANCE_UNIT = ASTRONOMICAL_UNITS;


	protected virtual void Awake () {
		transform.localScale =
			GetTransformedSize(
				(float)GetDiameterKm());

		transform.position = GetTransformedPosition();
				
	}

	Vector3 GetTransformedSize (float size) {
		float logSize = Mathf.Log (size, DISTANCE_UNIT);
		return new Vector3 (logSize, logSize, logSize);
	}

	protected Vector3 GetTransformedPosition () {
		return GetTransformedPosition(new Vector3 (0, 0, (float)GetDistanceKm()));
	}

	protected Vector3 GetTransformedPosition (Vector3 vector) {
		return vector / SIZE_UNIT;
	}

	abstract public double GetDiameterKm();

	abstract public double GetDistanceKm();


}