using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDettails : MonoBehaviour {
	public int maxBullets;
	public int bulletsToShot;
	public int damageForBullets;
	public int bulletsStored;
	public float aimFieldOfView;
	public float xPosistionCameraAim;

	// Use this for initialization
	void Start () {
		switch (gameObject.name) {
		case "Ak-47":
			maxBullets = 30; 
			bulletsToShot = 30; 
			damageForBullets = 50;
			bulletsStored = 200;
			aimFieldOfView=0;
			xPosistionCameraAim=0;
			break;

		case "M4A1_Sopmod":
			maxBullets = 30; 
			bulletsToShot=30; 
			damageForBullets=45;
			bulletsStored = 200;
			aimFieldOfView=0;
			xPosistionCameraAim=0;
			break;
		case "UMP-45":
			maxBullets = 20; 
			bulletsToShot = 20;
			damageForBullets = 35;
			bulletsStored = 200;
			aimFieldOfView=0;
			xPosistionCameraAim=0;
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
