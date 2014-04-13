using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceManager : MonoBehaviour
{
	private List<Asteroid> spaceObjects;
	public GameObject AsteroidPrefab;

	public void Awake()
	{
		spaceObjects = new List<Asteroid> ();
		buildAsteoridBelt ();
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

	


}
