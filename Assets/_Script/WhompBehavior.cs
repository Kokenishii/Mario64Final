using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script that attaches to the Whomp prefab
public class WhompBehavior : MonoBehaviour {

	public enum WhompState {
		waiting,	//0
		chasing,	//1
		falling,	//2
		fell,	//3
		rising	//4
	
	}

	public WhompState currentState;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (currentState == WhompState.waiting) {
		}

		if (currentState == WhompState.chasing) {
		}

		if (currentState == WhompState.falling) {
		}

		if (currentState == WhompState.fell) {
		}

		if (currentState == WhompState.rising) {
		}
	}
}
