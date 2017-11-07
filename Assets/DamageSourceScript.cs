using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSourceScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {

		//Makes sure that it is the player that this object has collided with
		if (col.gameObject.name == "Player") {

			PlayerHealthAndPickups.Instance.power--;
			Debug.Log ("Power: " + PlayerHealthAndPickups.Instance.power);
		}
	}
}
