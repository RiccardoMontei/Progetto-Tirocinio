using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class VitalityController : MonoBehaviour {
	/*Vedi un pò come gestirlo dalla UI 
	 * nel progetto noi abbiamo un'immagine filled che prende
	 * per esempio: immage.fillamount=life/100
	 * così facendo abbiamo sempre la barra aggiornata*/
	public float life=100;
	private float stamina = 200;
	private int maxParameters=100;
	private int maxParamStamina = 200;

	private int staminaDrop = 20; 

	private float staminaCounter=0.0f;
  
	private int minParameters = 0;

	private FirstPersonController controller;

	public GameObject weapons;

	private WeaponsManager wp;

	public bool isDead=false;//Indica quando il player muore

   

	public void DecreaseLife(float Damage)
	{
		life -= Damage;

	}

	public void Medikit(int mediLife)
	{
		life += mediLife;
	}

	public float getLife()
	{
		return life;
	}

	public float getStamina()
	{
		return stamina;
	}

	void Start () {
		wp = weapons.GetComponent<WeaponsManager> ();
        controller = GetComponent<FirstPersonController> ();
	}
	
	void Update () {
		
		PlayerGrounded ();

		StaminaController ();

		VitaController ();
	}
	private void StaminaController(){

		if (stamina <= minParameters) {//Controllo se ho finito la stamina
            stamina = 0;
			controller.m_RunSpeed = 5.0f; //Riduco la velocità di corsa
			staminaCounter += 0.05f; //Incremento un contatore che mi serve per limitare l'uso della corsa
			if (staminaCounter >= 10.0f) { //Quando il contatore raggiunde un punto adeguato (circa 5 secondi)
				stamina += 1;//Incremento la stamina
				controller.m_RunSpeed = 10.0f;//Risetto la velocità di corsa
				staminaCounter = 0.0f; //Risetto il contatore
			}
		} else {//Se le stamina non è finita
			if (!controller.m_IsWalking ) {// e non sto camminando
				stamina -= Time.deltaTime * staminaDrop;//Decremento la stamina
			} else if (controller.m_IsWalking && stamina > minParameters && stamina <= maxParamStamina) {//Se sto camminando e ho abbastanza stamina
				stamina += Time.deltaTime * staminaDrop;//rigenero stamina
			}
		}

	}

	private void VitaController(){
		if (life <= minParameters) {
			isDead = true;
			wp.animator.SetBool ("Dead", true);
        }
	}
   
	private void PlayerGrounded(){
		

		if (isDead ){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ().enabled = false;//Disattivo il Fps controller

			if (wp.activeWeapon) // e ho un'arma primaria equipaggiata
				wp.yourWeapons [0].gameObject.SetActive (false);//Disattivo l'arma primaria

			if (!wp.activeWeapon) //Se ho un'arma Secondaria
				wp.yourWeapons [1].gameObject.SetActive (false);

			if (transform.localRotation.eulerAngles.x <= 268.0f || transform.localRotation.eulerAngles.x >= 272.0f) {
				//Ruoto e abbasso il player 
				transform.Translate (new Vector3 (0, -0.5f, 0) * Time.deltaTime);
				transform.Rotate (new Vector3 (-30, 0, 0) * Time.deltaTime);
			}
		}
	}
}
	