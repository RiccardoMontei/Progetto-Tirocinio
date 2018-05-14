using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour {
	public GameObject fence;
	public GameObject pivotFence;
	public bool activeCrafting = false;

	public int wood=0;
	public int rope=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		StartStopCrafting ();	

		if (activeCrafting) {
			if (Input.GetButton ("Fire1"))
				pivotFence.transform.Rotate (0, 4,0);
			if(Input.GetButton("Aim"))
				pivotFence.transform.Rotate (0, -4,0);

			RaycastHit hit;
			Debug.DrawRay(transform.position, transform.forward * 7, Color.magenta);

			if (Physics.Raycast(transform.position, transform.forward, out hit, 7))
			{//Se il raggio colpisce qualcosa!
				Debug.Log (hit.transform.gameObject.tag);
				if (hit.transform.gameObject.CompareTag("Ground")) 
				{
					if (Input.GetKeyDown (KeyCode.E))
					if (wood >= 6 && rope >= 2) {
						Instantiate (fence, pivotFence.transform.position, pivotFence.transform.rotation);
						wood -= 6;
						rope -= 2;
					}
				}

			}
		}
	}

	public void StartStopCrafting(){
		
		if (Input.GetKeyDown (KeyCode.T)) {
			if (!activeCrafting && !pivotFence.activeInHierarchy) {
				activeCrafting = true;
				pivotFence.SetActive (true);
			} else if (activeCrafting && pivotFence.activeInHierarchy) {
				activeCrafting = false;
				pivotFence.SetActive (false);
			}
		}
	}
}
