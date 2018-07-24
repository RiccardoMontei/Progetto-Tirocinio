using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceLife : MonoBehaviour {

	private int life;

	public GameObject particlesDestroy;

	void Start () {
		life = 50;
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			Instantiate (particlesDestroy, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}
	}

	public void DecreaseLife(int damage){
		life -= damage;
	}
}
