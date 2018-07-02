using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	[SerializeField] private GameObject player;	//Player principale nella scena
	[SerializeField] private Sprite[] weaponsIcons; //array con le icone delle armi, serve per lo switch
	[SerializeField] private Sprite[] bulletsIcons; //array con le icone dei proiettili

	//barra vita e stamina
	public Image healthBar;		
	public Image staminaBar;

	//icone delle armi e tipo di munizioni corrente
	public Image currentWeaponIcon;
	public Image currentBulletType;

	//nome dell'arma e quantitativo di proiettili
	public Text currentWeaponName;
	public Text currentBulletsCount;

	//valori base per la vita e la stamina da usare per riempire le rispettive barre
	private int maxHealth = 100;
	private float currentHealth;
	private int maxStamina = 200;
	private float currentStamina;


	// allo start imposto i valori massimi
	void Start () {
		currentHealth = maxHealth;
		currentStamina = maxStamina;
	}


	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("player");
		}
		UpdateHealthBar ();
		UpdateStaminaBar ();
		UpdateWeaponIcon ();
		StatsController ();
	}

	//funzione di controllo per aggiornare le icone delle armi correnti
	private void UpdateWeaponIcon(){
		// se è attiva un arma primaria (activeWeapon == true) controllo per cambiare le icone giuste
		if (player.GetComponentInChildren<WeaponsManager> ().activeWeapon) {
			switch (player.GetComponentInChildren<WeaponsManager> ().YourWeapon ().gameObject.name) {
			case "Ak-47":
				currentWeaponIcon.sprite = weaponsIcons [0];
				currentBulletType.sprite = bulletsIcons [0];
				currentWeaponName.text = "AK-47";
				currentBulletsCount.text = "" + player.GetComponentInChildren<WeaponsDettails> ().bulletsToShot + "/" + player.GetComponentInChildren<WeaponsDettails> ().bulletsStored;
				break;
			case "M4A1_Sopmod":
				currentWeaponIcon.sprite = weaponsIcons [1];
				currentBulletType.sprite = bulletsIcons [1];
				currentWeaponName.text = "M4-A1";
				currentBulletsCount.text = "" + player.GetComponentInChildren<WeaponsDettails> ().bulletsToShot + "/" + player.GetComponentInChildren<WeaponsDettails> ().bulletsStored;
				break;
			case "UMP-45":
				currentWeaponIcon.sprite = weaponsIcons [2];
				currentBulletType.sprite = bulletsIcons [2];
				currentWeaponName.text = "UMP-45";
				currentBulletsCount.text = "" + player.GetComponentInChildren<WeaponsDettails> ().bulletsToShot + "/" + player.GetComponentInChildren<WeaponsDettails> ().bulletsStored;
				break;
			}
		//altrimenti controllo per le icone dell'arma secondaria
		} else {
			switch (player.GetComponentInChildren<WeaponsManager> ().YourWeapon ().gameObject.name) {
			case "machete":
				currentWeaponIcon.sprite = weaponsIcons [3];
				currentBulletType.sprite = bulletsIcons [3];
				currentWeaponName.text = "Machete";
				currentBulletsCount.text = "";
				break;
			case "BaseballBat":
				currentWeaponIcon.sprite = weaponsIcons [4];
				currentBulletType.sprite = bulletsIcons [3];
				currentWeaponName.text = "Mazza da baseball";
				currentBulletsCount.text = "";
				break;
			}
		}
	}

	//aggiorna i valori della vita
	private void UpdateHealthBar(){
		currentHealth = player.GetComponent<VitalityController> ().getLife();
		healthBar.fillAmount = currentHealth / maxHealth;
	}

	//aggiorna i valori della stamina
	private void UpdateStaminaBar(){
		currentStamina = player.GetComponent<VitalityController> ().getStamina ();
		staminaBar.fillAmount = currentStamina / maxStamina;
	}

	//controlla che i valori non sforino
	private void StatsController(){
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		if (currentStamina > maxStamina) {
			currentStamina = maxStamina;
		}
	}
}
