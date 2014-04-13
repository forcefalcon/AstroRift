using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceManager : MonoBehaviour
{
	private List<Asteroid> spaceObjects;
	public GameObject AsteroidPrefab;
	OrbitWrapper orbitWrapper = new OrbitWrapper ();
	public void Awake()
	{
		spaceObjects = new List<Asteroid> ();
		buildAsteoridBelt ();
			orbitWrapper.InitializeDatabase(0);


	}


	public void buildAsteoridBelt() 
	{
		for (int i = 0; i < 5; i++) {
			GameObject asteroidClone = 
				(GameObject)Instantiate(AsteroidPrefab);
			
			asteroidClone.transform.parent = transform;
			Asteroid asteroid = asteroidClone.GetComponent<Asteroid>();
			asteroid.SetDistance(Random.value * 350475390 + 327936637);
			spaceObjects.Add(asteroid);
			asteroid.UpdatePosition(Random.value * 3600);			                      
		}
	}

	public void Update()
	{
		List<OrbitWrapper.Coordinate> data = orbitWrapper.GetPlanetsOrbit(
			(int) TimeManager.Instance.CurrentTime);
		Planet mercury;
		mercury = GameObject.Find("Mercury").GetComponent<Planet>();
		Vector3 vector = new Vector3 (data [0].x, data [0].y, data [0].z);
		mercury.UpdatePosition (vector * SpaceObject.DISTANCE_UNIT);
	}
	




	


}
