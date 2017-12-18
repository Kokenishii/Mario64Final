using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //crouch key: shift
    //jump key: space
    //punch key:Mouse0

    //IF YOU ARE ADDING MOVEMENT RELATED EFFECTS (particles, etc)
    // Looking for //[[[[[[[[MOVEMENT NAME]]]]]]], I will leave instructions there
    //e.g. If you want to add effects when the character jumping, looking for [[[[[JUMPING]]]]

    public ParticleSystem jumpDust;
    public AudioClip footstep;
    AudioSource sound;

    bool isSlidingDown;    
    public Animator marioAnimator;
    public float backFlipSpeed;
    public Vector3 movement;
    public float moveSpeed;
    public float horizontal;
    public float vertical;
    public float gravity = 10f;
    float jumpSpeed; //internal use
    [Tooltip("how high jump is")]
    public float jumpForce = 10f;
    // bool crouch = false;
    // Use this for initialization
    public float crouchSpeed;
	bool startRunning = false;

    [Tooltip("between 0 and 1, recommend:0.9")]
    public float crouchMultiplier=0.9f;
    public float crouchJumpHeight;
    public float crouchJumpDistance;
    public GameObject myCamera;
    Vector3 additionalMove;
    bool takeInput = true;
    bool isLanded;
    bool canJump;
   
   
    void Start()
    {
		sound = GetComponent<AudioSource> ();
        crouchSpeed = 1;
        // marioAnimator = GetComponent<Animator>();
        canJump = true;
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
         movement = forwardMovement + rightMovement;
       // marioAnimator.SetFloat("runSpeed", movement.magnitude*1.2f );
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(movement.x, 0, movement.z), 0.7f);
        //roate the character to wherever it is facing
        if (movement.x == 0 && movement.z == 0)
        {
			sound.Stop (); //Stops the footstep sound if Mario isn't moving
            marioAnimator.SetBool("isRunning", false);
            marioAnimator.SetBool("isStanding", true);

        }
        else
        {
			if (marioAnimator.GetBool ("isStanding")) {
				startRunning = true;
			}
            marioAnimator.SetBool("isRunning", true);
            marioAnimator.SetBool("isStanding", false);
        }




        if (myCharacterController.isGrounded)
        {
            //[[[[[[WALKING]]]]]// CALLED when WALKING

			if (!sound.isPlaying && (movement.x != 0 || movement.z != 0)) { //Plays a footstep sound as long as he's
				sound.clip = footstep;
				sound.Play ();
			}

			if (startRunning) {
				startRunning = false;
				ParticleSystem p = Instantiate(jumpDust, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y-180, transform.localEulerAngles.z)); //When landing, create a dust cloud.
				Destroy(p.gameObject, 0.5f);
			}



            //[[[[LANDING]]]]/// If you are trying to add something happened ONCE when LANDING,
            // MODIFY THIS FUNCTION AT THE END OF THE SCRIPT

            if (!isLanded)
            {
                Landing();//MODIFY THE LANDING FUNCTION AT THE END
				ParticleSystem p = Instantiate(jumpDust, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), Quaternion.Euler(-90f, 0f, 0f)); //When landing, create a dust cloud.
				Destroy(p.gameObject, 0.5f);
            }



            //END OF YOUR SCRIPT


            marioAnimator.SetBool("isGroundPounding", false);
            //marioAnimator.SetBool("isStanding", true);
            marioAnimator.SetBool("isJumping", false);
            marioAnimator.SetBool("isLongJumping", false);
            marioAnimator.SetBool("isCrouching", false);
            marioAnimator.SetBool("isBackflipping", false);
            if(!isSlidingDown)
            {
                additionalMove = Vector3.zero;
            }
         
            crouchSpeed = 1;
            if (Mathf.Abs(horizontal) >= 0.9 || Mathf.Abs(vertical) >= 0.9)
            {
                if (Input.GetButtonDown("Punch") && !isSlidingDown && canJump )
                {
                    canJump = false;
                    //StartCoroutine(adjustJump());
                    jumpSpeed = 0.5f * jumpForce;
                    //movement *= 3;
                    //  crouchSpeed *= 4f*  crouchJumpDistance
                    additionalMove = transform.forward * 10f * Time.deltaTime;
                    marioAnimator.SetBool("isDiving", true);
                    StartCoroutine(DivingEnd());
                    print("longpunch");

                }
            }
            else
            {
                if (Input.GetButtonDown("Punch")&&!isSlidingDown)
                {
                    marioAnimator.SetTrigger("isPunchingTrigger");
                    if (Input.GetButtonDown("Punch"))
                    {
                        print("normal Punch");
                    }
                }
                // marioAnimator.SetBool("isPunching", true);
              
            }

            //jumpSpeed -= gravity * Time.deltaTime;
            if (Input.GetButton("Crouch"))
            {
				sound.Stop ();
                marioAnimator.SetBool("isCrouching", true);

                // additionalMove = -transform.forward * backFlipSpeed * Time.deltaTime;
                 crouchSpeed = Mathf.Lerp(crouchSpeed, 0, crouchMultiplier * Time.deltaTime);
                 
             
                // print("crouching");
                if (Input.GetButtonDown("Punch"))
                {
                    jumpSpeed = jumpForce * 0.4f;
                    
                    print("sliding kicik");
                }
              // StartCoroutine(crouchEnd());
                
   //Press Jump >>
                if (Input.GetButtonDown("Jump") && !isSlidingDown)
                {
    //Press Jump >> don't move, you back flip    
               
                    if (Mathf.Abs(horizontal) <= 0.1f && Mathf.Abs(vertical) <= 0.1f)
                    {
                        jumpSpeed = 1.2f * jumpForce;
                      
                        crouchSpeed = 1;

                        additionalMove = -transform.forward * backFlipSpeed * Time.deltaTime;
						sound.Stop ();
                        marioAnimator.SetBool("isBackflipping", true);
                        print("backflip??");
                        // StartCoroutine(Backflip());

						ParticleSystem p = Instantiate(jumpDust, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y-180, transform.localEulerAngles.z));
						Destroy (p.gameObject, 0.5f);
                     
                    }

                    else
                    {
                        jumpSpeed = crouchJumpHeight * jumpForce;
                        crouchSpeed *= crouchJumpDistance;
						sound.Stop ();
                        marioAnimator.SetBool("isLongJumping", true);
                        print("long jump");

						ParticleSystem p = Instantiate(jumpDust, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), Quaternion.identity);
						Destroy (p.gameObject, 0.5f);
                    }
                    
                                           
                    
                       
                    
               
                   

                }
                else
                {
                    //crouchSpeed = 1;
                }

            }

            else
            {
                crouchSpeed = 1;
                //reset crouch speed to 1 (normal running)

                if (Input.GetButtonDown("Jump") && !isSlidingDown &&canJump)
                {
                    //[[[[[[JUMPING]]]]]// CALLED when JUMPING STARTS

					sound.Stop ();

                    //END OF YOUR SCRIPT
                    marioAnimator.SetBool("isRunning", false);
                    marioAnimator.SetBool("isJumping", true);
                    jumpSpeed = jumpForce;
                    print("normal jump");

                }
            }
          



        }
        else //IN THE AIR
        //If jump is not grounded,keep adding gravity to jumpspeed
        {
            isLanded = false;
           // additionalMove = Vector3.zero;

            jumpSpeed -= gravity * Time.deltaTime;
            if(Input.GetButtonDown("Crouch"))
            {
                marioAnimator.SetBool("isGroundPounding", true);
                jumpSpeed -= 6f;
                print("grounchPunch");
            }
            if (Input.GetButtonDown("Punch")&&marioAnimator.GetBool("isDiving")==false&&canJump)
            {
                canJump = false;
                jumpSpeed = 0.5f * jumpForce;
                //movement *= 3;
                //  crouchSpeed *= 4f*  crouchJumpDistance
                additionalMove = transform.forward * 10f * Time.deltaTime;
                marioAnimator.SetBool("isDiving", true);
                StartCoroutine(DivingEnd());
                print("longpunchair");
            }

        }
       if(isSlidingDown)
        {
            //crouchSpeed = Mathf.Lerp(crouchSpeed, 0, 0.5f);

            additionalMove +=  new Vector3(0,0,0.3f) * Time.deltaTime;
        }
        //movement.y equals jumpspeed, which takes into gravity/jumping/high jumping, etc

        movement.x *= moveSpeed *crouchSpeed * Time.deltaTime;
        movement.z *= moveSpeed * crouchSpeed *Time.deltaTime;
        movement.y = jumpSpeed*moveSpeed*Time.deltaTime;
        // print(movement.magnitude);
        
        myCharacterController.Move(movement+additionalMove);

        //if(Input.GetKey(KeyCode.W))
        //{
        //    movement = transform.forward * moveSpeed * Time.deltaTime;
        //}
        //

    }
    IEnumerator Backflip()
    {

        yield return 0;


    }
    IEnumerator longJump()
    {
        yield return 0;

    }
    IEnumerator DivingEnd()
    {
        yield return new WaitForSeconds(1.5f);
        marioAnimator.SetBool("isDiving", false);
        StartCoroutine(adjustJump());
    }

    IEnumerator startSliding()
    {
        yield return new WaitForSeconds(0.1f);
        isSlidingDown = false;
    }
    IEnumerator adjustJump()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }

    void OnTriggerEnter(Collider col)
    {
    
        if(col.gameObject.tag == "SlidingPlatform")
        {
            
            isSlidingDown = true;

        }
       
    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "SlidingPlatform")
        {
            StartCoroutine(startSliding());


        }

    }

    void Landing() //CALLED ONCE PER LANDING
    {
        //WRITE YOUR CODES HERE
        print("landed!");

        isLanded = true;
        
      
    }



}


	
