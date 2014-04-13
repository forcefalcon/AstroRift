using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandControl : MonoBehaviour
{
	private Dictionary<Collider, bool> collisions;
	
	void Awake() {
		collisions = new Dictionary<Collider, bool>();
	}
	
	void OnTriggerEnter(Collider collider) {
		collisions[collider] = true;
	}
	
	void OnTriggerExit(Collider collider) {
		collisions[collider] = false;
	}
	
	public bool IsCollidingWith(Collider collider) {
		return collisions.ContainsKey(collider) && collisions[collider];
	}
}
