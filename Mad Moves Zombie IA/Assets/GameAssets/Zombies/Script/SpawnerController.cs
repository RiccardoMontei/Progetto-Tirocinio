using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    //Zombie da istanziare
    public GameObject[] zombies = new GameObject[6];//Zombie da istanziare
    public GameObject gameController; //Il gamController

    private int waitSpawn = 5;//Tempo di attesa tra uno spawn e l'altro
    private int randomZombieSpawn; //Variabile usata come indice nel vettore di zombie


    // Use this for initialization
    void Start()
    {
        /*StartCoroutine(ZombieSpawn());//Avvio la procedura di spawn*/
    }

    void Update()
    {


    }
    //Procedura di spawn
   /* IEnumerator ZombieSpawn()
    {

        while (true)
        {
            randomZombieSpawn = UnityEngine.Random.Range(0, 6);//Randomizzo l'indice del vettore
            if (gameController.GetComponent<GameController>().zombieCounter < gameController.GetComponent<GameController>().maxZombiesInScene)
            { //Se ho meno zombie del massimo
                if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 100)// Se lo spawner è nel raggio di 100 gli permetto di spawnare zombie
                {
                    Instantiate(zombies[randomZombieSpawn], transform.position, transform.rotation);//Ne istanzio uno random nel GO spawnZombie
                    gameController.GetComponent<GameController>().zombieCounter++; //Incremento il numero di zombie nella mappa
                }
            }
            yield return new WaitForSeconds(waitSpawn);//Attesa di spawn}
        }
    }*/
}
