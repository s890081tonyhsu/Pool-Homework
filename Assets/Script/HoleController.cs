using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBall"))
        {
        	Debug.Log("This is PlayerBall.");
        	other.gameObject.SetActive (false);
        }
        if (other.gameObject.CompareTag("GameBall"))
        {
        	Debug.Log("This is GameBall.");
        	other.gameObject.SetActive (false);
        }
    }
}
