using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to Thwomps to make them do what they do
public class ThwompBehavior : MonoBehaviour {

	//The two different states a Thwomp can inhabit
	public enum ThwompState
	{
		waiting,	//0
		falling,	//1
		rising		//2
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
	
		//Records the Thwomp's start position
		startPosition = this.transform.position;

		currentState = ThwompState.waiting;

		timer = 0f;
	}

	void Update () {

		//If the Thwomp is wating to drop
		if (currentState == ThwompState.waiting) {

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
			this.transform.Translate (Vector3.down * dropSpeed * Time.deltaTime);

			Debug.Log ("Falling!");
		}

		if (currentState == ThwompState.rising) {

			//Moves the Thwomp upward
			this.transform.Translate (Vector3.up * risingSpeed * Time.deltaTime);

			//Returns the Thwomp to waiting mode if it returns to it's original position
			if (this.transform.position.y >= startPosition.y) {
				currentState = ThwompState.waiting;
			}

			Debug.Log ("Rising.");
		}
	}
}
