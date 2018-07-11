using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardControllerNoZombie: MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController gameController;
	private RayPerception rayPerceptor;
	public GameObject detector;
	private CollisionDetector collisionDetector;

	private int timing=10;
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
		
		if (agent.getAction () == 2 || agent.getAction () == 3 || agent.getAction () == 0) {//Deve muoversi camminando senza attaccare
			agent.AddReward (-2f);// Malus alto
			agent.Done ();
		}
		randomZombieSpawn = Random.Range (0, zombieSpawn.Length);

		if (collisionDetector.getTimer () > timing) {
			agent.AddReward (2f);
			gameController.hitCount++;
			timing += 1;
			collisionDetector.resetTimer ();
		}
	}


	

	public void OnCollisionEnter(Collision other){
		 if (other.gameObject.CompareTag ("block")) {//Se lo zombie tocca un'altro zombie
				agent.SetReward (-2f); //Assegno un malus alto 
				agent.Done ();

				gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;
			}
		}

	private void TryToAttack(){//Serve solo che sia presente
			
		}
}

