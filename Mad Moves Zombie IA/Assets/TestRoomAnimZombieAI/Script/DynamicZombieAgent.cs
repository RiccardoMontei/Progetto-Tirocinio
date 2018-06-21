using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicZombieAgent : Agent {

	Animator animator;

	private int config = 0;//Determina che cervello si sta usando (default No Zombie Brain)
	private bool isBrainSwitched = false; //Booleano di controllo sul cambiamento di cervello (protezione del cambio)
	private float timer=0;

	public Brain ZombieBrain;

	public Brain ZombieBrainChest;

	public Brain NoZombieBrain;

	public GameObject area; //Game area
	public GameObject player; //The player
	public GameObject bounds;//Muri della mappa
	public GameObject chest;//Chest di rifornimento



	RayPerception rayPer;
	DynamicZombieAccademy academy;


	private Rigidbody zombieRB;  //cached on initialization
	private Rigidbody playerRB;  //cached on initialization

	public TrainingConfiguration trainer; //Contiene le info su cosa si stia facendo, attaccato all'academy

	/* Per addestrare un cervello basta andare su "Acaemy" nell'inspector e selezionare il flag di addestramento true più il flag di quale cervello si voglia addestrare, 
	 * tutte le porzioni di codice adibite ad addestramento hanno il controllo su quei flag, e anche la selezione del cervello lo ha.*/

	void Start (){
		academy = FindObjectOfType<DynamicZombieAccademy> ();
		trainer= FindObjectOfType<TrainingConfiguration>();

		if (trainer.zombieBrainTrainer)
			brain = ZombieBrain;

		if (trainer.noZombieBrainTrainer)
			brain = NoZombieBrain;

		if (trainer.zombieBrainChestTrainer)
				brain = ZombieBrainChest;
		
	}
		

	public override void InitializeAgent()
	{
		base.InitializeAgent ();
		zombieRB = GetComponent <Rigidbody> ();
		playerRB = player.GetComponent<Rigidbody> ();
		rayPer = GetComponent<RayPerception> ();
		animator = GetComponent<Animator> ();
	}

	public override void CollectObservations ()
	{
		float rayDistance = 30f;
		float[] rayAngles = { -20f, 0f, 20f, 40f, 60f, 80f,100f, 120f ,140f, 160f, 180f, 200f };
		string[] detectableObjects;

		detectableObjects = new string[] { "player", "wall", "block","chest" };
		//Vector3 localVelocity = transform.InverseTransformDirection(zombieRB.velocity);

		//AddVectorObs(localVelocity.x);
		//AddVectorObs(localVelocity.z);
		AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
		AddVectorObs((float)GetStepCount() / (float)agentParameters.maxStep);
		//AddVectorObs (transform.position.x);
		//AddVectorObs (transform.position.z);
		//AddVectorObs (Mathf.Abs (transform.position.x - area.transform.position.x));
		//AddVectorObs (Mathf.Abs (transform.position.z - area.transform.position.z));
		//AddVectorObs (transform.forward);
	}


	public void MoveAgent(float[] act)
	{

		Vector3 dirToGo = Vector3.zero;
		Vector3 rotateDir = Vector3.zero;

		int action = Mathf.FloorToInt(act[0]);

		switch (action) {
		case 0:
			animator.SetBool ("Stay", true);
			animator.SetBool ("Walk", false);
			animator.SetBool ("Run", false);
			break;
		case 1:
			animator.SetBool ("Stay", false);
			animator.SetBool ("Walk", true);
			animator.SetBool ("Run", false);
			break;
		case 2:
			animator.SetBool ("Stay", false);
			animator.SetBool ("Walk", false);
			animator.SetBool ("Run", true);
			break;
		case 3:
			rotateDir = transform.up * 1f;
			break;
		case 4:
			rotateDir = transform.up * -1f;
			break;
		}
		transform.Rotate (rotateDir, Time.fixedDeltaTime * 150f);

		/*if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous) {
			dirToGo = transform.forward * Mathf.Clamp (act [0], -1f, 1f);
			rotateDir = transform.up * Mathf.Clamp (act [1], -1f, 1f);
		} else {

			switch (action) {
			case 0:
				dirToGo = transform.forward * 1f;
				break;
			case 1:
				rotateDir = transform.up * 1f;
				break;
			case 2:
				rotateDir = transform.up * -1f;
				break;
			}

			transform.Rotate (rotateDir, Time.fixedDeltaTime * 150f);
			zombieRB.AddForce (dirToGo * academy.agentRunSpeed,
				ForceMode.VelocityChange);

			animator.SetFloat("Velocity", zombieRB.velocity.magnitude);//Setto il float dell'animator velocity
		}*/
	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		AddReward(-2f / agentParameters.maxStep);//Assegno un malus per incentivare la velocità di esecuzione
		MoveAgent(vectorAction);
	}

	void update(){
		timer += Time.deltaTime;//Incremento il timer

	}

	void FixedUpdate(){

		GameObject hit = rayPer.objectObserved; //Oggetto osservato dall'agente

		if(hit!= null)

		if (!trainer.activeTraining && hit !=null) {//Se non sto addestrando attivo lo switch dei cervelli

			if ((config == 0 || config == 2) ) { //Se sono in No zombie brain o in Zombie Brain Chest(priorità al player)
				if (hit.CompareTag ("player")) { // e vedo il player
					config = 1; //assegno il Zombie Brain
					ConfigureAgent (config); 
					timer = 0;
					isBrainSwitched = true;
					Debug.Log (brain);
				}
			}

			if (config == 0) {
				if (hit.CompareTag ("chest")) {
					config = 2;
					ConfigureAgent (config);
					timer = 0;
					isBrainSwitched = true;
					Debug.Log (brain);

				}
			}

			if (config != 0 && timer > 20 ){
				isBrainSwitched = false;  
				config = 0;
				ConfigureAgent (config);
				Debug.Log (brain);
			}


		}
	}

	void ConfigureAgent(int Config){ // funzione di Switch per i cervelli
		if (Config == 0) {
			GiveBrain (NoZombieBrain);
		}
		if (Config == 1) {
			GiveBrain (ZombieBrain);
		}
		if (Config == 2) {
			GiveBrain (ZombieBrainChest);
		}
	}
}
