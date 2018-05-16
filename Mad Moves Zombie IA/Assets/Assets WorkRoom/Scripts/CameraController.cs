using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //variabile per assegnare come attributo un oggetto unity
    public GameObject player;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        //differenza di posizione della camera
        offset = transform.position - player.transform.position;
}
	void LateUpdate () {
        //posizione aggiornata della camera 
        transform.position = player.transform.position + offset;
	}
}
