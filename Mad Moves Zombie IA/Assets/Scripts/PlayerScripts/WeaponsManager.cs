using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class WeaponsManager : MonoBehaviour {

	//Game Objects e correlati
	public GameObject[] weapons =new GameObject[5];//Vettore delle armi
	public GameObject[] yourWeapons = new GameObject[2];//Vettore delle 2 categorie di armi
	private bool activeWeapon= false; //true arma primaria false arma secondaria

	//Variabili dell'animator
	public Animator animator;
	
    //Variabili di supporto al codice
	public int bulletsShotted;//Tiene Conto di quanti proiettili del caricatore ho sparato

    void Start () {
		animator = GameObject.FindGameObjectWithTag("Animator").GetComponent<Animator> ();//Prendo l'animator del player
    }

	void Update () {
		whatIsActiveWeapon ();// Controllo che arma ho in mano cambiando il vettore bool
		WeaponsSelector();//Funzione che setta i flag per le armi nell'animator

        Shot();
        ChangeWeapon ();//Funzione per il cambio arma
		ReloadFunction(); //Funzione per la ricarica dell'arma
	}

    private void whatIsActiveWeapon(){
		//Controlla che arma del vettore sia attiva
		if (yourWeapons [0].activeSelf && yourWeapons [0].CompareTag ("PrimaryWeapon")) {
			activeWeapon = true;
		} else if (yourWeapons [1].activeSelf && yourWeapons [1].CompareTag ("Knife")) {
			activeWeapon = false;
		}

	}
	private void ReloadFunction(){
		bulletsShotted=YourWeapon().GetComponent<WeaponsDettails>().maxBullets-YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot; //Proiettili sparati
		
		if (Input.GetKey("r") && 
			YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot < YourWeapon().GetComponent<WeaponsDettails>().maxBullets && // e ho meno proiettili del massimo nel caricatore
			YourWeapon().GetComponent<WeaponsDettails>().bulletsStored != 0 && // e ho proiettili conservati
			!animator.GetBool("IsShotting") && //e non sto sparando
			!animator.GetBool("IsReloading")) { // e non sto gia ricaricando

            animator.SetBool ("IsReloading", true);//Avvio L'animazione della ricarica
        }
	}

	private void ChangeWeapon(){
		if (Input.GetButtonDown ("ChangeWeapon") && !animator.GetBool("IsReloading") && !animator.GetBool("IsShotting")) {// Se premo ALT e non sto ricaricando o sparando
			if (activeWeapon) {// e ho un'arma primaria equipaggiata
				yourWeapons [0].gameObject.SetActive (false);//Disattivo l'arma primaria
				yourWeapons [1].gameObject.SetActive (true);//Attivo la secondaria
			} else if (!activeWeapon) {//Se ho un'arma corpo a corpo
				yourWeapons [1].gameObject.SetActive (false);
				yourWeapons [0].gameObject.SetActive (true);//Attivo la primaria
			}
		}
	}

    private void Shot(){
		
		if (activeWeapon) {
			if (Input.GetButton ("Fire1") && YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot >= 0) {//Se sparo e ho proiettili nel caricatore
				animator.SetBool ("IsShotting", true); //Se non ho il caricatore vuoto
			}
		} else if (!activeWeapon) { //Con l'arma secondaria
			if (Input.GetButton ("Fire1"))
				animator.SetBool ("IsShotting", true);
		}
        if(Input.GetButtonUp("Fire1")) animator.SetBool("IsShotting", false);//Se sollevo il dito disattivo l'animazione
        if (activeWeapon && YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot == 0) animator.SetBool("IsShotting", false); //Se finisco i proiettili disattivo l'animazione
    } 	


	//Funzione che setta tutti i tag delle armi false
	private void SetAllAnimatorBoolFalse(){
		animator.SetBool ("AK47", false);
		animator.SetBool ("M4A1", false);
		animator.SetBool ("UMP5", false);
		animator.SetBool ("Machete", false);
		animator.SetBool ("BaseBallBat", false);
	}

	private void WeaponsSelector(){
		//Ogni frame viene fatto un controllo per capire che arma si sta usando ed impostare la giusta animazione
		switch (YourWeapon ().gameObject.name) {
			case "Ak-47":
				SetAllAnimatorBoolFalse ();
				animator.SetBool ("AK47", true);
					break;
			case "M4A1_Sopmod":
				SetAllAnimatorBoolFalse ();
				animator.SetBool ("M4A1", true);
					break;
			case "UMP-45":
				SetAllAnimatorBoolFalse ();
				animator.SetBool ("UMP5", true);
					break;
			case "machete":
				SetAllAnimatorBoolFalse ();
				animator.SetBool ("Machete", true);
					break;
			case "BaseBallBat":
				SetAllAnimatorBoolFalse ();
				animator.SetBool ("BaseballBat", true);
					break;
		}
	
	}
	//Funzione d'appoggio che restituisce l'arma che si ha in mano 
	public GameObject YourWeapon(){
		if (activeWeapon) {
			return yourWeapons [0];
		} else if (!activeWeapon) {
			return yourWeapons [1];
		}
		return gameObject;
	}

}
