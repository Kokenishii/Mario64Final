using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    //Problem Lists:
    //1. Crouching Lerp(Slow down instead of stopping suddenly) Lerp is not working well
    //2. If you keep pressing crouch, jump and jump again, it's not working. Because
   // Vector3 movement = new Vector3();
    public float moveSpeed;
    public float horizontal;
    public float vertical;
    public float gravity = 10f;
    float jumpSpeed;
   public float jumpForce = 10f;
    // bool crouch = false;
    // Use this for initialization
   public float crouchSpeed;
    public float crouchJumpHeight;
    public float crouchJumpDistance;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //get the character controller component
        CharacterController myCharacterController = GetComponent<CharacterController>();

        //initiating the input from keyboard
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //create vector3 movement for CharacterController.Move
        Vector3 movement = new Vector3(horizontal, jumpSpeed, vertical);

        //roate the character to wherever it is facing
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(horizontal,0,vertical), 0.9f);

        //if (horizontal != 0f || vertical != 0f)
        //{
        //    if(Input.GetKey(KeyCode.LeftShift))
        //    {
        //        print("crouch");
        //        movement = Vector3.Lerp(movement, new Vector3(0,movement.y,0), 0.5f);
        //        horizontal = 0;
        //        vertical = 0;
        //    }
        //}
        
    
   
        if (myCharacterController.isGrounded)
        {
            //jumpSpeed -= gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {

                //movement = Vector3.Lerp(movement, new Vector3(0, movement.y, 0), 0.1f);

      

                movement = Vector3.zero;

                if (Input.GetButtonDown("Jump"))
                {
                    jumpSpeed = crouchJumpHeight*jumpForce;
                    movement += crouchJumpDistance * transform.forward;

                }

            }
            else
            {

                if (Input.GetButtonDown("Jump"))
                {
                    jumpSpeed = jumpForce;
                    
                }

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
