using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script that attaches to the Whomp prefab in order to make it do the 
//things it does in it's various states

//NOTE: This script relies on two game objects, "Whomp Marker A" and "Whomp Marker B," to determine
//what it should walk towards and where it should stop walking and turn around. These two objects should
//be placed at wither side of the Whomp at whatever boundaries you would like to set for it's movement,
//and then assigned to this script in the inspector

//ALSO NOTE: The code that governs if the player can be seen by the Whomp is in a script
//attached to the "Whomp Vision Box" game object, which is a child of the base
//Whomp game object


public class WhompBehavior : MonoBehaviour {

	//A singleton, which will allow other scripts
	//(specifically the WhompPlayerDetection script)
	//to interact with this script and change it's variables
	public static WhompBehavior instance;

	//The character controller used to regulate the Whomp's movement
	CharacterController whompCharacterController;

	//A listing of all the Whomp's possible states
	public enum WhompState {
		patrolling,	//0
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

	//The speed at which the Whomp moves
	float speed;

	//The value for how fast the Whomp moves when it's patrolling, determined in the inspector
	public float patrolSpeed;

	//The value for how fast the Whomp moves when it's chasing, determined in the inspector
	public float chaseSpeed;

	//The speed at which the Whomp turns around when it has reached the end of it's movement
	public float turnSpeed;

	//Determines if the Whomp has turned around at the end of it's rotation
	bool turnedAround;

	//The direction that the Whomp walks in
	Vector3 walkDirection;

	//The game object representing the player
	public GameObject player;

	//A bool that indicates whether or not the player has been seen by this Whomp
	//and should be chased by them.
	//This bool is mainly altered in the WhompPlayerDetection script, and then
	//passed to this script via the singleton "Instance"
	public bool playerDetected;

	// Use this for initialization
	void Start () {

		//Initializes the singleton
		instance = this;

		whompCharacterController = this.GetComponent<CharacterController> ();

		turnedAround = false;

		walkDirection = Vector3.forward;

		this.transform.rotation = Quaternion.LookRotation (boundaryA.transform.position - transform.position, Vector3.up);

		player = GameObject.FindGameObjectWithTag ("Player");

		playerDetected = false;

	}
	
	// Update is called once per frame
	void Update () {

		//If the Whomp is just walking around without having sighted a player
		if (currentState == WhompState.patrolling) {

			WhompMovement ();
		}

		//If the Whomp has spotted the player and is waddling aggressively towards them
		//as Whomps are wont to do
		if (currentState == WhompState.chasing) {

			WhompMovement ();

			if (Vector3.Distance (player.transform.position, this.transform.position) <= 3f) {

				currentState = WhompState.falling;
			}

			Debug.Log ("I'm chasing!");
		}

		//The Whomp is collapsing on the ground in attempts to crush the player
		if (currentState == WhompState.falling) {

			Debug.Log ("I'm FALLING!");
		}

		//If the Whomp has fallen is waiting to get up
		if (currentState == WhompState.fell) {
		}

		//If the Whomp is rising back up
		if (currentState == WhompState.rising) {
		}
	}





	//This function governs how a Whomp moves
	void WhompMovement() {

		//If the player has collided with the "Whomp Vision Box" game object, which is a child of the base
		//Whomp game object, the Whomp will move more quickly, so as to catch the player. Otherwise, the Whomp
		//will move at it's normal patrol speed
		if (playerDetected == true) {

			speed = chaseSpeed;

		} else if (playerDetected == false) {

			speed = patrolSpeed;

		}
	
		//If the Whomp has moved past boundary A, it turns around and walks towards boundary B
		if (this.transform.position.z >= boundaryA.position.z
			&& turnedAround == false) {

			this.transform.rotation = Quaternion.LookRotation (boundaryB.transform.position - transform.position, Vector3.up);

			walkDirection = walkDirection * -1f;
			turnedAround = true;

			//Vice-versa: If the Whomp has moved past boundary B, it turns around and walks towards boundary A
		} else if (this.transform.position.z <= boundaryB.position.z
			&& turnedAround == false) {

			this.transform.rotation = Quaternion.LookRotation (boundaryA.transform.position - transform.position, Vector3.up);

			walkDirection = walkDirection * -1f;
			turnedAround = true;

			//If the Whomp hasn't passed either boundary, it just keeps on walkin' forward
		} else {

			whompCharacterController.Move (walkDirection * speed * Time.deltaTime);

			turnedAround = false;

		}
	}
}
