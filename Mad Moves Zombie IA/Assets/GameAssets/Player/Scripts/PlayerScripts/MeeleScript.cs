using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleScript : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		switch (other.gameObject.tag) {
		case "block":
			other.gameObject.GetComponent<ZombieScript> ().TakeDamage (GetComponent<WeaponsDettails> ().damageForBullets);
			break;
		case "fence":
			other.gameObject.GetComponent<FenceLife> ().DecreaseLife (GetComponent<WeaponsDettails> ().damageForBullets);
			break;
		}
	}

	

}
