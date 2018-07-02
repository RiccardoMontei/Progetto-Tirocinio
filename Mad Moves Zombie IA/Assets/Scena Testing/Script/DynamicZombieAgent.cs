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
		float rayDistance = 70f;
		float[] rayAngles = { -20f, -10f, 0f, 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f, 100f, 110f, 120f , 130f, 140f, 150f, 160f, 170f, 180f, 190f, 200f };
		string[] detectableObjects;

		detectableObjects = new string[] { "player", "ground", "block","chest" };

		AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
		AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 2.5f, 0f));
		AddVectorObs((float)GetStepCount() / (float)agentParameters.maxStep);

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
			animator.SetBool ("Attack", false);
			break;
		case 1:
			animator.SetBool ("Stay", false);
			animator.SetBool ("Walk", true);
			animator.SetBool ("Run", false);
			animator.SetBool ("Attack", false);
			break;
		case 2:
			animator.SetBool ("Stay", false);
			animator.SetBool ("Walk", false);
			animator.SetBool ("Run", true);
			animator.SetBool ("Attack", false);
			break;
		case 3:
			animator.SetBool ("Stay", false);
			animator.SetBool ("Walk", false);
			animator.SetBool ("Run", false);
			animator.SetBool ("Attack", true);
			break;
		case 4:
			rotateDir = transform.up * 1f;
			break;
		case 5:
			rotateDir = transform.up * -1f;
			break;
		}
		transform.Rotate (rotateDir, Time.fixedDeltaTime * 150f);


	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		AddReward(-1f / agentParameters.maxStep);//Assegno un malus per incentivare la velocità di esecuzione
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
