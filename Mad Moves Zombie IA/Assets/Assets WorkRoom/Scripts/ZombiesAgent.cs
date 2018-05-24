using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesAgent : Agent {

	public GameObject area; //Game area
	public GameObject player; //The player
	public GameObject bounds;

	private float distance;
	private float minDistance=100.0f;

	RayPerception rayPer;
	ZombiesAcademy academy;


	Rigidbody zombieRB;  //cached on initialization
	Rigidbody playerRB;  //cached on initialization

	void Awake(){
		brain = FindObjectOfType<Brain> ();
		academy = FindObjectOfType<ZombiesAcademy> ();

	
	}
	public override void InitializeAgent(){
		base.InitializeAgent ();
		zombieRB = GetComponent<Rigidbody> ();
		playerRB = player.GetComponent<Rigidbody> ();
		rayPer = GetComponent<RayPerception> ();
	}

	public override void CollectObservations ()
	{
		float rayDistance = 30f;
		float[] rayAngles = { -20f, 0f, 20f, 40f, 60f, 80f,100f, 120f ,140f, 160f, 180f, 200f };
		string[] detectableObjects;
		detectableObjects = new string[] { "player", "wall", "block" };
		Vector3 localVelocity = transform.InverseTransformDirection(zombieRB.velocity);
		AddVectorObs(localVelocity.x);
		AddVectorObs(localVelocity.z);
		AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
		AddVectorObs((float)GetStepCount() / (float)agentParameters.maxStep);

	

	}


	public void MoveAgent(float[] act)
	{

		Vector3 dirToGo = Vector3.zero;
		Vector3 rotateDir = Vector3.zero;

		int action = Mathf.FloorToInt(act[0]);

		if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous) {
			dirToGo = transform.forward * Mathf.Clamp (act [0], -1f, 1f);
			rotateDir = transform.up * Mathf.Clamp (act [1], -1f, 1f);
		} else {
			// Goalies and Strikers have slightly different action spaces.
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
		}
	}

	/// <summary>
	/// Called every step of the engine. Here the agent takes an action.
	/// </summary>
	public override void AgentAction(float[] vectorAction, string textAction)
	{
		AddReward(-1f / agentParameters.maxStep);
		MoveAgent(vectorAction);
	}



}
