using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelScript : MonoBehaviour {

	private GameObject player;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("player");
	}
	void Update () {
		transform.LookAt (player.transform);
	}
}
