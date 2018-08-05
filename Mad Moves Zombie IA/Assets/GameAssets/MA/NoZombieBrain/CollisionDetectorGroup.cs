using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorGroup : MonoBehaviour {
	private bool zombieNear=false;
	private float timer = 0;
	private bool chestNear = false;

	public int mult = 1;

	void Update(){
		if (zombieNear && timer < 30) {
			timer += Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.gameObject.CompareTag ("block") && other.transform.gameObject != gameObject.transform.parent.gameObject) {
			Debug.Log (other.transform.tag);
			zombieNear = true;	
			mult++;
			timer = 0;
		}
	}

	void OnTriggerStay(Collider other){
		if (other.transform.gameObject.CompareTag ("block") && other.transform.gameObject != gameObject.transform.parent.gameObject) {
			Debug.Log (other.transform.tag);
			zombieNear = true;	
		}
	}

	void OnTriggerExit(Collider other){
		if (other.transform.gameObject.CompareTag ("block") && other.transform.gameObject != gameObject.transform.parent.gameObject) {
			zombieNear = false;	
			if (mult > 1) {
				mult--;
			}
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
