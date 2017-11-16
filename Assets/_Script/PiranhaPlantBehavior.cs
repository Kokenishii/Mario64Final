﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to a Piranha Plant object to make it respond to (and damage) the player
public class PiranhaPlantBehavior : MonoBehaviour {

	//The different states the Piranha Plant can inhabit
	public enum PiranhaPlantState {
		sleeping,	//0
		attacking	//1
	}

	public PiranhaPlantState currentState;

	//The game object for the player
	GameObject player;

	//The head (and stem) of the Piranha Plant, which points towards the player when the player is detected,
	//assigned in the inspector
	public GameObject piranhaPlantHead;

	//The distance from the center of the Piranha Plant game object
	//that the player needs to be under in order to wake up the Piranha Plant
	//and get it to attack,
	//assigned in the inspector
	public float distanceToWakeUp;

	//The speed the player needs to be moving in order to wake up the Piranha Plant,
	//assigned in the inspector
	public float speedToWakeUp;

	// Use this for initialization
	void Start () {

		//Assigns the player object
		player = GameObject.FindWithTag ("Player");

		currentState = PiranhaPlantState.sleeping;
	}
	
	// Update is called once per frame
	void Update () {

		//The state of the Piranha when it has not detected the player
		if (currentState == PiranhaPlantState.sleeping) {

			if (Vector3.Distance (player.transform.position, this.transform.position) < distanceToWakeUp) {

				//IN PROGRESS: This code, meant to prevent the plant from responding to the player
				//if they're moving slowly enough, still needs fixing.
				//For now, the plant will automatically move into it's attacking state
				//regardless of player speed

				//if (player.GetComponent<Rigidbody> ().velocity.magnitude > speedToWakeUp) {

				currentState = PiranhaPlantState.attacking;
			
				//}
			}
		}

		//The state of the Piranha when it has not detected the player
		if (currentState == PiranhaPlantState.attacking) {

			//IN PROGRESS: The following code is meant to allow the plant to rotate to face the player.
			//Currently it rotates, but not as desired.

			float headRotationX = piranhaPlantHead.transform.rotation.x;
			float headRotationZ = piranhaPlantHead.transform.rotation.z;

			piranhaPlantHead.transform.rotation = Quaternion.LookRotation (player.transform.position);

			Vector3 headRotation = piranhaPlantHead.transform.rotation.eulerAngles;

			headRotation.x = headRotationX;
			headRotation.z = headRotationZ;

			piranhaPlantHead.transform.rotation = Quaternion.LookRotation(headRotation);
		}
	}
}