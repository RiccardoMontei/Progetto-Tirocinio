using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFunctions: MonoBehaviour {
	public bool startShot=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnStartAnimation(){
		startShot = true;

	}
	public void OnEndAnimation(){
		startShot=false;
	}

}
