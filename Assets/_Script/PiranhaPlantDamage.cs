using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script attached to the "Mouth" game object to allow it to damage
//to the player when player collides with it

public class PiranhaPlantDamage : MonoBehaviour
{

	//Checks to see if the player has taken damage from the Piranha Plant
	bool doneDamage;

	// Use this for initialization
	void Start ()
	{

		doneDamage = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider col)
	{

		//Makes sure that it is the player that this object has collided with
		if (col.gameObject.tag == "Player") {

			//Destroys the Piranha Plant if the player lands on it from above
			if (col.gameObject.transform.position.y > this.transform.position.y) {

				GameObject.Destroy (this.gameObject);
			
			//If the player didn't hit the plant from above, then the player is going to 
			//take damage from the plant
			} else {

				//Makes sure the plant hasn't already done damage in this collision
				if (doneDamage == false) {

					//Does three points of damage to the player
					PlayerHealthAndPickups.Instance.power -= 3;
                    ScreenShake.shakeStrength = 1f;

					//Makes sure that the plant hasn't done damage to the player in this collision yet
					//(So that it doesn't do a ton of damage for a bunch of frames in a row to the player
					//after a single collision)
					doneDamage = true;
				}
			}
		}
	}

	void OnTriggerExit (Collider col)
	{

		//Makes sure that it is the player that this object has collided with
		if (col.gameObject.tag == "Player") {

			//Resets this bool so that the player can take damage from the plant for the next collision
			doneDamage = false;
		}
	}
}
