using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingConfiguration : MonoBehaviour {
	public bool activeTraining;
	public bool zombieBrainTrainer;
	public bool zombieBrainChestTrainer;
	public bool noZombieBrainTrainer;

	public GameObject zombieBrain;
	public GameObject zombieBrainChest;
	public GameObject noZombieBrain;

	void start(){
		//Selettore di cervello da addestrare in base ai flag da attivare nella UI Di Unity
		if(activeTraining){
			if (zombieBrainTrainer) {
				zombieBrain.SetActive (true);
				zombieBrainChest.SetActive (false);
				noZombieBrain.SetActive (false);
			}
			if(zombieBrainChestTrainer) {
				zombieBrainChest.SetActive (true);
				zombieBrain.SetActive (false);
				noZombieBrain.SetActive (false);
			}
			if(noZombieBrainTrainer) {
				noZombieBrain.SetActive (true);
				zombieBrainChest.SetActive (false);
				zombieBrain.SetActive (false);
			}
		}
			
	}
}
