using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	[SerializeField] private Transform player;
	[SerializeField] private GameObject zombie;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (zombie != null) {
			if (!zombie.GetComponent<ZombieController> ().isTriggered && zombie.GetComponent<ZombieController> ().lifePoints > 0) {
				zombie.GetComponent<ZombieController> ().TargetingSystem (player);
			}
		}
	}


}
