using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerMovementNick : MonoBehaviour {

	Vector3 inputVector;
	Rigidbody rbody;

	public float gravityStrength;
	public float speed;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		//Altering speed in update, rather than fixed update like we did before (also valid)
		inputVector = transform.right * horizontal + transform.forward * vertical;

		//Don't forget to normalize for diagonal exploit
		if (inputVector.magnitude > 1f) {
			inputVector = Vector3.Normalize (inputVector);
		}

	}

	//Capitalize me
	void FixedUpdate(){
		//Override velocity, but only when we're not pressing anything
		if (inputVector.magnitude > 0.01f) {
			rbody.velocity = inputVector * speed + Physics.gravity * gravityStrength;
		}
	}
}
