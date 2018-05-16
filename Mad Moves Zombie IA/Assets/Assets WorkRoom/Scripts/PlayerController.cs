using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed;//Variabile usata per l'intensità della forza
	private Rigidbody rb;

    void Start (){
  		rb = GetComponent<Rigidbody>(); //oggetto per rigid body (fisica)
       
    }
	// Update is called once per frame
    void FixedUpdate (){
        // prende il movimento tramite stringhe standard di Unity
        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");
        //Vettore del movimento
        Vector3 movemnet = new Vector3(movementHorizontal, 0.0f, movementVertical);
        //prende la pressione di "space" tramite
       
        rb.AddForce(movemnet*speed);
     }
}
