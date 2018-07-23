using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDettails : MonoBehaviour {
	public int maxBullets;
	public int bulletsToShot;
	public int damageForBullets;
	public int bulletsStored;
	public float fieldOfViewAimWeapon;
	public Vector3 pointOfViewAimWeapon;


	// Use this for initialization
	void Start () {
		switch (gameObject.name) {
		case "Ak-47":
			maxBullets = 30; 
			bulletsToShot = 30; 
			damageForBullets = 50;
			bulletsStored = 200;
			fieldOfViewAimWeapon = 40;
			pointOfViewAimWeapon = new Vector3 (-0.06f, -0.18f, -0.13f);
			break;

		case "M4A1_Sopmod":
			maxBullets = 30; 
			bulletsToShot=30; 
			damageForBullets=45;
			bulletsStored = 200;
			fieldOfViewAimWeapon = 40;
			pointOfViewAimWeapon = new Vector3(-0.095f, -0.18f, -0.13f);
			break;
		case "UMP-45":
			maxBullets = 20; 
			bulletsToShot = 20;
			damageForBullets = 35;
			bulletsStored = 200;
			fieldOfViewAimWeapon = 40;
			pointOfViewAimWeapon = new Vector3(-0.063f, -0.085f, -0.13f);
			break;
		case "machete":
			damageForBullets = 20;
			break;
		case "BaseballBat":
			damageForBullets = 20;
			break;
		}
	}
	

}
