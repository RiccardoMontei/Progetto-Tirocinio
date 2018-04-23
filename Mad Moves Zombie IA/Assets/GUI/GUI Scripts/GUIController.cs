using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	[SerializeField] private GameObject player;

	public Image healthBar;
	public Image staminaBar;

	private int maxHealth = 100;
	private float currentHealth;
	private int maxStamina = 200;
	private float currentStamina;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		currentStamina = maxStamina;
	}

	// Update is called once per frame
	void Update () {
		UpdateHealthBar ();
		UpdateStaminaBar ();
		StatsController ();
	}


	private void UpdateHealthBar(){
		currentHealth = player.GetComponent<VitalityController> ().getLife();
		healthBar.fillAmount = currentHealth / maxHealth;
	}

	private void UpdateStaminaBar(){
		currentStamina = player.GetComponent<VitalityController> ().getStamina ();
		float myStamina = currentStamina / maxStamina;
		staminaBar.transform.localScale = new Vector3 (myStamina, staminaBar.transform.localScale.y, staminaBar.transform.localScale.z);
	}

	private void StatsController(){
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		if (currentStamina > maxStamina) {
			currentStamina = maxStamina;
		}
	}
}
