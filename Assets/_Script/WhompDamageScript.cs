using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the Whomp to do damage to the player when the Whomp drops down on them.

//This is attached to the central joint in the Whomp's rectangular body, the same joint upon which
//a box collider is placed. This script and that collider are placed there because that joint moves WITH
//the Whomp as it tries to fall on the player, allowing it's collider to move with it as well.

public class WhompDamageScript : MonoBehaviour {


	void OnTriggerEnter(Collider col) {

		//Makes sure that it is the player that the Whomp has collided with
		if (col.gameObject.tag == "Player") {

			//Ensures that the Whomp is in falling mode or has just finished falling (because touching the Whomp
			//doesn't cause damage unless it's falling)
			if (WhompBehavior.instance.currentState == WhompBehavior.WhompState.falling
				|| WhompBehavior.instance.currentState == WhompBehavior.WhompState.fell) {

			PlayerHealthAndPickups.Instance.power -= 3;
			Debug.Log ("Power: " + PlayerHealthAndPickups.Instance.power);
		}
	}
	}
}
