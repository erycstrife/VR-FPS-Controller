using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {


	public SteamVR_TrackedController Controller;
	Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
	}


	void Update () {
		if (Controller.triggerPressed) {
			anim.SetBool ("IsGrabbing", true);
		} else {
			anim.SetBool ("IsGrabbing", false);			
		}
	}
}
