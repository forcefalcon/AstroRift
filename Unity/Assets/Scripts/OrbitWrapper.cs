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

	[StructLayout(LayoutKind.Sequential)]
	unsafe struct coordinates
	{
		public double index;
		public double H;
		public double x;
		public double y;
		public double z;
		public double r;
	}

	public struct Coordinate
	{
		public float x;
		public float y;
		public float z;
	}

	public void InitializeDatabase (int timeStamp) {
		initializeDatabase((uint)timeStamp);
	}
	public List<Coordinate> GetPlanetsOrbit (int timeStamp) 
	{
		List<Coordinate> coordinates = new List<Coordinate>();
		unsafe
		{
			coordinates* data = 
				(coordinates*) getPlanetsOrbits((uint)timeStamp);
			for (int i = 0; i < 8; i++)
			{
				Coordinate coordinateWrapper =
					new Coordinate();
				coordinateWrapper.x = (float)data[i].x;
				coordinateWrapper.y = (float)data[i].y;
				coordinateWrapper.z = (float)data[i].z;
				coordinates.Add(coordinateWrapper);
			}
		}
		return coordinates;
	}
} 