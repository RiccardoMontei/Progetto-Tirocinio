using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	//variabili private di oggetti collegati all npc
	private Animator anim;
	private NavMeshAgent agent;

	//variabili pubbliche da inspector
	public Transform target; //transform destinato al player quando avvistato
	public Transform eyesPoint; //punto da cui parte il raycast
	public GameObject viewField; //campo visivo dello zombie;
	public float velocity; //velocità dell'agente, pubblico per controllare lo stato da inspector
	public bool isTriggered; //lo zombie ha avvistato il player?
	public float rad = 5.0f; //raggio di ricerca della nuova posizione quando in stato onRest
	public float range= 50.0f; //raggio di avvistamento del player;
	public float speedRest = 0.3f;
	public float speedHunt = 0.5f;
	public Vector3 nextDest = Vector3.zero;


	void Start(){
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (newDestination ());
	}

	void Update(){

		if (!isTriggered) {
			OnRest ();
		} else {
			OnHunt ();
		}

	}


	//funzione per il calcolo di un nuovo punto sulla navMesh
	private Vector3 newDestination(){

		Vector3 randomDir = Random.insideUnitSphere * rad; //vettore direzione random
		randomDir += transform.position; //unisco alla attuale posizione

		NavMeshHit hit; //oggetto tipo "hit" di raycast, con la funzione sampleposition, questo oggetto servirà per restituire la posizione raggiungibile random generata

		if (NavMesh.SamplePosition (randomDir,out hit, rad, 1)) {
			nextDest = hit.position; //assegno la nuova posizione da raggiungere
		}

		return nextDest;
	}

	private void OnRest(){
		agent.speed = speedRest;
		if (agent.stoppingDistance < agent.remainingDistance && agent.hasPath) {
			velocity = agent.velocity.magnitude;
			anim.SetFloat ("Velocity", agent.velocity.magnitude);
		} else if (target == null && !agent.hasPath) {
			anim.SetFloat ("Velocity", 0.0f);
			agent.SetDestination (newDestination ());
		}

		if (agent.stoppingDistance >= agent.remainingDistance) {
			agent.SetDestination (newDestination ());
		}
	}

	private void OnHunt(){
		velocity = agent.velocity.magnitude;
		anim.SetFloat ("Velocity", velocity);
		if (agent.stoppingDistance > agent.remainingDistance && Vector3.Distance (transform.position, target.position) < 2.0f) {
			Vector3 lookingTarget = new Vector3 (target.position.x, transform.position.y, target.position.z);
			transform.LookAt (lookingTarget);
			anim.CrossFade ("Attack", 0f);
		} else {
			agent.destination = target.position;
		}
		if (Vector3.Distance(transform.position, target.position) >= 30f) {
			target = null;
			isTriggered = false;
			agent.SetDestination (newDestination ());
		}
	}

	public void TargetingSystem(Transform tag){
	
		transform.LookAt (tag.position);
		RaycastHit hit;

		if (Physics.Raycast (eyesPoint.position, eyesPoint.forward, out hit, range)) {
			if (hit.transform.gameObject.CompareTag ("Player")) {
				target = tag;
				agent.isStopped = true;
				agent.speed = 0f;
				anim.CrossFade ("Scream", 0f);
				agent.destination = target.position;
				agent.speed = speedHunt;
				isTriggered = true;
				agent.isStopped = false;

			}
		}

	}

}
