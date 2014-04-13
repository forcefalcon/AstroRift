using UnityEngine;
using System;

public class Asteroid : SpaceObject {

	override public double GetDiameterKm()
	{
		return 7;
	}
	
	override public double GetDistanceKm()
	{
		return mDistance;
	}
}


