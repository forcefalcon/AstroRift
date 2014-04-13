using UnityEngine;
using System.Collections;

public class Planet : SpaceObject {
	protected override void Awake()
	{
		base.Awake ();
		//UpdatePosition(Random.value * 3600);
	}
	
	override public double GetDiameterKm()
	{
		switch (this.gameObject.name) 
		{
		case  "Mercury":
			return 4879.4;
		case "Venus":
			return 12103.6;
		case  "Earth":
			return 12756.274;
		case  "Mars":
			return 6792.4;
		case  "Saturn":
			return 116464;
		case  "Jupiter":
			return 142984;
		case  "Uranus":
			return 51118;
		case  "Neptune":
			return 49528;
		}
		return 0;
	}
	
	override public double GetDistanceKm()
	{
		switch (this.gameObject.name) 
		{
		case  "Mercury":
			return 57909176/ASTRONOMICAL_UNITS;
		case "Venus":
			return 108208930/ASTRONOMICAL_UNITS;
		case  "Earth":
			return 149597887.5/ASTRONOMICAL_UNITS;
		case  "Mars":
			return 227936637/ASTRONOMICAL_UNITS;
		case  "Jupiter":
			return 778412027/ASTRONOMICAL_UNITS;
		case  "Saturn":
			return 1421179772/ASTRONOMICAL_UNITS;
		case  "Uranus":
			return 2876679082/ASTRONOMICAL_UNITS;
		case  "Neptune":
			return 4503443661/ASTRONOMICAL_UNITS;
		}
		return 0;
	}
}
