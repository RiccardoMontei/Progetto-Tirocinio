using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {
	private GameObject gameController;
	private Animator myAnim; //animator dello zombie
	private bool hitted;
	private bool dead;
	private GameObject player;
	private Rigidbody rb;

	public int health;
	public int damage;

	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		player = GameObject.FindGameObjectWithTag ("player");
		myAnim = GetComponent<Animator> ();
		hitted = false;
		dead = false;
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () {
		if (health <= 0 && !dead) {
			rb.isKinematic = true;
			dead = true;
			myAnim.CrossFade ("Died", 0.0f);
			Invoke ("ResetFunction", 3.0f);
		}
	}

	private void ResetFunction(){
		myAnim.CrossFade ("Idle", 0.0f);
		health = 100;
		rb.isKinematic = false;
		dead = false;
		gameController.GetComponent<GameController> ().zombiesInScene--;
		this.gameObject.SetActive (false);


	}

	private void OnTriggerStay( Collider objectHitted ){
		//Se nel trigger davanti lo zombie c'è il player
		if (objectHitted.transform.gameObject.CompareTag ("player")){
			hitted = true; //lo sto colpendo!
		}else{
			hitted=false;//Non lo sto colpendo in qualsiasi altro caso
		}

	}

	public void TryToAttack(){//Funzione evento dell'animazione di attacco, richiamata nel frame in cui la mano è alla massima estensione(Dentro il trigger)
		if (hitted) {//Se sto colpendo nel momento di massima estensione

			player.GetComponentInChildren<VitalityController> ().DecreaseLife (damage);
			hitted=false; //Proteggo l'operazione per essere sicuro che sia atomica

		} else {
			hitted = false;//Se non sto colpendo hitted per sicurezza chiudo il flag
			//agent.AddReward (-0.5f);// Malus alto
		}
	}

	public void TakeDamage(int damage){
		if (health >= 0) {
			health -= damage;
		}
	}
}
