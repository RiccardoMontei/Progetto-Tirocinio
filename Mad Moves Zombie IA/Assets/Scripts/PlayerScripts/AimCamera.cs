using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;



public class AimCamera : MonoBehaviour {
	
	public GameObject theCamera;
	public GameObject player;

	private Vector3 cameraRotation;
	private Transform cameraPosition;

	private Camera cameraOBJ;

	private FirstPersonController controller;

	private bool isAiming;
	public float pointOfViewAimWeapon; //Variabile che inseriremo nel vector3 della local position in base all'arma

	// Use this for initialization
	void Start () {
		cameraOBJ=Camera.main;
		cameraPosition = theCamera.GetComponent<Transform> ();
		controller = player.GetComponent<FirstPersonController> ();//Prendo il FPC del player su controller

	
	}
	
	// Update is called once per frame
	void Update () {
		cameraPosition = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> (); 
		cameraRotation = cameraPosition.rotation.eulerAngles;
		cameraRotation = new Vector3 (-cameraRotation.x, cameraRotation.y + 180, cameraRotation.z); //Variabile che contiene la rotazione della camera modificata ad hoc per il nostro player
		transform.rotation = Quaternion.Euler (cameraRotation);//Ruoto sull'asse verticale il body del player in base al movimento della camera
		//2 if per forzare il fieldOfView della camera che crea problemi mentre si corre
		if(cameraOBJ.fieldOfView!=53.0f && !isAiming) 
			cameraOBJ.fieldOfView = 53.0f;

		if (cameraOBJ.fieldOfView != 12.0f && isAiming)
			cameraOBJ.fieldOfView = 12.0f;

		/* Funzioni per la mira il valore di spostamento cambia in base all'arma che si impugna*/
		if (cameraOBJ.fieldOfView == 53.0f) { 

			if (Input.GetButtonDown ("Aim")) {
				isAiming=true;
				controller.m_RunSpeed = 5.0f;//Quando si mira non si corre, classico degli FPS
				cameraOBJ.fieldOfView = 12.0f;
				cameraPosition.localPosition = new Vector3 (0.07f, 0.78f, 0.0f);//Imposto staticamente la posizione di mira, cambia in base all'arma(Float PointOfViewAimWeapon)
			}
		}

		if (Input.GetButtonUp ("Aim")) {
			//Torno alle impostazioni di default
			cameraOBJ.fieldOfView = 53.0f;
			cameraPosition.localPosition = new Vector3 (0.0f, 0.78f, 0.0f); 
			controller.m_RunSpeed = 10.0f;
			isAiming=false;
		}
		
		/**************************************************************************************************/
	
	}
}
