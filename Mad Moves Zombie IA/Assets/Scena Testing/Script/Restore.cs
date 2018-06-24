using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Restore : MonoBehaviour {
	public Vector3 randomPosition; //Vettore di posizione randomica

	public GameObject pivot; //Gameobject al centro del ring
	private int score1;

	//public Text score;
	public TrainingConfiguration trainer;

	void Start (){
		trainer= FindObjectOfType<TrainingConfiguration>();
	}

	void Update () {
		//	score.text= score1.ToString();
		//Generazione random di un vettore di posizione
		randomPosition= new Vector3(Random.Range(pivot.transform.position.x - 45,pivot.transform.position.x + 45), pivot.transform.position.y+1.5f, Random.Range (pivot.transform.position.z - 45,pivot.transform.position.z + 45));
		//Reset della posizione in caso di bug di uscita dalla mappa (solo CPU velocity)
		if (Vector3.Distance (gameObject.transform.position, pivot.transform.position) > 100)
			gameObject.transform.position = randomPosition;
		
	}

	public void OnCollisionEnter(Collision other){
		//Raccolta del prototipo della cassa di rifornimenti
		if(other.gameObject.CompareTag("chest") && !trainer.activeTraining){ //Se non sto addestrando posso raccogliere la chest 
			other.gameObject.transform.position = randomPosition; //Viene  rigenerata in posizione random nella mappa
			score1++;

		}
		/*if(other.gameObject.CompareTag("block") && ){ // posso essere mangiato dai cubi
			gameObject.transform.position = randomPosition; //Viene  rigenerata la pallina  in posizione random nella mappa 
			score1--;
		}*/

	

	}
}
