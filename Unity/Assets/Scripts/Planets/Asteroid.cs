using UnityEngine;
using System;

public class Asteroid : SpaceObject {

	override public double GetDiameterKm()
	{
		return 5;
	}
	
	override public double GetDistanceKm()
	{
		return mDistance;
	}
}


