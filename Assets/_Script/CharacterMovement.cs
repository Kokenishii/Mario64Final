using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //crouch key: shift
    //jump key: space
    //punch key:J
    //Problem Lists:
    //1. Crouching Lerp(Slow down instead of stopping suddenly) Lerp is not working well
    //2. If you keep pressing crouch, jump and jump again, it's not working. Because
    // Vector3 movement = new Vector3();
    public float moveSpeed;
    public float horizontal;
    public float vertical;
    public float gravity = 10f;
    float jumpSpeed; //internal use
    [Tooltip("how high jump is")]
    public float jumpForce = 10f;
    // bool crouch = false;
    // Use this for initialization
    float crouchSpeed;

    [Tooltip("between 0 and 1, recommend:0.9")]
    public float crouchMultiplier=0.9f;
    public float crouchJumpHeight;
    public float crouchJumpDistance;
    public GameObject myCamera;

    bool takeInput = true;

   
   
    void Start()
    {
        crouchSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //get the character controller component
        CharacterController myCharacterController = GetComponent<CharacterController>();
      
       
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        horizontal *= Mathf.Abs(horizontal);
        vertical *= Mathf.Abs(vertical); 
        //initiating the input from keyboard
    

        //create vector3 movement for CharacterController.Move
        //Using forward and rightward of camera to treat vertical and horizontal axis first
        Vector3 forwardMovement = Camera.main.transform.forward * vertical;
        Vector3 rightMovement = Camera.main.transform.right * horizontal;
        Vector3 movement = forwardMovement + rightMovement;


        transform.forward = Vector3.Lerp(transform.forward, new Vector3(movement.x, 0, movement.z), 0.7f);
        //roate the character to wherever it is facing
        
        if(Mathf.Abs(horizontal) >=0.9|| Mathf.Abs(vertical) >=0.9)
        {
            if(Input.GetButtonDown("Punch"))
            {
                jumpSpeed = 0.5f * jumpForce;
                //movement *= 3;
                crouchSpeed *= crouchJumpDistance;
                print("longpunch");
            }
        }
        else if(Input.GetButtonDown("Punch"))
        {
            print("normal Punch");
        }

        if (myCharacterController.isGrounded)
        {
            //jumpSpeed -= gravity * Time.deltaTime;
            if (Input.GetButton("Crouch"))
            {
                //crouch
                crouchSpeed = Mathf.Lerp(crouchSpeed, 0, crouchMultiplier * Time.deltaTime);
               // print("crouching");
                if(Input.GetButtonDown("Punch"))
                {
                    jumpSpeed = jumpForce * 0.4f;
                    
                    print("sliding kicik");
                }
              // StartCoroutine(crouchEnd());
                

                if (Input.GetButtonDown("Jump"))
                {
                   
                        jumpSpeed = crouchJumpHeight * jumpForce;
                        crouchSpeed *= crouchJumpDistance;
                    print("long jump");
                   

                }
                else
                {
                    crouchSpeed = 1;
                }

            }

            else
            {
                crouchSpeed = 1;
                //reset crouch speed to 1 (normal running)

                if (Input.GetButtonDown("Jump"))
                {
                    jumpSpeed = jumpForce;
                    print("normal jump");

                }
            }
          



        }
        else
        //If jump is not grounded,keep adding gravity to jumpspeed
        {

            jumpSpeed -= gravity * Time.deltaTime;
            if(Input.GetButtonDown("Punch"))
            {
                jumpSpeed -= 6f;
                print("grounchPunch");
            }
        }

        //movement.y equals jumpspeed, which takes into gravity/jumping/high jumping, etc
        movement.x *= moveSpeed *crouchSpeed * Time.deltaTime;
        movement.z *= moveSpeed * crouchSpeed *Time.deltaTime;
        movement.y = jumpSpeed*moveSpeed*Time.deltaTime;

        myCharacterController.Move(movement );

        //if(Input.GetKey(KeyCode.W))
        //{
        //    movement = transform.forward * moveSpeed * Time.deltaTime;
        //}
        //

    }
    IEnumerator crouchEnd()
    {
        yield return 0;
       
    }
    IEnumerator longJump()
    {
        yield return 0;

    }
}
