using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicRewardController : MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController gameController;

	private GameObject player;
	private bool hitted=false;

	private GameObject chest;

	private float timer = 0 ;
	private float counter = 0 ;

	public GameObject[]zombieSpawn= new GameObject[46];

	private TrainingConfiguration trainer;
	private int randomZombieSpawn = 0 ;




	void Start (){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> (); //Il game Controller
		agent = GetComponent<DynamicZombieAgent> ();
		trainer= FindObjectOfType<TrainingConfiguration>();
	}

	void Update () {
		timer += Time.deltaTime;
		randomZombieSpawn = Random.Range (0, zombieSpawn.Length);
	}

	public void OnCollisionEnter(Collision other){
		if (trainer.zombieBrainTrainer || trainer.noZombieBrainTrainer) { //Se sto addestrando Zombie Brain) 
			if (other.gameObject.CompareTag ("block")) {//Se lo zombie tocca un'altro zombie
				agent.SetReward (-1f); //Assegno un malus alto 
				agent.Done ();

				gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;
			}
		}
	}
	/*
		if (trainer.zombieBrainChestTrainer) { //Se sto addestrando Zombie Brain Chest) 

			if (other.gameObject.CompareTag ("chest")) {// se il cubo tocca il chest
				
				agent.SetReward (-3f);
				agent.Done ();

			}
		}

	}
	public void OnTriggerEnter(Collider other){
		if (trainer.noZombieBrainTrainer) { //Se sto addestrando No Zombie Brain) 

			if (other.gameObject.CompareTag ("block")) { // se il cubo entra nel trigger di un'altro cubo
				timer = 0; //azzero il mio timer
				agent.SetReward (1.5f); //Assegno un reward basso per incentivare la ricerca
				agent.Done (); 
			}
		}	
		if (trainer.zombieBrainChestTrainer) { //Se sto addestrando Zombie Brain Chest) 

			if (other.gameObject.CompareTag ("chest")) {// se il cubo entra nel triggr della chest
				timer = 0;
				agent.SetReward (1f);

			}
		}

	}
	/*public void OnTriggerStay(Collider other){
		if (trainer.noZombieBrainTrainer) { //Se sto addestrando no Zombie Brain) 

			if (other.gameObject.CompareTag ("block")) { 

				if (timer > (counter + 5f)) { //Se rimango nel trigger per 5 secondi

					agent.SetReward (1.5f); //Assegno una ricompensa sostanziosa per incentivare la vicinanza con i cubi
					agent.Done ();
					timer = 0;
					counter++;
				}
			}
		}
		if (trainer.zombieBrainChestTrainer) { //Se sto addestrando Zombie Brain Chest)  

			if (other.gameObject.CompareTag ("chest")) {

				if (timer > (counter + 5f)) { //Se rimango nel trigger della chest per 5 secondi

					agent.SetReward (2f); //Assegno una ricompensa sostanzisa per incentivare la vicinanza
					agent.Done ();
					timer = 0;
					counter++;
				}
			}
		}
	}*/

	private void OnTriggerStay( Collider objectHitted ){
		//Se nel triggerdavanti lo zombie c'è il player
		if (objectHitted.transform.gameObject.CompareTag ("player")){
			player = objectHitted.transform.gameObject;//assegno quel player alla variabile
			hitted = true; //lo sto colpendo!
		}else{
			player=null;
			hitted=false;//Non lo sto colpendo in qualsiasi altro caso
		}
			
	}

	private void TryToAttack(){//Funzione evento dell'animazione di attacco, richiamata nel frame in cui la mano è alla massima estensione(Dentro il trigger)
		if (hitted) {//Se sto colpendo nel momento di massima estensione

			agent.AddReward (6f);//Assegno una ricompensa alta

			Debug.Log ("Hitted");
			gameController.hitCount += 1; 

			gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;//Repawno lo zombie TODO decidere se lasciarlo o meno
			gameController.RespawnFunction(player); //Respawno il player


			//Eliminazione graduale players
			if (gameController.hitCount == 50) 
				player.SetActive (false);

			if (gameController.hitCount == 90) 
				player.SetActive (false);

			if (gameController.hitCount == 120) 
				player.SetActive (false);

			if (gameController.hitCount == 140) 
				player.SetActive (false);

			if (gameController.hitCount == 150) 
				player.SetActive (false);

			agent.Done (); //L'agente ha fatto il suo dovere
			hitted=false; //Proteggo l'operazione per essere sicuro che sia atomica
		
		} else {
			hitted = false;//Se non sto colpendo hitted per sicurezza chiudo il flag
			agent.AddReward (-0.5f);// Malus alto
		}
	}
}
