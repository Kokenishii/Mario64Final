using System.Collections;
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


	// Use this for initialization
	void Start () {

		currentState = PiranhaPlantState.sleeping;
	}
	
	// Update is called once per frame
	void Update () {

		//The state of the Piranha when it has not detected the player
		if (currentState == PiranhaPlantState.sleeping) {

			//Do nothing
		}

		//The state of the Piranha when it has not detected the player
		if (currentState == PiranhaPlantState.attacking) {

			//Reduces player health by three
			PlayerHealthAndPickups.Instance.power -= 3;
		}
	}

	//Checks to see if something, potentially the player, has entered the Piranha Plant's
	//detection zone
	void OnCollisionEnter(Collision col) {

		Debug.Log ("Collide");

		//If the object is a player, the Piranha Plant will attack
		if (col.gameObject.tag == "Player") {
			currentState = PiranhaPlantState.attacking;
		}
	}
}
