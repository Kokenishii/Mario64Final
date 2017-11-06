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

	void OnCollisionEnter(Collision col) {

		//Checks to see if the player has collected the star
		if (col.gameObject.name == "Star") {
			stars++;
			GameObject.Destroy (col.gameObject);
			Debug.Log ("You win!");
		}

		//Checks to see if the player has collected a coin
		if (col.gameObject.name == "Coin") {

			if (power > 8) {
				power++;
			}

			Debug.Log ("Power: " + power);

			GameObject.Destroy (col.gameObject);
		}
	}
}
