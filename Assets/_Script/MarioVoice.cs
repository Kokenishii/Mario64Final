using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioVoice : MonoBehaviour {
    public Animator marioAnimator;
    public AudioSource marioWah;
    public AudioSource marioYah;
    public AudioSource marioYah2;
    public AudioSource marioWoo;
    public AudioSource marioYahoo;
    public AudioSource marioYahooo;
    public AudioSource marioWehoo;
    public AudioSource marioWahoo;
    public AudioSource marioOoo;
    public AudioSource marioOof;
    public AudioSource marioOof2;
    public AudioSource marioOof3;
    public AudioSource marioOof4;
    public AudioSource marioFalling;
    public AudioSource marioFalling2;
    public AudioSource marioHereWeGo;
    public AudioSource marioLetsAGo;
    int numSoundPlayed = 0;
    int numSoundPlayed1 = 0;
    int numSoundPlayed2 = 0;
    int numSoundPlayed3 = 0;
   public static int numSoundPlayed4 = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((marioAnimator.GetBool("isJumping") || marioAnimator.GetBool("isDiving")) && numSoundPlayed < 1)
        {
            
            marioJumpingNoise();

        }
        if (marioAnimator.GetBool("isPunching") && numSoundPlayed < 1)
            {
                float randomnumber = Random.value;
                if (randomnumber <= .25)
                {
                    marioWah.Play();
                numSoundPlayed++;

                }
                else if (randomnumber > .25 && randomnumber <= .50)
                {
                    marioYah.Play();
                numSoundPlayed++;
                }
                else if (randomnumber > .50 && randomnumber <= .75)
                {
                    marioWoo.Play();
                numSoundPlayed++;
                }
                else if (randomnumber > .75)
                {
                    marioOoo.Play();
                numSoundPlayed++;
                }
        }
        if((marioAnimator.GetBool("isGroundPounding")) && numSoundPlayed1 <1)
            {
                marioWah.Play();
            numSoundPlayed1++;
            }
        if((marioAnimator.GetBool("isBackflipping"))  && numSoundPlayed2 <1)
        {
            marioYah.Play();
            numSoundPlayed2++;
        }
        if((marioAnimator.GetBool("isLongJumping")) && numSoundPlayed3 <1)
        {
            marioWahoo.Play();
            numSoundPlayed3++;
        }
        if(ScreenShake.shakeStrength == 1f && numSoundPlayed4 < 1)
            {
            Debug.Log("didItWork");
                    float randomnumber = Random.value;
                if (randomnumber <= .25)
                    {
                        marioOof.Play();
                numSoundPlayed4++;
                    }
                else if (randomnumber > .25 && randomnumber <= .50)
                    {
                        marioOof2.Play();
                numSoundPlayed4++;
                    }
                else if (randomnumber > .50 && randomnumber <= .75)
                    {
                        marioOof3.Play();
                numSoundPlayed4++;
                    }
                else if (randomnumber > .75)
                    {
                        marioOof4.Play();
                numSoundPlayed4++;
                    }
            }
        if ((marioAnimator.GetBool("isJumping") || marioAnimator.GetBool("isDiving")) == false)
        {
            numSoundPlayed = 0;
        } 
        if(marioAnimator.GetBool("isGroundPounding") == false)
        {
            numSoundPlayed1 = 0;
        }
        if(marioAnimator.GetBool("isBackflipping") == false)
        {
            numSoundPlayed2 = 0;
        }
        if(marioAnimator.GetBool("isLongJumping") == false)
        {
            numSoundPlayed3 = 0;
        }
        if(ScreenShake.shakeStrength == 0f)
        {
            numSoundPlayed4 = 0;
        }
	}
    void marioJumpingNoise()
    {
        float randomnumber = Random.value;
        if (randomnumber <= .25)
        {
            marioWah.Play();
            numSoundPlayed++;
        }
        else if (randomnumber > .25 && randomnumber <= .50)
        {
            marioYah.Play();
            numSoundPlayed++;
        }
        else if (randomnumber > .50 && randomnumber <= .75)
        {
            marioYahoo.Play();
            numSoundPlayed++;
        }
        else if (randomnumber > .75)
        {
            marioYah2.Play();
            numSoundPlayed++;
        }
    }

	public void fallToDeath()
	{
		marioFalling2.Play ();
	}


}
