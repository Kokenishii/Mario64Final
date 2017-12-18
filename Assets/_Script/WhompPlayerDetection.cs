﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the invisible trigger object parented 
//to the Whomp that represents the Whomp's field of vision.
//This script checks for collisions with this object to determine
//whether or not the Whomp sees the player
public class WhompPlayerDetection : MonoBehaviour {

	//The WhompBehavior script attached to this particular Whomp
	public WhompBehavior myWhompBehavior;

	// Use this for initialization
	void Start () {
		
		myWhompBehavior = GetComponentInParent<WhompBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Checks to see if Whomp sees the player, and informs
	//the WhompBehavior script of the result
	void OnTriggerEnter (Collider col)
	{

		//Makes sure that it is the player that this object has collided with, 
		//and that the Whomp was in patrolling mode
		if (col.gameObject.tag == "Player"
			&& myWhompBehavior.currentState == WhompBehavior.WhompState.patrolling) {

			myWhompBehavior.playerDetected = true;
			myWhompBehavior.currentState = WhompBehavior.WhompState.chasing;
		}
	}

	//Checks to see if the player has left the Whomp's field of vision, and informs
	//the WhompBehavior script of the result
	void OnTriggerExit (Collider col)
	{

		//Makes sure that it is the player that this object has collided with
		if (col.gameObject.tag == "Player") {
			myWhompBehavior.playerDetected = false;
		}
	}
}
