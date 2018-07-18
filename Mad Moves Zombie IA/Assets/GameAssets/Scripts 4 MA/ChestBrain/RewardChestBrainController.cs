using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChestBrainController: MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController gameController;

	private GameObject[]zombieSpawn;
	private int randomZombieSpawn = 0 ;

	void Start (){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> (); //Il game Controller
		agent = GetComponent<DynamicZombieAgent> ();
		zombieSpawn = GameObject.FindGameObjectsWithTag ("zombieSpawn");
	}

	void Update () {
		randomZombieSpawn = Random.Range (0, zombieSpawn.Length);
	}
		
	public void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("block") ) {//Se lo zombie tocca un'altro zombie
			agent.SetReward (-4f); //Assegno un malus alto 
			agent.Done ();

			gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;
		}
	}


	private void TryToAttack(){//Serve solo che sia presente
		agent.SetReward(-4f);
		agent.Done ();
	}
}
