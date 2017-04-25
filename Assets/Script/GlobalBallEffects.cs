using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBallEffects : MonoBehaviour {
	private AudioSource audioS;
	void Start () {
		audioS = GetComponent<AudioSource>();
	}
	public void soundPlay() {
		audioS.Play();
	}
}
