using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	void OnTriggerStay(Collider other){
		if (!(GetComponentInParent<ZombieController> ().isTriggered)) {
			if (other.gameObject.CompareTag ("Player")) {
				GetComponentInParent<ZombieController> ().TargetingSystem (other.transform);
			}
		}
	}
}
