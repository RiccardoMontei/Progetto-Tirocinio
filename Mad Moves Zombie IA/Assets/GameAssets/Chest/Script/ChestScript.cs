using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

	private bool used;
	private Animation anim;

	public GameObject resources;
	public GameObject label;
	public AnimationClip open;


	// Use this for initialization
	void Start () {
		used = false;
		anim = GetComponent<Animation> ();
		HideLabel ();
	}

	private void ShowLabel(){
		if (!used) {
			label.SetActive (true);
		} else {
			label.SetActive (false);
		}
	}

	private void HideLabel(){
		label.SetActive (false);
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag ("player")) {
			if (!used) {
				ShowLabel ();
			} else {
				HideLabel ();
			}
			if (Input.GetKeyDown (KeyCode.E) && !used) {
				anim.Play (anim.clip.name);
				Instantiate (resources, transform.position + new Vector3(Random.Range(0f, 5f), 0.0f, Random.Range(0f, 5f)) , transform.rotation);
				Instantiate (resources, transform.position + new Vector3(Random.Range(0f, 5f), 0.0f, Random.Range(0f, 5f)) , transform.rotation);
				Instantiate (resources, transform.position + new Vector3(Random.Range(0f, 5f), 0.0f, Random.Range(0f, 5f)) , transform.rotation);
				other.gameObject.GetComponentInChildren<WeaponsDettails> ().bulletsStored += Random.Range (30, 120);
				other.gameObject.GetComponentInChildren<VitalityController> ().medikit += Random.Range (0, 3);
				used = true;
				Invoke ("KillMe", 5.0f);
			}
		}
	}

	void OnTriggerExit(Collider other){
		HideLabel ();
	}

	private void KillMe(){
		Destroy (this.gameObject);
	}

}
