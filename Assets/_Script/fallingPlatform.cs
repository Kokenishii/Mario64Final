using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour {

	//Causes the moving platform to fall after a period of time when a player touches it.

	public float timerGoal = 1; //The time in seconds that the platform waits before it falls
	public float fallSpeed = 1; //The speed at which the platform falls
	bool countdown = false;
	bool falling = false;
	Vector3 startPos;
	float fallTimer = 0;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown) { //Determines if enough time has passed for the platform to start falling
			fallTimer += Time.deltaTime;
			if (fallTimer > timerGoal) {
				countdown = false;
				falling = true;
			}
		}
		if (falling) { //Moves the platform down after enough time has passed
			transform.Translate (0, -1*fallSpeed, 0);
			if (transform.position.y < startPos.y - 50f) {
				transform.position = startPos;
				falling = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			countdown = true;
			fallTimer = 0;
		}
	}
}
