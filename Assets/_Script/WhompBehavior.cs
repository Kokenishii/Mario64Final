using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script that attaches to the Whomp prefab in order to make it do the 
//things it does in it's various states
public class WhompBehavior : MonoBehaviour {

	//The character controller used to regulate the Whomp's movement
	CharacterController whompCharacterController;

	public enum WhompState {
		walking,	//0
		chasing,	//1
		falling,	//2
		fell,	//3
		rising	//4
	
	}

	public WhompState currentState;

	//The locations at which the Whomp turns around and walks
	//in the other direction
	public Transform boundaryA;
	public Transform boundaryB;

	//The speed at which the Whomp walks when it isn't attacking
	public float walkSpeed;

	//The speed at which the Whomp turns around when it has reached the end of it's movement
	public float turnSpeed;

	//Determines if the Whomp has turned around at the end of it's rotation
	bool turnedAround;

	Vector3 walkDirection;

	// Use this for initialization
	void Start () {
		
		whompCharacterController = this.GetComponent<CharacterController> ();

		turnedAround = false;

		walkDirection = Vector3.forward;

		this.transform.rotation = Quaternion.LookRotation (boundaryA.transform.position - transform.position, Vector3.up);

	}
	
	// Update is called once per frame
	void Update () {

		if (currentState == WhompState.walking) {

			if (this.transform.position.z >= boundaryA.position.z
				&& turnedAround == false) {

				this.transform.rotation = Quaternion.LookRotation (boundaryB.transform.position - transform.position, Vector3.up);

				walkDirection = walkDirection * -1f;
				turnedAround = true;

			} else if (this.transform.position.z <= boundaryB.position.z
				&& turnedAround == false) {

				this.transform.rotation = Quaternion.LookRotation (boundaryA.transform.position - transform.position, Vector3.up);

				walkDirection = walkDirection * -1f;
				turnedAround = true;

			} else {

				whompCharacterController.Move (walkDirection * walkSpeed * Time.deltaTime);

				turnedAround = false;

			}

			//else if whomp is at point B, turn around

			//else, walk forward
				//if just cleared point A, go one way
				//if just cleared point B, go the other way
		}

		if (currentState == WhompState.chasing) {
		}

		if (currentState == WhompState.falling) {
		}

		if (currentState == WhompState.fell) {
		}

		if (currentState == WhompState.rising) {
		}
	}
}
