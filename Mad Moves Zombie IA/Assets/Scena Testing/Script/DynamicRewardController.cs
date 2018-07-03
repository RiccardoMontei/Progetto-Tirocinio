using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicRewardController : MonoBehaviour {

	public DynamicZombieAgent agent;

	private Vector3 randomPosition;
	private Vector3 chestRandomPosition;

	public GameObject [] players= new GameObject[15];
	public GameObject player;
	public float minDistance=5000.0f;
	public GameObject pivot;
	private GameObject chest;

	public Text counterText;


	private float timer;
	private float counter;
	public GameObject[]zombieSpawn= new GameObject[46];
	public GameObject[] playerSpawn = new GameObject[22];
	public TrainingConfiguration trainer;
	private int randomZombieSpawn;
	private int randomPlayerSpawn;

	void Start (){
		agent = GetComponent<DynamicZombieAgent> ();
		trainer= FindObjectOfType<TrainingConfiguration>();
		counter = 0f;
	}

	void Update () {
		timer += Time.deltaTime;
		randomPlayerSpawn = Random.Range (0, playerSpawn.Length);
		randomZombieSpawn = Random.Range (0, zombieSpawn.Length);

		for (int i = 0; i < players.Length; i++) {
			if (players [i].activeInHierarchy) {
				if (Vector3.Distance (gameObject.transform.position, players [i].transform.position) < minDistance) {
					minDistance = Vector3.Distance (gameObject.transform.position, players [i].transform.position);
					player = players [i];
				}
			} 
				
		}
	}
	public void OnCollisionEnter(Collision other){
		if (trainer.zombieBrainTrainer || trainer.noZombieBrainTrainer) { //Se sto addestrando Zombie Brain) 

			if (other.gameObject.CompareTag ("block") ) {//Se il cubo tocca un'altro cubo
				agent.SetReward (-1f); //Assegno un malus alto 
				agent.Done ();

				gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;


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
	private void TryToAttack(){
		Debug.Log ("OnTry");
		Debug.Log (Vector3.Distance (transform.position, player.transform.position));
		if (Vector3.Distance (transform.position, player.transform.position) <= 4.1f) {
			agent.AddReward (4f);//Assegno una ricompensa di 2f( alta)
			agent.Done (); //L'agente ha fatto il suo dovere
			Debug.Log("Hitted");

			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().hitCount += 1;
			if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().hitCount % 5 == 0 && GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().hitCount != 0)
				players [Random.Range (0, players.Length)].SetActive (false);
			counterText.text =GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().hitCount.ToString ();
			gameObject.transform.position = zombieSpawn [randomZombieSpawn].transform.position;
			player.transform.position = playerSpawn [randomPlayerSpawn].transform.position;
		} else {
			Debug.Log ("Fail");
			agent.AddReward (-0.5f);
			agent.Done ();
		}
	}
}
