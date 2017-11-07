using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
   // Vector3 movement = new Vector3();
    public float moveSpeed;
    public float horizontal;
    public float vertical;
    public float gravity = 10f;
    float jumpSpeed;
   public float jumpForce = 10f;
    // bool crouch = false;
    // Use this for initialization
    float crouchSpeed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CharacterController myCharacterController = GetComponent<CharacterController>();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, jumpSpeed, vertical);
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(horizontal,0,vertical), 0.9f);
        if (horizontal != 0f || vertical != 0f)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {

            }
        }
   
        if (myCharacterController.isGrounded)
        {
            jumpSpeed -= gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                jumpSpeed = jumpForce;
            }
        }
        else
        {
            
            jumpSpeed -= gravity * Time.deltaTime;
        }

        //I still need to adjust how jumpspeed is applied



        myCharacterController.Move(movement*moveSpeed*Time.deltaTime);

        //if(Input.GetKey(KeyCode.W))
        //{
        //    movement = transform.forward * moveSpeed * Time.deltaTime;
        //}
        //

    }
}
