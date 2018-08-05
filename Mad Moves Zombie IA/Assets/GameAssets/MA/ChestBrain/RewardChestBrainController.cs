using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChestBrainController: MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController4MA gameController;

	private GameObject[]zombieSpawn;
	private int randomZombieSpawn = 0 ;

	void Start (){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController4MA> (); //Il game Controller
		agent = GetComponent<DynamicZombieAgent> ();
		zombieSpawn = GameObject.FindGameObjectsWithTag ("zombieSpawn");
	}

	void Update () {
		randomZombieSpawn = Random.Range (0, zombieSpawn.Length);
	}
		

	private void TryToAttack(){//Serve solo che sia presente
		agent.SetReward(-4f);
		agent.Done ();
	}
}
