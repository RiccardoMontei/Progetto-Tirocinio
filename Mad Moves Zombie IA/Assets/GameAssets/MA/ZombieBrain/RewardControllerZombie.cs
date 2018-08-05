using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardControllerZombie : MonoBehaviour {

	private DynamicZombieAgent agent;
	private GameController4MA gameController;

	private GameObject player;
	private bool hitted=false;

	private float timer = 0 ;

	//private GameObject[]zombieSpawn;

	//private int randomZombieSpawn = 0 ;




	void Start (){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController4MA> (); //Il game Controller
		agent = GetComponent<DynamicZombieAgent> ();
		//zombieSpawn = GameObject.FindGameObjectsWithTag ("zombieSpawn");
	}

	void Update () {
		timer += Time.deltaTime;
		//randomZombieSpawn = Random.Range (0, zombieSpawn.Length);
	}

	/*public void OnCollisionEnter(Collision other){
		
		if (other.gameObject.CompareTag ("block")) {//Se lo zombie tocca un'altro zombie
				agent.SetReward (-0.5f); //Assegno un malus alto 
				agent.Done ();

					gameObject.transform.position = new Vector3 (Random.Range(-120,120),gameObject.transform.position.y,Random.Range(-120,120));
			}
	}*/

	private void OnTriggerStay( Collider objectHitted ){
		//Se nel trigger davanti lo zombie c'è il player
		if (objectHitted.transform.gameObject.CompareTag ("player")){
			player = objectHitted.transform.gameObject;//assegno quel player alla variabile
			hitted = true; //lo sto colpendo!
		}else{
			hitted=false;//Non lo sto colpendo in qualsiasi altro caso
			player=null;

		}
			
	}

	private void TryToAttack(){//Funzione evento dell'animazione di attacco, richiamata nel frame in cui la mano è alla massima estensione(Dentro il trigger)
		if (hitted) {//Se sto colpendo nel momento di massima estension
			//gameController.hitCount += 1; 

			//gameObject.transform.position = new Vector3 (Random.Range(-120,120),gameObject.transform.position.y,Random.Range(-120,120));//Repawno lo zombie TODO decidere se lasciarlo o meno
			gameController.RespawnFunction(player); //Respawno il player

			/*//Eliminazione graduale players
			if (gameController.hitCount == 50) 
				player.SetActive (false);

			if (gameController.hitCount == 90) 
				player.SetActive (false);

			if (gameController.hitCount == 120) 
				player.SetActive (false);

			if (gameController.hitCount == 140) 
				player.SetActive (false);

			if (gameController.hitCount == 150) 
				player.SetActive (false);*/
			agent.SetReward (2f);//Assegno una ricompensa alta

			agent.Done (); //L'agente ha fatto il suo dovere
			hitted=false; //Proteggo l'operazione per essere sicuro che sia atomica
		
		} else {
			hitted = false;//Se non sto colpendo hitted per sicurezza chiudo il flag
			agent.AddReward(-0.1f);
		}
	}
}
