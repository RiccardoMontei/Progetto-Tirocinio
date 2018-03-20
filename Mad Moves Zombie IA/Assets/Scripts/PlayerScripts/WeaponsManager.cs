using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class WeaponsManager : MonoBehaviour {

	//Game Objects e correlati
	public GameObject[] weapons =new GameObject[5];//Vettore delle armi
	public GameObject[] yourWeapons = new GameObject[2];//Vettore delle 2 categorie di armi
	public bool activeWeapon= false; //true arma primaria false arma secondaria

	//Variabili dell'animator
	public Animator animator;
	public AnimatorStateInfo currentState;

	//Variabili di supporto al codice
	public float deltaTime=0;//variabile per sincronizzare alcune parti di codice, che richiedono delle attese
	public int bulletsShotted;//Tiene Conto di quanti proiettili del caricatore ho sparato
	public bool isRecharged = false;//Flag che indica che è stata effettuata una ricarica
	public float stateLeght;
	public bool startShot=false;



	void Start () {
		animator = GameObject.FindGameObjectWithTag("Animator").GetComponent<Animator> ();//Prendo l'animator del player

	}

	void Update () {
		InizializzazioneVariabiliControlli ();
	
		whatIsActiveWeapon ();// Controllo che arma ho in mano cambiando il vettore bool
		WeaponsSelector();//Funzione che setta i flag per le armi nell'animator

		ChangeWeapon ();//Funzione per il cambio arma
		ReloadFunction(); //Funzione per la ricarica dell'arma
	}

	void FixedUpdate(){
		Shot ();
	}
	private void InizializzazioneVariabiliControlli(){
		deltaTime += Time.deltaTime;//Il nostro timer è perennemente incrementato è azzerato all'occorrenza

		stateLeght = currentState.length;
		currentState= animator.GetCurrentAnimatorStateInfo(0);
		startShot = GameObject.FindGameObjectWithTag ("Animator").GetComponent<ShotFunctions> ().startShot;

		if(Input.GetButtonUp("Fire1")) animator.SetBool("IsShotting",false);//Se sollevo il dito disattivo l'animazione
		if (activeWeapon && YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot == 0) animator.SetBool ("IsShotting", false); //Se finisco i proiettili disattivo l'animazione


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
		bulletsShotted=YourWeapon().GetComponent<WeaponsDettails>().maxBullets-YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot;
		//Se premo r
		if (Input.GetKey("r") && 
			YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot < YourWeapon().GetComponent<WeaponsDettails>().maxBullets && // e ho meno proiettili del massimo nel caricatore
			YourWeapon().GetComponent<WeaponsDettails>().bulletsStored != 0 && // e ho proiettili conservati
			!animator.GetBool("IsShotting") && //e non sto sparando
			!animator.GetBool("IsReloading")) { // e non sto gia ricaricando
			deltaTime = 0; //azzero il timer di sistema
			isRecharged = true; // setto il flag in ricarica
			animator.SetBool ("IsReloading", true);//Avvio L'animazione della ricarica

			if(bulletsShotted <= YourWeapon().GetComponent<WeaponsDettails>().bulletsStored){
				YourWeapon().GetComponent<WeaponsDettails>().bulletsStored -= bulletsShotted;//Diminuzione proiettili totali
				YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot +=bulletsShotted;//Incremento caricatore
			}
			else if(bulletsShotted > YourWeapon().GetComponent<WeaponsDettails>().bulletsStored){
				bulletsShotted = YourWeapon ().GetComponent<WeaponsDettails> ().bulletsStored;
				YourWeapon().GetComponent<WeaponsDettails>().bulletsStored -= bulletsShotted;//Diminuzione proiettili totali
				YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot +=bulletsShotted;//Incremento caricatore
			}
		}
		if(deltaTime >= stateLeght-0.05 && isRecharged){
			animator.SetBool ("IsReloading", false);
			isRecharged=false;
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
		GameObject Fire = GameObject.FindGameObjectWithTag ("Fire");//Cerco il fire dell'arma 
		RaycastHit hit; //Parametro che contiene info sull'oggetto colpito
		if (activeWeapon) {
			if (Input.GetButton ("Fire1") && YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot > 0 && !startShot) {//Se sparo e ho proiettili nel caricatore
				if (YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot != 0 ){
					animator.SetBool ("IsShotting", true); //Se non ho il caricatore vuoto
				}
			}
		} else if (!activeWeapon) { //Con l'arma secondaria
			if (Input.GetButton ("Fire1"))
				animator.SetBool ("IsShotting", true);
		}
		if (currentState.IsTag ("Shot")) {
			if (deltaTime >= stateLeght) {//Diminuzione proiettili in base all'animazione
				deltaTime = 0;
				YourWeapon ().GetComponent<WeaponsDettails> ().bulletsToShot--;//Diminuzione proiettili
				Debug.DrawRay (Fire.transform.position, Fire.transform.forward * 300, Color.black);//Debug che mostra il raggio del raycast
				if (Physics.Raycast (Fire.transform.position, Fire.transform.forward, out hit, 300)) {//Se il raggio colpisce qualcosa!
					if (hit.transform.gameObject.CompareTag ("Zombie")) {//Ed è uno zombie (ancora da settare, da errore nell'editor)
						//Logica sugli zombie
					}
				}
			}
		}  
		if (!currentState.IsTag ("Shot")) {
			startShot = false;
		}
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
	private GameObject YourWeapon(){
		if (activeWeapon) {
			return yourWeapons [0];
		} else if (!activeWeapon) {
			return yourWeapons [1];
		}
		return gameObject;
	}

}
