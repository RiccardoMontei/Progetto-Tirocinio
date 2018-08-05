using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorChest : MonoBehaviour {
	private bool zombieNear=false;
	private float timer = 0;
	private bool chestNear = false;

	public bool deactiveChest = false;

	private DynamicZombieAgent agent;

	public GameObject chest;

	private int timing=10;

	private GameController4MA gameController;

	void Start(){
		agent= gameObject.GetComponentInParent<DynamicZombieAgent> ();
		gameController = FindObjectOfType<GameController4MA> ();
	}
	void Update(){
		
		if(timer> timing){
			gameController.ResetChest (chest);
			timer = 0;
			timing ++;
			Debug.Log (timing);
		}
	}

	void OnTriggerEnter(Collider other){	
	
		if(other.transform.gameObject.CompareTag("chest")){
			chest = other.transform.gameObject;
			timer = 0;
			agent.SetReward (2f);
			agent.Done ();
		}

	}
	void OnTriggerStay(Collider other){	

		if(other.transform.gameObject.CompareTag("chest")){
			chest = other.transform.gameObject;
			agent.AddReward (0.01f);
			timer += Time.deltaTime;
		}

}

	void OnTriggerExit(Collider other){
		if (other.transform.gameObject.CompareTag ("chest")) {
			timer = 0;
			agent.SetReward (-1f);
			agent.Done ();
		}
	}

}
