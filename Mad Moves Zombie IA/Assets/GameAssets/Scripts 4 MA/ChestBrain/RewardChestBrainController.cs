using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChestBrainController: MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController gameController;
	private RayPerception rayPerceptor;
	public GameObject detector;
	private CollisionDetector collisionDetector;

	private int timing=0;
	private GameObject[]zombieSpawn;

	private int randomZombieSpawn = 0 ;

	void Start (){
		collisionDetector = detector.GetComponent<CollisionDetector> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> (); //Il game Controller
		agent = GetComponent<DynamicZombieAgent> ();
		rayPerceptor = GetComponent<RayPerception> ();

		zombieSpawn = GameObject.FindGameObjectsWithTag ("zombieSpawn");
	}

	void Update () {
		randomZombieSpawn = Random.Range (0, zombieSpawn.Length);

		if (collisionDetector.getTimer () > timing) {
			agent.SetReward (2f * collisionDetector.mult);
			gameController.ResetChest ();
			gameController.hitCount++;
			if (timing <= 29) {
				timing += 5;
			} else {
				timing = 30;
			}
		}
	}




	public void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("chest")) {//Se lo zombie tocca un'altro zombie
			agent.SetReward (-4f); //Assegno un malus alto 
			agent.Done ();

			gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;
		}
	}


	private void TryToAttack(){//Serve solo che sia presente
		agent.SetReward(-4f);
		agent.Done ();
		gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;
	}
}
