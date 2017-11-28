using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPlatformOffset : MonoBehaviour {

    Animator liftAnimator;

    public float offset = 0.5f;

	// Use this for initialization
	void Start () {
        liftAnimator = GetComponent<Animator>();
        liftAnimator.SetFloat("Offset", offset);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
