using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRewardController : MonoBehaviour {

	public DynamicZombieAgent agent;

	private Vector3 randomPosition;
	private Vector3 chestRandomPosition;

	public GameObject pivot;
	private GameObject chest;

	private float timer;
	private float counter;

	public TrainingConfiguration trainer;

	void Start (){
		agent = GetComponent<DynamicZombieAgent> ();
		trainer= FindObjectOfType<TrainingConfiguration>();
		counter = 0f;
	}

	void Update () {
		timer += Time.deltaTime;

		chest = GameObject.FindGameObjectWithTag ("chest");
		chestRandomPosition= new Vector3(Random.Range(pivot.transform.position.x - 45,pivot.transform.position.x + 45), pivot.transform.position.y, Random.Range (pivot.transform.position.z - 45,pivot.transform.position.z + 45));
		randomPosition= new Vector3(Random.Range(pivot.transform.position.x - 45,pivot.transform.position.x + 45), pivot.transform.position.y, Random.Range (pivot.transform.position.z - 45,pivot.transform.position.z + 45));

		if (Vector3.Distance (gameObject.transform.position, pivot.transform.position) > 100)
			gameObject.transform.position = randomPosition;



	}
	public void OnCollisionEnter(Collision other){
		if (trainer.zombieBrainTrainer || trainer.noZombieBrainTrainer) { //Se sto addestrando Zombie Brain) 

			if (other.gameObject.CompareTag ("player") ){// e il cubo tocca il player
				agent.SetReward (2f);//Assegno una ricompensa di 2f( alta)
				agent.Done (); //L'agente ha fatto il suo dovere

				gameObject.transform.position = randomPosition; //Rispawna il cubetto random per ricominciare la ricerca

			}
			if (other.gameObject.CompareTag ("block") ) {//Se il cubo tocca un'altro cubo
				agent.SetReward (-2f); //Assegno un malus alto 
				agent.Done ();

				gameObject.transform.position = randomPosition;

			}

		}
		if (trainer.zombieBrainChestTrainer) { //Se sto addestrando Zombie Brain Chest) 

			if (other.gameObject.CompareTag ("chest")) {// se il cubo tocca il chest
				
				agent.SetReward (-3f);
				agent.Done ();
				other.collider.transform.position = chestRandomPosition;

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
	public void OnTriggerStay(Collider other){
		if (trainer.noZombieBrainTrainer) { //Se sto addestrando no Zombie Brain) 

			if (other.gameObject.CompareTag ("block")) { 

				if (timer > (counter + 5f)) { //Se rimango nel trigger per 5 secondi

					agent.SetReward (1.5f); //Assegno una ricompensa sostanziosa per incentivare la vicinanza con i cubi
					agent.Done ();
					gameObject.transform.position = randomPosition;
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
					other.transform.position = chestRandomPosition;	
					gameObject.transform.position = randomPosition;
					timer = 0;
					counter++;
				}
			}
		}
	}
}
