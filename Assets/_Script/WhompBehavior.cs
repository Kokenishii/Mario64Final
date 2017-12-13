using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script that attaches to the Whomp prefab in order to make it do the
//things it does in it's various states

//NOTE 1: This script relies on two game objects, "Whomp Marker A" and "Whomp Marker B," to determine
//what it should walk towards and where it should stop walking and turn around. These two objects should
//be placed at wither side of the Whomp at whatever boundaries you would like to set for it's movement,
//and then assigned to this script in the inspector.

//NOTE 2: The code that governs if the player can be seen by the Whomp is in a script
//attached to the "Whomp Vision Box" game object, which is a child of the base
//Whomp game object


public class WhompBehavior : MonoBehaviour
{

	//A singleton, which will allow other scripts
	//(specifically the WhompPlayerDetection script)
	//to interact with this script and change it's variables
	public static WhompBehavior instance;

	//The character controller used to regulate the Whomp's movement
	CharacterController whompCharacterController;

	//A listing of all the Whomp's possible states
	public enum WhompState
	{
		patrolling,
		//0
		chasing,
		//1
		falling,
		//2
		fell,
		//3
		rising
		//4
	
	}

	public WhompState currentState;

	//The locations at which the Whomp turns around and walks
	//in the other direction
	public GameObject startingDirectionBoundary;
	public GameObject secondDirectionBoundary;

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

	//Determines whether or not the Whomp should be moving at the moment
	bool isMoving;

	//The game object parented to the Whomp that casues the Whomp to go into chasing mode
	//whenever the player walks into it
	public GameObject whompVisionBox;

	//A timer used to measure how long ago a Whomp fell on the ground
	public float timer;
	//The number of seconds before the Whomp rises back up after falling
	public float timeToRiseAgain;

	//The animator used to govern the Whomp's animations
	Animator myAnimator;

	// Use this for initialization
	void Start ()
	{

		//Initializes the singleton
		instance = this;

		whompCharacterController = this.GetComponent<CharacterController> ();

		turnedAround = false;

		this.transform.rotation = Quaternion.LookRotation (startingDirectionBoundary.transform.position - transform.position, Vector3.up);

		walkDirection = startingDirectionBoundary.transform.position - transform.position;

		player = GameObject.FindGameObjectWithTag ("Player");

		playerDetected = false;

		isMoving = true;

		//Grabs the first child of the Whomp (the only child of the Whomp is the Whomp Vision Box)
		//and links it to the variable whompVisionBox
		//whompVisionBox = this.gameObject.transform.GetChild (0);

		timer = 0f;

		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		//If the Whomp is just walking around without having sighted a player
		if (currentState == WhompState.patrolling) {

			//Allows the Whomp to move
			isMoving = true;

			//Sets the Whomp to it's patrolling animation
			myAnimator.SetBool ("isPatrolling", true);

			//The Whomp movement function
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

			//Keeps the Whomp from moving back and forth
			isMoving = false;

			//Makes sure a few other animation states are set to false
			myAnimator.SetBool ("isRising", false);
			myAnimator.SetBool ("isPatrolling", false);

			//Starts the Whomp's falling animation
			myAnimator.SetBool ("isFalling", true);

			Debug.Log ("I'm FALLING!");

			currentState = WhompState.fell;
		}

		//If the Whomp has fallen is waiting to get up
		if (currentState == WhompState.fell) {

			//Counts up a timer
			timer += Time.deltaTime;

			//When the timer reaches a given publically assigned value, the Whomp begins rising again
			if (timer >= timeToRiseAgain) {

				//Resets the timer
				timer = 0f;

				currentState = WhompState.rising;
			}

			Debug.Log ("I fell.");
		}

		//If the Whomp is rising back up
		if (currentState == WhompState.rising) {

			//Makes sure the Whomp isn't stuck in it's falling animation later when it isn't time to fall
			myAnimator.SetBool ("isFalling", false);

			//Starts the rising animation
			myAnimator.SetBool ("isRising", true);

			Debug.Log ("I'm rising.");

			currentState = WhompState.patrolling;
		}
	}





	//This function governs how a Whomp moves
	void WhompMovement ()
	{

		//If the player has collided with the "Whomp Vision Box" game object, which is a child of the base
		//Whomp game object, the Whomp will move more quickly, so as to catch the player. Otherwise, the Whomp
		//will move at it's normal patrol speed
		if (playerDetected == true) {

			speed = chaseSpeed;

		} else if (playerDetected == false) {

			speed = patrolSpeed;

		}
	
		//If the Whomp has moved past boundary A, it turns around and walks towards boundary B
		//if (this.transform.position.z >= boundaryA.z
		//    && turnedAround == false) {

		//	this.transform.rotation = Quaternion.LookRotation (boundaryB - transform.position, Vector3.up);

		//	walkDirection = walkDirection * -1f;
		//	turnedAround = true;

		//Vice-versa: If the Whomp has moved past boundary B, it turns around and walks towards boundary A
		//} else if (this.transform.position.z <= boundaryB.z
		//           && turnedAround == false) {
			
		//	this.transform.rotation = Quaternion.LookRotation (boundaryA - transform.position, Vector3.up);

		//	walkDirection = walkDirection * -1f;
		//	turnedAround = true;

		//If the Whomp hasn't passed either boundary, it just keeps on walkin' forward
		//	} else {

		//Checks to see if the Whomp should be moving or not.
		//If it has just tried to fall on the player, then it shouldn't be moving.
		if (isMoving == true) {
				
			whompCharacterController.Move (walkDirection * speed * Time.deltaTime);

			turnedAround = false;
		}
		//}
	}




	void OnTriggerEnter (Collider col)
	{

		//Debug.Log ("COLLIDED!!!");

		//if you collide with this or this, reverse direction & head towards the other target

		if (col.gameObject.tag == "Whomp Starting Boundary"
		    && turnedAround == false) {
		
			this.transform.rotation = Quaternion.LookRotation (secondDirectionBoundary.transform.position - transform.position, Vector3.up);

			//walkDirection = walkDirection * -1f;
			walkDirection = secondDirectionBoundary.transform.position - transform.position;
			turnedAround = true;

			Debug.Log ("This is the starting boundary!");
		}

		if (col.gameObject.tag == "Whomp Second Boundary"
		    && turnedAround == false) {

			this.transform.rotation = Quaternion.LookRotation (startingDirectionBoundary.transform.position - transform.position, Vector3.up);

			//walkDirection = walkDirection * -1f;
			walkDirection = startingDirectionBoundary.transform.position - transform.position;
			turnedAround = true;

			Debug.Log ("This is the second boundary!");
		}
	}
}
