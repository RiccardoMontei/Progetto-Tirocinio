using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private float speed=20f;//Variabile usata per l'intensità della forza
	private Rigidbody rb;

    void Start (){
  		rb = GetComponent<Rigidbody>(); //oggetto per rigid body (fisica)

    }
	// Update is called once per frame
    void FixedUpdate (){
		
	/*	TODO MOVIMENTO RANDOM
	 * float movementHorizontal = Random.Range(-1.0f, 1.0f);
		float movementVertical = Random.Range(-1.0f, 1.0f);
		//Vettore del movimento
		Vector3 movemnet = new Vector3(movementHorizontal, 0.0f, movementVertical);
		//Applicazione della forza
		rb.AddForce(movemnet * speed);
*/   
        // TODO MOVIMENTO PLAYER UMANO
        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");
        //Vettore del movimento
        Vector3 movemnet = new Vector3(movementHorizontal, 0.0f, movementVertical);
        //prende la pressione di "space" tramite 
       
        rb.AddForce(movemnet*speed);     
     }
}
