using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardControllerNoZombie: MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController4MA gameController;
	private RayPerception rayPerceptor;
	public GameObject detector;
	private CollisionDetectorGroup collisionDetector;

	private int timing=5;
	//private GameObject[]zombieSpawn;

	//private int randomZombieSpawn = 0 ;

	void Start (){
		collisionDetector = detector.GetComponent<CollisionDetectorGroup> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController4MA> (); //Il game Controller
		agent = GetComponent<DynamicZombieAgent> ();
		rayPerceptor = GetComponent<RayPerception> ();

		//zombieSpawn = GameObject.FindGameObjectsWithTag ("zombieSpawn");
	}

	void Update () {
		//randomZombieSpawn = Random.Range (0, zombieSpawn.Length);

		if (collisionDetector.zombieNear) {
			agent.SetReward (1f);
			//gameController.hitCount++;
			/*
			if (timing <= 29) {
				timing += 1;
			} else {
				timing = 30;
			}
			*/
			//collisionDetector.resetTimer ();
		}
	}


	

	public void OnCollisionEnter(Collision other){
		 if (other.gameObject.CompareTag ("block")) {//Se lo zombie tocca un'altro zombie
				agent.SetReward (-4f); //Assegno un malus alto 
				agent.Done ();

			gameObject.transform.position = new Vector3 (Random.Range (-120, 120), transform.position.y, Random.Range (-120, 120));
			}
		}


	private void TryToAttack(){//Serve solo che sia presente
		agent.SetReward(-4f);
		agent.Done ();
		gameObject.transform.position = new Vector3 (Random.Range (-120, 120), transform.position.y, Random.Range (-120, 120));
	}
}

