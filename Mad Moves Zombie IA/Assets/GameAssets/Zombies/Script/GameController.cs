using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private GameObject [] zombies;
	private GameObject[] zombieSpawns;
	private GameObject[] chestSpawn;
	public GameObject chest;

	private bool deactiveAll=true;

	private int wave =1;
	public int zombiesInScene = 0;
	private int maxZombiesinScene=0;

	private float timer=0;

	void Start(){
		zombies = GameObject.FindGameObjectsWithTag ("block");
		zombieSpawns = GameObject.FindGameObjectsWithTag ("zombieSpawn");
		chestSpawn = GameObject.FindGameObjectsWithTag ("chestSpawn");
	
		for (int i = 0; i < zombies.Length; i++) {
			zombies [i].SetActive (false);
		}
	}

	void Update(){
		timer += Time.deltaTime;
		maxZombiesinScene = 10 + (wave * 5);
		if (deactiveAll) {
			for (int i = 0; i < zombies.Length; i++) {
				zombies [i].SetActive (false);
			}
			deactiveAll = false;
		}

		if (wave % 3 == 0)
			Instantiate (chest, chestSpawn [Random.Range (0, chestSpawn.Length)].transform);

		if (zombiesInScene == 0 || timer > 420) {
			do {
				int index=Random.Range(0, zombies.Length);

				if(!zombies[index].activeInHierarchy){
					zombies[index].SetActive(true);
					zombiesInScene++;
				}
			
			} while(zombiesInScene < maxZombiesinScene);
			timer = 0;
			wave++;
		}


	}


}