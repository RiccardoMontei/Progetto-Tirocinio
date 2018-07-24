using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private GameObject [] zombies;
	private GameObject[] zombieSpawns;
	private GameObject[] chestSpawn;
	public GameObject chest;
	public Text timerCounter;

	private bool deactiveAll=true;
	private bool spawnedChests=false;

	private int counterWave = 1;
	private int wave =1;
	public int zombiesInScene = 0;
	private int maxZombiesinScene=0;

	private float timer=0;
	private int realTimer;

	void Start(){
		zombies = GameObject.FindGameObjectsWithTag ("block");
		zombieSpawns = GameObject.FindGameObjectsWithTag ("zombieSpawn");
		chestSpawn = GameObject.FindGameObjectsWithTag ("chestSpawn");
	
		for (int i = 0; i < zombies.Length; i++) {
			zombies [i].SetActive (false);
		}
	}

	void Update(){
		realTimer = 300 - (int)timer;
		timerCounter.text = "Next wave:\n" + realTimer;
		timer += Time.deltaTime;
		maxZombiesinScene = 10 + (wave * 5);
		if (deactiveAll) {
			for (int i = 0; i < zombies.Length; i++) {
				zombies [i].SetActive (false);
			}
			deactiveAll = false;
		}

		if (counterWave % 3 == 0) {
			spawnedChests = true;
			counterWave = 1;
		}
		if (spawnedChests) {
			Instantiate (chest, chestSpawn [Random.Range (0, chestSpawn.Length)].transform);
			Instantiate (chest, chestSpawn [Random.Range (0, chestSpawn.Length)].transform);
			Instantiate (chest, chestSpawn [Random.Range (0, chestSpawn.Length)].transform);
			spawnedChests = false;
		}

		if (zombiesInScene == 0 || timer > 300) {
			do {
				int index=Random.Range(0, zombies.Length);

				if(!zombies[index].activeInHierarchy){
					zombies[index].SetActive(true);
					zombiesInScene++;
				}
			
			} while(zombiesInScene < maxZombiesinScene);
			timer = 0;
			wave++;
			counterWave++;
		}


	}


}