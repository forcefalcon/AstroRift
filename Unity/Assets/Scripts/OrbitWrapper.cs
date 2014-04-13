using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

class OrbitWrapper {
	public static int ASTEROID_COUNT=1000;
	
	// Other platforms load plugins dynamically, so pass the name
	// of the plugin's dynamic library.
	[DllImport ("orbitsData")]
	internal static extern int initializeDatabase (uint timestamp);

	[DllImport("orbitsData", SetLastError = true)]
	internal static extern System.IntPtr getPlanetsOrbits(uint timestamp);

	[DllImport ("orbitsData")]
	internal static extern void uninitializeDatabase();


	[StructLayout(LayoutKind.Sequential)]
    public struct Coordinates
	{
		public double index;
		public double H;
		public double x;
		public double y;
		public double z;
		public double r;
	}

	public static void InitializeDatabase (int timeStamp) {
		initializeDatabase((uint)timeStamp);
	}

	public static void UninitializeDatabase() {
		uninitializeDatabase();
	}
	protected static float ASTRONOMICAL_UNITS = 149597887;
	internal static float GetDistanceKm(int index)
	{
		switch (index) 
		{
		case  0:
			return 57909176/ASTRONOMICAL_UNITS;
		case 1:
			return 108208930/ASTRONOMICAL_UNITS;
		case  2:
			return 149597887.5f/ASTRONOMICAL_UNITS;
		case  3:
			return 227936637/ASTRONOMICAL_UNITS;
		case  4:
			return 778412027/ASTRONOMICAL_UNITS;
		case  5:
			return 1421179772/ASTRONOMICAL_UNITS;
		case  6:
			return 2876679082/ASTRONOMICAL_UNITS;
		case  7:
			return 4503443661/ASTRONOMICAL_UNITS;
		}
		return 0;
	}
	public static List<Vector3> GetPlanetsOrbit (int timeStamp) 
	{
		List<Vector3> planets = new List<Vector3>();

		System.IntPtr structPtr = getPlanetsOrbits ((uint)timeStamp);
		var structSize = Marshal.SizeOf(typeof(Coordinates));
		var coordinates = new List<Coordinates>();
		var ptr = structPtr;
		for (int i = 0; i < 8; i++)
		{
			coordinates.Add((Coordinates)Marshal.PtrToStructure(ptr, 
			                                                     typeof(Coordinates)));
			ptr = (System.IntPtr)((int)ptr + structSize);
		}
		for (int i = 0; i < 8; i++)
		{
			planets.Add(
					new Vector3((float) coordinates[i].x, (float)coordinates[i].y, (float)coordinates[i].z));
		}
		return planets;
	}
	static List<Vector3> sPlanetPosition = null;
	public static List<Vector3> GetPlanetsOrbit2 (int dayStamp) 
	{
		if (sPlanetPosition == null) 
		{
			sPlanetPosition = new List<Vector3>();
			for (int i = 0; i < 8; i++)
			{
				sPlanetPosition.Add(
					GetDisplacement(
					(int)(Random.value * 3600),
					new Vector3(0, GetBoxMuller(0, 0.025f), GetDistanceKm(i)))); 
			}
		}
		List<Vector3> coordinates = new List<Vector3>();
		for (int i = 0; i < sPlanetPosition.Count; i++) 
		{
			coordinates.Add (GetDisplacement(dayStamp, sPlanetPosition[i]));
		}
		return coordinates;
	}
	static List<Vector3> sInitialPosition = null;
	static List<int> sInitialShift = null;
	public static List<Vector3> GetAsteroidOrbit2 (int dayStamp) 
	{
		if (sInitialPosition == null) 
		{
			sInitialPosition = new List<Vector3>();
			sInitialShift = new List<int>();
			for (int i = 0; i < ASTEROID_COUNT; i++)
			{

				sInitialPosition.Add(
					GetDisplacement(
					(int)(Random.value * 3600),
					new Vector3(0, 
				            GetBoxMuller(0, 3f)*0.05f,
				            GetBoxMuller(503174332/ASTRONOMICAL_UNITS, 0.5f)))); 
				            	//Random.value * 350475390/ASTRONOMICAL_UNITS + 327936637/ASTRONOMICAL_UNITS)));
			}
		}

		List<Vector3> coordinates = new List<Vector3>();
		for (int i = 0; i < sInitialPosition.Count; i++) 
		{
			coordinates.Add (GetDisplacement(dayStamp, sInitialPosition[i]));
		}
		return coordinates;
	}
	private static Vector3 GetDisplacement(int day, Vector3 initialPosition)
	{
		Quaternion quaternion = Quaternion.identity;
		quaternion = quaternion * Quaternion.AngleAxis (day, Vector3.up);
		//quaternion = quaternion * Quaternion.AngleAxis (day, Vector3.Cross(initialPosition, Vector3.up));
		//Quaternion left = Quaternion.identity;
		//left = left * Quaternion.AngleAxis (5, Vector3.left);
		Vector3 temp = ((quaternion * initialPosition));
		return temp;//Vector3.Cross(temp, Vector3.up);
	}

	static float GetBoxMuller(float mean, float stdDev)
	{
		float u1 = Random.value; //these are uniform(0,1) random doubles
		float u2 = Random.value;
		double randStdNormal = Mathf.Sqrt((float)(-2.0 * Mathf.Log(u1))) *
			Mathf.Sin((float)(2.0 * Mathf.PI * u2)); //random normal(0,1)
		return (float)
			(mean + stdDev * randStdNormal); //random normal(mean,stdDev^2)
	}

} 