using UnityEngine;
using System.Collections;

public class Sun : Planet {
	protected override void Awake()
	{
		base.Awake ();
	}

	override public double GetDiameterKm()
	{
		return 2785368;
	}

	override public double GetDistanceKm()
	{
		return 0;
	}
}
