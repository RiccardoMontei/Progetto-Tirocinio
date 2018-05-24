using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore : MonoBehaviour {
	public Vector3 defaultPosition;
	public GameObject pivot;
	// Use this for initialization
	void Start () {
		
		defaultPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		defaultPosition= new Vector3(Random.Range(pivot.transform.position.x - 49,pivot.transform.position.x + 49), pivot.transform.position.y, Random.Range (pivot.transform.position.z - 49,pivot.transform.position.z + 49));
		if (Vector3.Distance (gameObject.transform.position, pivot.transform.position) > 55)
			gameObject.transform.position = defaultPosition;
		
	}
	public void OnCollisionEnter(Collision other){

		if(other.gameObject.CompareTag("block")) {
		//	gameObject.transform.position = defaultPosition;

		}

	}
}
