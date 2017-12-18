using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to the "Damage Source" object, a child of the Thwomp object
public class ThwompDamageScript : MonoBehaviour
{

	public bool doneDamage = false;

	//The script attached to the parent of this object, which governs things such as the Thwomp's movement
	//NewThwompBehavior myNewThwompBehavior;


	// Use this for initialization
	void Start ()
	{

		//myNewThwompBehavior = this.gameObject.GetComponentInParent<NewThwompBehavior> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider col)
	{

		//Makes sure that it is the player that this object has collided with
		if (col.gameObject.tag == "Player") {

			//Makes sure the Thwomp is above the player, and can only do damage when landing on the player from above
			if (col.transform.position.y <= this.transform.position.y) {

				//Checks if the player has already recieved damage from this Thwomp during this trigger
				if (doneDamage == false) {

					//Deals damage
					PlayerHealthAndPickups.Instance.power -= 3;

					//Creates some screen shake
					ScreenShake.shakeStrength = 1f;

					//Ensures that the Whomp can't deal damage to the player repeatedly over the enxt few frames during this trigger
					doneDamage = true;
				}
			}
		}
	}

	void OnTriggerExit (Collider col)
	{

		//Makes sure that it is the player that the Whomp has collided with
		if (col.gameObject.tag == "Player") {

			//myNewThwompBehavior.stopFalling = false;
			//this.GetComponent<BoxCollider> ().isTrigger = false;

			//Allows this Whomp to do damage to the player once again, now that it was moved off of the player
			doneDamage = false;
		} else {

			//myNewThwompBehavior.stopFalling = true;
		}
	}
}
