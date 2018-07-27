using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController4MA : MonoBehaviour {
	public int hitCount = 0 ; //Counter dei colpi dati

	private int randomPlayerSpawn=0 ; //Indice di spawn random per il player

	public GameObject[] playerSpawn; 
	public GameObject[] chestSpawns;

	public int deactivedCHests = 0;

	private bool flagI = true; //Indice di scorrimento arry spawns dal basso
	private int indexI = 1 ;

	private bool flagJ = false; //Indice di scorrimento arry spawns dal basso
	private int indexJ = 54;

	public Text counterText;

	void Start(){
		//playerSpawn = GameObject.FindGameObjectsWithTag ("playerSpawns");
		//chestSpawns = GameObject.FindGameObjectsWithTag ("chestSpawn");

	}

	void Update(){
		//counterText.text = hitCount.ToString (); //Aggirno il contatore degli hit
		//randomPlayerSpawn = Random.Range (0, playerSpawn.Length); //Valore random per lo spawn del player
	}

	public void ResetChest(GameObject chest){
		int i = Random.Range (0, chestSpawns.Length);
		chest.transform.position = chestSpawns [i].transform.position;
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
