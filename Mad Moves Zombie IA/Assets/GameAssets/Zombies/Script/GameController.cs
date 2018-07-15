﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int hitCount = 0 ; //Counter dei colpi dati

	private int randomPlayerSpawn=0 ; //Indice di spawn random per il player

	public GameObject[] playerSpawn = new GameObject[55]; //Attay di spawn
	public GameObject[] chestSpawns;

	private bool flagI = true; //Indice di scorrimento arry spawns dal basso
	private int indexI = 1 ;

	private bool flagJ = false; //Indice di scorrimento arry spawns dal basso
	private int indexJ = 54;

	public Text counterText;

	void Start(){
		chestSpawns = GameObject.FindGameObjectsWithTag ("chestSpawn");

	}

	void Update(){
		counterText.text = hitCount.ToString (); //Aggirno il contatore degli hit
		randomPlayerSpawn = Random.Range (0, playerSpawn.Length); //Valore random per lo spawn del player
	}

	public void ResetChest(){
		for (int i = 0; i < chestSpawns.Length; i++) {
			int j = Random.Range (0, chestSpawns.Length);
			if (!chestSpawns [j].activeInHierarchy) {
				chestSpawns [i].SetActive (false);
				chestSpawns [j].SetActive (true);
			} else if(!chestSpawns[i].activeInHierarchy) {
				chestSpawns [j].SetActive (false);
				chestSpawns [i].SetActive (true);
			}
		}
	}

	//Funzione che respawna il player usando indici univoci per ciascun player 
	public void RespawnFunction(GameObject player){
		if (hitCount < playerSpawn.Length) { //Se ho colpito il numero di spawns totali
			if (!flagI) { // e l'ultimo ad essere stato colpito era in posizione "indexJ"
				player.transform.position = playerSpawn [indexI].transform.position; //Spawno il player in "indexI"
				if (indexI <= playerSpawn.Length)//Proteggo l'incremento
					indexI++; //incremento l'indice
				//Setto i flag corretti
				flagI = true; 
				flagJ = false;
			}
			if (!flagJ) {
				player.transform.position = playerSpawn [indexJ].transform.position;
				if (indexJ >= 0)
					indexJ--;
				flagI = false;
				flagJ = true;
			}
		} else//Se ho colpito più di TOT volte spawno random
			player.transform.position = playerSpawn [randomPlayerSpawn].transform.position;
	}
}