using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndPickups : MonoBehaviour {

	public int power;
	public int lives;
	public int stars;

	public GameObject starCountText;
	public GameObject livesCountText;


	// Use this for initialization
	void Start () {
		power = 8;
	}
	
	// Update is called once per frame
	void Update () {

		//Updates the onscreen lives counter
		livesCountText.GetComponent<Text>().text = "Lives x " + lives;

		//Updates the onscreen stars counter
		starCountText.GetComponent<Text>().text = "Stars x " + stars;
	}
}
