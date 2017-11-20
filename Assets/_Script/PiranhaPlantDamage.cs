using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlantDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{

		//Makes sure that it is the player that this object has collided with
		if (col.gameObject.tag == "Player") {

			//Does three points of damage to the player
			PlayerHealthAndPickups.Instance.power -= 3;
		}
	}
}
