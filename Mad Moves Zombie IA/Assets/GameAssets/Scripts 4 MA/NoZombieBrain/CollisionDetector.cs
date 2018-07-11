using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {
	private bool zombieNear=false;
	private float timer = 0;

	void Update(){
		if (zombieNear) {
			timer += Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.gameObject.CompareTag ("block") && other.transform.gameObject != gameObject.transform.parent.gameObject) {
			Debug.Log (other.transform.tag);
			zombieNear = true;	
			timer = 0;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.transform.gameObject.CompareTag ("block") && other.transform.gameObject != gameObject.transform.parent.gameObject) {
			zombieNear = false;	
			timer = 0;
		}
	}

	public float getTimer(){
		return timer;
	}

	public void resetTimer(){
		timer = 0;
	}
}
