using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRollFix : MonoBehaviour {

	public bool gameStart;

	private GlobalBallEffects globalBallEffects;

	void Start () {
		GameObject globalBallEffectsObject = GameObject.Find("Balls");
        if(globalBallEffectsObject != null)
            globalBallEffects = globalBallEffectsObject.GetComponent<GlobalBallEffects>();
        if(globalBallEffects == null)
            Debug.Log("Cannot find 'GlobalBallEffects' script");	
		Physics.sleepThreshold = 0.3F;
	}
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag("GameBall") && gameStart) {
			globalBallEffects.soundPlay();
			Debug.Log(name + " and " + collision.gameObject.name);
		}
	}
}
