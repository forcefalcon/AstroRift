using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

class OrbitWrapper {

	
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
	public static List<Vector3> GetPlanetsOrbit2 (int timeStamp) 
	{
		List<Vector3> coordinates = new List<Vector3>();
		for (int i = 0; i < 8; i++) 
		{
			Quaternion quaternion = Quaternion.identity;
			quaternion = quaternion * Quaternion.AngleAxis (timeStamp, Vector3.up);
			coordinates.Add (quaternion * Vector3.forward * GetDistanceKm (i));
		}
		return coordinates;
	}
	static List<Vector3> sInitialPosition = null;
	public static List<Vector3> GetAsteroidOrbit2 (int timeStamp) 
	{
		if (sInitialPosition == null) 
		{
			sInitialPosition = new List<Vector3>();
			for (int i = 0; i < 5; i++)
			{
				sInitialPosition.Add(
					new Vector3(0, 0, Random.value * 350475390/ASTRONOMICAL_UNITS + 327936637/ASTRONOMICAL_UNITS));
			}
		}

		List<Vector3> coordinates = new List<Vector3>();
		for (int i = 0; i < 5; i++) 
		{
			Quaternion quaternion = Quaternion.identity;
			quaternion = quaternion * Quaternion.AngleAxis (timeStamp, Vector3.up);
			coordinates.Add (quaternion * sInitialPosition[i]);
		}
		return coordinates;
	}

} 