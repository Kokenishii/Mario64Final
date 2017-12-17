using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to Thwomps to make them hover in place,
//periodically descend, and then rise back to their starting position
public class ThwompBehavior : MonoBehaviour
{

	AudioSource sounds;
	public AudioClip grunt;
	public AudioClip boom;

	//The character controller used to regulate the Thwomp's movement
	CharacterController thwompCharacterController;

	//The different states a Thwomp can inhabit
	public enum ThwompState
	{
		waitingToFall,
		//0
		falling,
		//1
		waitingToRise,
		//2
		rising
		//3
	}

	public ThwompState currentState;

	//The position to which the Thwomp returns after it has dropped on something
	Vector3 startPosition;

	//Counts the time until a Thwomp falls
	float timer;
	//Determines how long a Thwomp should hang in the air before falling, determined in the inspector
	public float secondsToDrop;
	//Determines how long a Thwomp should rest on the ground before rising, determined in the inspector
	public float secondsToRise;

	//The speed at which the Thwomp falls, determined in the inspector
	public float dropSpeed;
	//The speed at which the Thwomp rises, determined in the inspector
	public float risingSpeed;

	//Tells whether or not the Thwomp has done damage to the player on this cycle of it's motion
	bool didDamage = false;

	void Start ()
	{
		sounds = GetComponent<AudioSource> ();

		thwompCharacterController = this.GetComponent<CharacterController> ();

		//Records the Thwomp's start position
		startPosition = this.transform.position;

		currentState = ThwompState.waitingToFall;

		timer = 0f;
	}

	void Update ()
	{

		//STEP 1: Define ray
		Ray thwompDetectionRay = new Ray(transform.position, new Vector3(0, -1, 0));

		//STEP 2: Declare raycast distance
		float rayDistance = 2f;

		//STEP 3: Visualize the raycast
		Debug.DrawRay(thwompDetectionRay.origin, thwompDetectionRay.direction * rayDistance, Color.yellow);

		//If the Thwomp is wating to drop
		if (currentState == ThwompState.waitingToFall) {

			//Tells the script that it's okay for the Thwomp to do damage to the player again
			didDamage = false;

			//Resets the Thwomp to it's starting position
			this.GetComponent<Transform> ().position = startPosition;

			//Ticks up the timer
			timer += Time.deltaTime;

			//Checks if it's time to drop yet
			if (timer >= secondsToDrop) {

				//Resets the timer for the next drop
				timer = 0f;

				currentState = ThwompState.falling;
			}
		}

		if (currentState == ThwompState.falling) {

			//Moves the Thwomp downward
			thwompCharacterController.Move (Vector3.down * dropSpeed * Time.deltaTime);

			Vector3 currentPos = this.transform.position;

			//Checks if the Thwomp is still moving or if it has run into something
			StartCoroutine (CheckIfMoving ());
		}

		//If the Thwomp is wating to rise
		if (currentState == ThwompState.waitingToRise) {

			//Ticks up the timer
			timer += Time.deltaTime;

			//Checks if it's time to rise back up yet
			if (timer >= secondsToRise) {

				//Resets the timer for the next drop
				timer = 0f;

				currentState = ThwompState.rising;
			}
		}

		if (currentState == ThwompState.rising) {

			//Moves the Thwomp upward
			thwompCharacterController.Move (Vector3.up * risingSpeed * Time.deltaTime);

			//Returns the Thwomp to waiting mode if it returns to it's original position
			if (this.transform.position.y >= startPosition.y) {
				currentState = ThwompState.waitingToFall;
			}
		}
	}
		




	//This coroutine helps determine if the Thwomp has run into something
	//(as an alternative to OnCollisionEnter(), which was giving me (Nick) some problems)
	IEnumerator CheckIfMoving ()
	{

		//Saves the current position of the Thwomp
		Vector3 currentPos = this.transform.position;

		//Waits a frame
		yield return 0;

		//Checks to see if the Thwomp is in the same position as it was a frame ago
		if (currentPos == this.transform.position) {

			sounds.PlayOneShot (boom, 0.5f);
			sounds.PlayOneShot (grunt, 1f);

			//Tells the Thwomp to stop moving and to prepare to rise again
			currentState = ThwompState.waitingToRise;
		}

	}





	//void OnTriggerEnter (Collider col)
	//{


		//Makes sure that it is the player that this object has collided with
	//	if (col.gameObject.tag == "Player") {

			//Checks to make sure the Thwomp hasn't already damaged the player in this cycle of it's movement.
			//Prevents it from dealing damage on every frame of collision.
	//		if (didDamage == false) {
			
				//Does three points of damage to the player
	//			PlayerHealthAndPickups.Instance.power -= 3;

				//Ensures that this Thwomp can't damage the player again until it falls another time
	//			didDamage = true;
	//		}
	//	}
	//}
}
