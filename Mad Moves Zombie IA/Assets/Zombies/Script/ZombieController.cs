using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	//variabili private di oggetti collegati all npc
	private Animator anim;
	private NavMeshAgent agent;
	private bool playerFound;

	//variabili pubbliche da inspector
	public Transform target; //transform destinato al player quando avvistato
	public float velocity; //velocità dell'agente, pubblico per controllare lo stato da inspector
	public bool isTriggered; //lo zombie ha avvistato il player?
	public float rad = 5.0f; //raggio di ricerca della nuova posizione quando in stato onRest
	public float range= 50.0f; //raggio di avvistamento del player;
	public float speedRest = 0.3f;
	public float speedHunt = 0.5f;
	public Vector3 nextDest = Vector3.zero;
	public float playerTooFar;
	public int lifePoints;
	private int damageForHit=100;



	void Start(){
		playerFound = false;
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (newDestination ());
	}

	void Update(){
		CheckHealth ();
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
		if (Vector3.Distance (transform.position, target.position) <= 1.8f) {
			agent.enabled = false;
			Vector3 lookingTarget = new Vector3 (target.position.x, transform.position.y, target.position.z);
			transform.LookAt (lookingTarget);
			anim.CrossFade ("Attack", 0f);
		} else {
			if (agent.enabled == false) {
				agent.enabled = true;
			}
			agent.destination = target.position;
		}
		if (Vector3.Distance(transform.position, target.position) >= playerTooFar) {
			target = null;
			isTriggered = false;
			agent.SetDestination (newDestination ());
		}
		if (Vector3.Distance (transform.position, target.position) <= 10.2f && !playerFound) {
			anim.CrossFade ("Scream", 0.0f);
			playerFound = true;
		}
	}

	public void TargetingSystem(Transform tag){
				target = tag;
				isTriggered = true;
				agent.destination = target.position;
				agent.speed = speedHunt;
	}

	public void Hit(int damage){
		lifePoints -= damage;
	}

	private void CheckHealth(){
		if (lifePoints <= 0) {
			agent.enabled = false;
			anim.CrossFade ("Death", 0.0f);
			Invoke ("Suicide", 5.0f);
		}
	}

	private void Suicide(){
		Destroy (gameObject);
	}

	private void TryToAttack(){
		if (Vector3.Distance (transform.position, target.position) <= 1.8f) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<VitalityController> ().DecreaseLife (damageForHit);
		}
	}

}
