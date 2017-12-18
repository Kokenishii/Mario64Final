using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the Whomp to do damage to the player when the Whomp drops down on them.

//This is attached to a central joint in the Whomp's rectangular body (joint 8, parented to joint 7), the same joint upon which
//a box collider is placed. This script and that collider are placed there because that joint moves WITH
//the Whomp as it tries to fall on the player, allowing it's collider to move with it as well.

public class WhompDamageScript : MonoBehaviour
{

	//The WhompBehavior script attached to this particular Whomp
	public WhompBehavior myWhompBehavior;

	bool doneDamage = false;

	// Use this for initialization
	void Start () {

		myWhompBehavior = GetComponentInParent<WhompBehavior>();
	}

	void OnTriggerEnter (Collider col)
	{

		//Makes sure that it is the player that the Whomp has collided with
		if (col.gameObject.tag == "Player") {

			//Ensures that the Whomp is in falling mode or has just finished falling (because touching the Whomp
			//doesn't cause damage unless it's falling)
			if (myWhompBehavior.currentState == WhompBehavior.WhompState.falling
			    || myWhompBehavior.currentState == WhompBehavior.WhompState.fell) {

				//Makes sure the Whomp is above the player, and can only do damage when landing on the player from above
				if (col.transform.position.y <= this.transform.position.y) {

				//Checks if the player has already recieved damage from this Whomp during this trigger
				if (doneDamage == false) {

					//Allows the Whomp to pass through the player, rather than causing them to be pushed into the ground,
					//by changing the collider into a trigger
					this.GetComponent<BoxCollider> ().isTrigger = true;

					//Deals damage
					PlayerHealthAndPickups.Instance.power -= 3;

					//Creates some screen shake
                    ScreenShake.shakeStrength = 1f;

					//Ensures that the Whomp can't deal damage to the player repeatedly over the enxt few frames during this trigger
					doneDamage = true;

					Debug.Log ("Power: " + PlayerHealthAndPickups.Instance.power);
				}
				}
			}
		}
	}

	void OnTriggerExit (Collider col)
	{

		//Makes sure that it is the player that the Whomp has collided with
		if (col.gameObject.tag == "Player") {

			this.GetComponent<BoxCollider> ().isTrigger = false;

			//Allows this Whomp to do damage to the player once again, now that it was moved off of the player
			doneDamage = false;
		}
	}
}
