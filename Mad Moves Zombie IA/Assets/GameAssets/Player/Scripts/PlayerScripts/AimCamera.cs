using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;



public class AimCamera : MonoBehaviour {

	public GameObject theCamera;
	public GameObject player;
	public GameObject wP;

	private Vector3 cameraRotation;
	private Transform cameraPosition;

	private Camera cameraOBJ;

	private FirstPersonController controller;

	private bool isAiming;
	public Vector3 pointOfViewAimWeapon;
	public float fieldOfViewAimWeapon=0;

	// Use this for initialization
	void Start () {
		cameraOBJ=Camera.main;
		cameraPosition = theCamera.GetComponent<Transform> ();
		controller = player.GetComponent<FirstPersonController> ();//Prendo il FPC del player su controller


	}

	// Update is called once per frame
	void Update () {
		pointOfViewAimWeapon = wP.GetComponent<WeaponsManager>().YourWeapon().GetComponent<WeaponsDettails>() .pointOfViewAimWeapon;
		fieldOfViewAimWeapon = wP.GetComponent<WeaponsManager>().YourWeapon().GetComponent<WeaponsDettails>().fieldOfViewAimWeapon;

		cameraPosition = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> (); 
		cameraRotation = cameraPosition.rotation.eulerAngles;
		cameraRotation = new Vector3 (-cameraRotation.x, cameraRotation.y + 180, cameraRotation.z); //Variabile che contiene la rotazione della camera modificata ad hoc per il nostro player

		transform.rotation = Quaternion.Euler (cameraRotation);//Ruoto sull'asse verticale il body del player in base al movimento della camera
		// if per forzare il fieldOfView della camera che crea problemi mentre si corre
		if(cameraOBJ.fieldOfView!=53.0f && !isAiming) 
			cameraOBJ.fieldOfView = 53.0f;

		if (cameraOBJ.fieldOfView != fieldOfViewAimWeapon && isAiming)
			cameraOBJ.fieldOfView = fieldOfViewAimWeapon;

		/* Funzioni per la mira il valore di spostamento cambia in base all'arma che si impugna*/
		if (cameraOBJ.fieldOfView == 53.0f) { 
			if (!wP.GetComponent<WeaponsManager> ().animator.GetBool ("BaseballBat") && !wP.GetComponent<WeaponsManager> ().animator.GetBool ("Machete")) {
				if (Input.GetButtonDown ("Aim") && !GameObject.FindGameObjectWithTag ("CraftingSystem").GetComponent<CraftingSystem> ().activeCrafting) {
					isAiming = true;
					controller.m_RunSpeed = 5.0f;//Quando si mira non si corre, classico degli FPS
					cameraOBJ.fieldOfView = fieldOfViewAimWeapon;
					transform.localPosition = pointOfViewAimWeapon;
				}
			}
		}

		if (Input.GetButtonUp ("Aim")) {
			//Torno alle impostazioni di default
			cameraOBJ.fieldOfView = 53.0f;
			transform.localPosition = new Vector3 (0.07f, -0.2f, -0.13f); //posizione di default
			controller.m_RunSpeed = 10.0f;
			isAiming=false;
		}

		/**************************************************************************************************/

	}
}
