using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to Thwomps to make them hover in place,
//periodically descend, and then rise back to their starting position
public class ThwompBehavior : MonoBehaviour {

	//The character controller used to regulate the Thwomp's movement
	CharacterController thwompCharacterController;

	//The two different states a Thwomp can inhabit
	public enum ThwompState
	{
		waitingToFall,	//0
		falling,	//1
		waitingToRise,	//2
		rising		//3
	}

	public ThwompState currentState;

	//The position to which the Thwomp returns after it has dropped on something
	Vector3 startPosition;

	//Counts the time until a Thwomp falls
	float timer;
	//Determines how long a Thwomp should hang in place, determined in the inspector
	public float secondsToDrop;

	//The speed at which the Thwomp falls, determined in the inspector
	public float dropSpeed;
	//The speed at which the Thwomp rises, determined in the inspector
	public float risingSpeed;


	void Start () {
		thwompCharacterController = this.GetComponent<CharacterController>();

		//Records the Thwomp's start position
		startPosition = this.transform.position;

		currentState = ThwompState.waitingToFall;

		timer = 0f;
	}

	void Update () {



		//If the Thwomp is wating to drop
		if (currentState == ThwompState.waitingToFall) {

			//Resets the Thwomp to it's starting position
			//this.GetComponent<Transform> ().position = startPosition;

			//Ticks up the timer
			timer += Time.deltaTime;

			//Checks if it's time to drop yet
			if (timer >= secondsToDrop) {

				//Resets the timer for the next drop
				timer = 0f;

				currentState = ThwompState.falling;
			}

			Debug.Log ("Waiting to fall.");
		}

		if (currentState == ThwompState.falling) {

			//Moves the Thwomp downward
			thwompCharacterController.Move (Vector3.down * dropSpeed * Time.deltaTime);

			Debug.Log ("Falling!");
		}

		if (currentState == ThwompState.rising) {

			//Moves the Thwomp upward
			thwompCharacterController.Move (Vector3.up * risingSpeed * Time.deltaTime);

			//Returns the Thwomp to waiting mode if it returns to it's original position
			if (this.transform.position.y >= startPosition.y) {
				currentState = ThwompState.waitingToFall;
			}

			Debug.Log ("Rising.");
		}
	}


	void OnCollisionEnter(Collision col) {

		Debug.Log ("Collided with " + col.gameObject.name);

		//Returns the Thwomp to a rising state if it lands on something that isn't the player
		if (col.gameObject.tag != "Player"
			&& currentState == ThwompState.falling) {

			//Makes sure the Thwomp isn't just colliding with itself
			if (col.gameObject.tag != "Thwomp") {

				currentState = ThwompState.rising;
			}
		}
	}
}
