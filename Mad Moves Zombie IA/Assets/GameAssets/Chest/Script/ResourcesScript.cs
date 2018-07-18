using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScript : MonoBehaviour {

	private Rigidbody rb;


	void Start(){
		rb = GetComponent<Rigidbody> ();

		rb.AddForce (new Vector3 (Random.Range (1f, 5f), Random.Range (1f, 1.5f), Random.Range (1f, 5f)), ForceMode.Impulse);
	}

	void Update(){
		this.transform.Rotate (new Vector3(0f, 45f, 0f) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("player")) {
			other.gameObject.GetComponentInChildren<CraftingSystem> ().wood += Random.Range (1, 8);
			Debug.Log (other.gameObject.GetComponentInChildren<CraftingSystem> ().wood);
			other.gameObject.GetComponentInChildren<CraftingSystem> ().rope += Random.Range (1, 6);
			Debug.Log (other.gameObject.GetComponentInChildren<CraftingSystem> ().rope);
			KillMe ();
		}
	}

	private void KillMe(){
		Destroy (this.gameObject);
	}
	

}
