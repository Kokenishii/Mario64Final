using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounding : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
