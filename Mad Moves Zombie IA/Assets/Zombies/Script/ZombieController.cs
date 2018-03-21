using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	private NavMeshAgent agent;
	private Animator anim;
	private bool isTriggered;
	private float velocity;
	private RaycastHit hit;
	public bool isAttacking;
	private Transform target;


	public Transform pointView;
	public float range;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> (); //NavMeshAgent collegato all'oggetto
		anim = GetComponent<Animator> (); //Animator degli zombie
		isTriggered = false;	//booleano attivato in caso di avvistamento del player
		isAttacking = false;
		Destination();
	}
	
	// Update is called once per frame
	void Update () {


		if (isAttacking) {
			transform.LookAt (target);
		} else if (Physics.Raycast (pointView.position, pointView.forward, out hit, range)) {
			if (hit.transform.gameObject.CompareTag ("Player") && !isAttacking) {
				target = hit.transform;
				agent.SetDestination (target.position);
				if (!isTriggered && !isAttacking) {
					Triggered ();
				}
			}
		}

		if (isTriggered) {
			Triggered ();
		}

	}

	void FixedUpdate(){
		
		setVelocity (agent.velocity.magnitude);

		if (getVelocity() > 0) {

			anim.SetFloat ("Velocity", getVelocity ());

		}


	}

	private void Destination(){
	
		if (GameObject.Find ("First").activeInHierarchy) {
			target = GameObject.Find ("First").transform;
			agent.SetDestination (target.position);
		} else if (GameObject.Find ("Second").activeInHierarchy) {
			target = GameObject.Find ("Second").transform;
			agent.SetDestination (target.position);
		}
	
	}

	private void Attacking(){
		isAttacking = true;
		anim.SetBool ("isAttacking", isAttacking);
	}

	private void Triggered(){
		float dist = Vector3.Distance (transform.position, target.position);

		setTrigg (true);
		anim.SetBool ("isTriggered", getTrigg ());

		if (dist <= 2.0f) {
			setTrigg (false);
			anim.SetBool ("isTriggered", getTrigg ());
			agent.enabled = false;
			Attacking ();
		} else if (dist >= 2.0f) {
			setTrigg (true);
			anim.SetBool ("isTriggered", getTrigg ());
			agent.enabled = true;
		}

	}


	//setter della velocità
	public void setVelocity(float v){
		
		velocity = v;

	}


	//getter della velocità
	public float getVelocity(){
		
		return velocity;

	}


	//setter del trigger Avvistato
	public void setTrigg(bool t){
		
		isTriggered = t;

	}


	//getter del trigger Avvistato
	public bool getTrigg(){
	
		return isTriggered;
	
	}
}
