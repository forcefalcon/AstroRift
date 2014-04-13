using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceManager : MonoBehaviour
{
	private List<Asteroid> spaceObjects;
	public GameObject AsteroidPrefab;
	List<Planet> planets = new List<Planet>();
	
	TimeCache planetPositionCache = new TimeCache();
	TimeCacheAsteroid asteroidCache = new TimeCacheAsteroid();

	public void Awake()
	{
		planets.Add(GameObject.Find("Mercury").GetComponent<Planet>());
		planets.Add(GameObject.Find("Venus").GetComponent<Planet>());
		planets.Add(GameObject.Find("Earth").GetComponent<Planet>());
		planets.Add(GameObject.Find("Mars").GetComponent<Planet>());
		planets.Add(GameObject.Find("Saturn").GetComponent<Planet>());
		planets.Add(GameObject.Find("Jupiter").GetComponent<Planet>());
		planets.Add(GameObject.Find("Uranus").GetComponent<Planet>());
		planets.Add(GameObject.Find("Neptune").GetComponent<Planet>());

		spaceObjects = new List<Asteroid> ();
		buildAsteoridBelt ();
		OrbitWrapper.InitializeDatabase(0);
	}

	public void OnDestroy()
	{
		OrbitWrapper.UninitializeDatabase();
	}


	public void buildAsteoridBelt() 
	{
		for (int i = 0; i < OrbitWrapper.ASTEROID_COUNT; i++) {
			GameObject asteroidClone = 
				(GameObject)Instantiate(AsteroidPrefab);
			
			asteroidClone.transform.parent = transform;
			Asteroid asteroid = asteroidClone.GetComponent<Asteroid>();
			spaceObjects.Add(asteroid);
		}
	}

	public void Update()
	{
		List<Vector3> planetPositions = planetPositionCache.GetTime(
			TimeManager.Instance.CurrentTime);
		for (int i = 0; i < planetPositions.Count; i++) {
			planets[i].UpdatePosition(
				planetPositions[i]);
		}
		List<Vector3> asteroidPositions = asteroidCache.GetTimeAsteroid(
			TimeManager.Instance.CurrentTime);
		for (int i = 0; i < spaceObjects.Count; i++) {
			spaceObjects[i].UpdatePosition(
				asteroidPositions[i]);
		}
	}
	

	public class TimeCache
	{
		Dictionary<int, List<Vector3>> cache = 
			new Dictionary<int, List<Vector3>> ();
		public List<Vector3> GetTime(double currentTime)
		{
			int currentDay = (int)currentTime;
			List<Vector3> currentDayPlanets = GetTime(
				currentDay);
			List<Vector3> nextDayPlanets = GetTime(
				currentDay+1);
			List<Vector3> newVector = new List<Vector3> ();
			for (int i = 0; i < currentDayPlanets.Count; i++) {
				newVector.Add(currentDayPlanets[i] + 
				((nextDayPlanets[i] - currentDayPlanets[i]) * 
				   (float)(TimeManager.Instance.CurrentTime - currentDay)));
			}
			return newVector;
		}
		private List<Vector3> GetTime(int currentTime)
		{
			if (!cache.ContainsKey(currentTime))
			{ 
				List<Vector3> list = OrbitWrapper.GetPlanetsOrbit2(currentTime);
				cache.Add(currentTime, list);
			}
			return cache [currentTime];
		}
	};

	public class TimeCacheAsteroid
	{
		Dictionary<int, List<Vector3>> cache = 
			new Dictionary<int, List<Vector3>> ();

		public List<Vector3> GetTimeAsteroid(double currentTime)
		{
			int currentDay = (int)currentTime;
			List<Vector3> currentDayPlanets = GetTimeAsteroid(
				currentDay);
			List<Vector3> nextDayPlanets = GetTimeAsteroid(
				currentDay+1);
			List<Vector3> newVector = new List<Vector3> ();
			for (int i = 0; i < currentDayPlanets.Count; i++) {
				newVector.Add(currentDayPlanets[i] + 
				              ((nextDayPlanets[i] - currentDayPlanets[i]) * 
				 (float)(TimeManager.Instance.CurrentTime - currentDay)));
			}
			return newVector;
		}
		private List<Vector3> GetTimeAsteroid(int currentTime)
		{
			if (!cache.ContainsKey(currentTime))
			{ 
				List<Vector3> list = OrbitWrapper.GetAsteroidOrbit2(currentTime);
				cache.Add(currentTime, list);
			}
			return cache [currentTime];
		}
	};

	


}
