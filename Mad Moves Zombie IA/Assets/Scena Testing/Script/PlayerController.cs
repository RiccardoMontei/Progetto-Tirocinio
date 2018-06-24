using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private float speed=20f;//Variabile usata per l'intensità della forza

	private Rigidbody rb;

	public TrainingConfiguration trainer;


    void Start (){
  		rb = GetComponent<Rigidbody>(); //oggetto per rigid body (fisica)
	
		trainer= FindObjectOfType<TrainingConfiguration>();

		if (!trainer.activeTraining)
			rb.isKinematic = false;
		else
			rb.isKinematic = true;
	}
	// Update is called once per frame
    void FixedUpdate (){
		
		if (!trainer.activeTraining) {//Se non sto addestrando posso muovere la pallina
			
			float movementHorizontal = Input.GetAxis ("Horizontal");
			float movementVertical = Input.GetAxis ("Vertical");
			//Vettore del movimento
			Vector3 movemnet = new Vector3 (movementHorizontal, 0.0f, movementVertical);
			rb.AddForce (movemnet * speed);   
		}
           
     }
}