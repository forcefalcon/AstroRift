using UnityEngine;
using System.Collections;

public class Planet : SpaceObject {
	protected override void Awake()
	{
		base.Awake ();
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
			return 57909176;
		case "Venus":
			return 108208930;
		case  "Earth":
			return 149597887.5;
		case  "Mars":
			return 227936637;
		case  "Jupiter":
			return 778412027;
		case  "Saturn":
			return 1421179772;
		case  "Uranus":
			return 2876679082;
		case  "Neptune":
			return 4503443661;
		}
		return 0;
	}
}
