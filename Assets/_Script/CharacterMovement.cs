using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

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
        
            
       
        //initiating the input from keyboard
    

        //create vector3 movement for CharacterController.Move
        //Using forward and rightward of camera to treat vertical and horizontal axis first
        Vector3 forwardMovement = Camera.main.transform.forward * vertical;
        Vector3 rightMovement = Camera.main.transform.right * horizontal;
        Vector3 movement = forwardMovement + rightMovement;


        transform.forward = Vector3.Lerp(transform.forward, new Vector3(movement.x, 0, movement.z), 0.7f);
        //roate the character to wherever it is facing



        if (myCharacterController.isGrounded)
        {
            //jumpSpeed -= gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                crouchSpeed = Mathf.Lerp(crouchSpeed, 0, crouchMultiplier * Time.deltaTime);

              // StartCoroutine(crouchEnd());
                

                if (Input.GetButtonDown("Jump"))
                {
                   
                        jumpSpeed = crouchJumpHeight * jumpForce;
                        movement += crouchJumpDistance * transform.forward;
                   
                   

                }

            }

            else
            {
                crouchSpeed = 1;
                if (Input.GetButtonDown("Jump"))
                {
                    jumpSpeed = jumpForce;

                }
            }
          



        }
        else
        //If jump is not grounded,keep adding gravity to jumpspeed
        {

            jumpSpeed -= gravity * Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                jumpSpeed -= 6f;
            }
        }

        //movement.y equals jumpspeed, which takes into gravity/jumping/high jumping, etc
        movement.y = jumpSpeed;
        myCharacterController.Move(movement * moveSpeed * Time.deltaTime*crouchSpeed);

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
}
