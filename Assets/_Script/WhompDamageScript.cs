using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the Whomp to do damage to the player when the Whomp drops down on them.

//This is attached to a central joint in the Whomp's rectangular body (joint 8, parented to joint 7), the same joint upon which
//a box collider is placed. This script and that collider are placed there because that joint moves WITH
//the Whomp as it tries to fall on the player, allowing it's collider to move with it as well.

public class WhompDamageScript : MonoBehaviour
{

	bool doneDamage = false;

	void OnTriggerEnter (Collider col)
	{

		//Makes sure that it is the player that the Whomp has collided with
		if (col.gameObject.tag == "Player") {

			//Ensures that the Whomp is in falling mode or has just finished falling (because touching the Whomp
			//doesn't cause damage unless it's falling)
			if (WhompBehavior.instance.currentState == WhompBehavior.WhompState.falling
			    || WhompBehavior.instance.currentState == WhompBehavior.WhompState.fell) {

				//Checks if the player has already recieved damage from this Whomp during this trigger
				if (doneDamage == false) {

					//Deals damage
					PlayerHealthAndPickups.Instance.power -= 3;
                    ScreenShake.shakeStrength = 1f;

					//Ensures that the Whomp can't deal damage to the player repeatedly over the enxt few frames during this trigger
					doneDamage = true;

					Debug.Log ("Power: " + PlayerHealthAndPickups.Instance.power);
				}
			}
		}
	}

	void OnTriggerExit (Collider col)
	{

		//Makes sure that it is the player that the Whomp has collided with
		if (col.gameObject.tag == "Player") {

			//Allows this Whomp to do damage to the player once again, now that it was moved off of the player
			doneDamage = false;
		}
	}
}
