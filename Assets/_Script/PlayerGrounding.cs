using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounding : MonoBehaviour {

	//Checks if the player is inside a moving platform's trigger hitbox, and if so, makes the player a child of the moving platform until they exit the trigger hitbox.

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "MovingPlatform") {
			transform.parent = col.transform;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "MovingPlatform") {
			transform.parent = null;
		}
	}
}
