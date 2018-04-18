using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	[SerializeField] private Transform player;
	[SerializeField] private GameObject zombie;

	// Use this for initialization
	void Start () {
		Invoke ("StartHunt", 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void StartHunt(){
		if (!zombie.GetComponent<ZombieController> ().isTriggered) {
			zombie.GetComponent<ZombieController> ().TargetingSystem (player);
		}
	}


}
