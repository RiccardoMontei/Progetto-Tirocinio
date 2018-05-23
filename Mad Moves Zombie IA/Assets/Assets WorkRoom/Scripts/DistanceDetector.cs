using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetector : MonoBehaviour {

	public ZombiesAgent agent;
	private Vector3 vector0;
	private Vector3 defaultPosition;
	public GameObject pivot;
	void Awake(){
		agent = GetComponent<ZombiesAgent> ();
		vector0 = Vector3.zero;
	}
	void Start(){
	 	defaultPosition = gameObject.transform.position;
	}
	// Update is called once per frame
	void Update () {
		defaultPosition= new Vector3(Random.Range(pivot.transform.position.x - 24,pivot.transform.position.x + 24), pivot.transform.position.y, Random.Range (pivot.transform.position.z - 24,pivot.transform.position.z + 24));
		if (Vector3.Distance (gameObject.transform.position, pivot.transform.position) > 26)
			gameObject.transform.position = defaultPosition;

	
	}
	public void OnCollisionEnter(Collision other){

		 if(other.gameObject.CompareTag("player")) {
			agent.SetReward (1f);
			agent.Done ();

			gameObject.transform.position = defaultPosition;

		}
	
	}
}
