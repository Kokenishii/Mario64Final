using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMove : MonoBehaviour {
    Rigidbody myRigidBody;
    Vector3 inputVector;
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        inputVector = new Vector3(0f, 0f, verticalInput); 
        transform.Rotate(0f, horizontalInput * Time.deltaTime * 90f, 0f);

        if (inputVector.magnitude > 1)
        {
            inputVector = Vector3.Normalize(inputVector);
        }

	}

    void FixedUpdate()
    {
        //myRigidBody.AddForce(transform.TransformDirection(inputVector) * 25f);
        myRigidBody.AddRelativeForce(inputVector * 25f);
        //Debug.Log("my velocity: " + myRigidbody.velocity.ToString());
        //Debug.Log("my speed: " + myRigidbody.velocity.magnitude.ToString());
    }
}
