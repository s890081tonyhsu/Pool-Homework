using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class HoleController : MonoBehaviour {

    private CameraController cameraController;
    private GlobalHoleEffects globalHoleEffects;

	void Start () {
        GameObject cameraControllerObject = GameObject.FindWithTag("GameController");
        if(cameraControllerObject != null)
            cameraController = cameraControllerObject.GetComponent<CameraController>();
        if(cameraController == null)
            Debug.Log("Cannot find 'CameraController' script");
        GameObject globalHoleEffectsObject = GameObject.Find("Holes");
        if(globalHoleEffectsObject != null)
            globalHoleEffects = globalHoleEffectsObject.GetComponent<GlobalHoleEffects>();
        if(globalHoleEffects == null)
            Debug.Log("Cannot find 'GlobalHoleEffects' script"); 
	}
	
	void OnTriggerEnter (Collider other)
    {
        string name = other.gameObject.name;
        string num;
        if (other.gameObject.CompareTag("PlayerBall")) {
        	Debug.Log("This is PlayerBall.");
            cameraController.AddScore(-1);
        	other.gameObject.transform.position = new Vector3(0, 0.5f, 0);
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            globalHoleEffects.soundPlay();
        }
        if (other.gameObject.CompareTag("GameBall")) {
        	Debug.Log("This is GameBall.");
            num = Regex.Match(name, @"\d+").Value;
            cameraController.AddScore(int.Parse(num));
        	other.gameObject.SetActive (false);
            globalHoleEffects.soundPlay();
        }
    }
}
