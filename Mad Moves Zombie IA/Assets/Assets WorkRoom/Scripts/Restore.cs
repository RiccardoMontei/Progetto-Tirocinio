using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Restore : MonoBehaviour {
	public Vector3 randomPosition; //Vettore di posizione randomica

	public GameObject pivot; //Gameobject al centro del ring

	public TrainingConfiguration trainer;

	void Start (){
		trainer= FindObjectOfType<TrainingConfiguration>();
	}

	void Update () {
		//Generazione random di un vettore di posizione
		randomPosition= new Vector3(Random.Range(pivot.transform.position.x - 45,pivot.transform.position.x + 45), pivot.transform.position.y, Random.Range (pivot.transform.position.z - 45,pivot.transform.position.z + 45));
		//Reset della posizione in caso di bug di uscita dalla mappa (solo CPU velocity)
		if (Vector3.Distance (gameObject.transform.position, pivot.transform.position) > 100)
			gameObject.transform.position = randomPosition;
		
	}

	public void OnCollisionEnter(Collision other){
		//Raccolta del prototipo della cassa di rifornimenti
		if(other.gameObject.CompareTag("chest") && !trainer.activeTraining){ //Se non sto addestrando posso raccogliere la chest 
			other.gameObject.transform.position = randomPosition; //Viene  rigenerata in posizione random nella mappa 
		}
		if(other.gameObject.CompareTag("block") && trainer.activeTraining){//Se sto addestrando la pallina svanisce al contatto con il nemici
			other.gameObject.transform.position = randomPosition; //Vengono rigenerati in posizione random nella mappa 
		}

	}
}
