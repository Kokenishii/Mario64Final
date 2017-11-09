using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Lets us talk to scene management
using UnityEngine.SceneManagement;

public class PlayerHealthAndPickups : MonoBehaviour {

	public int power;
	public static int lives = 3;
	public int stars;
	public int coinScore;
	public int redCoinCount;

	public GameObject starCountText;
	public GameObject coinCountText;
	public GameObject livesCountText;
	public GameObject powerMeter;

	//Creates a Singleton, something that other pieces of code can reference 
	//to change values in this script
	public static PlayerHealthAndPickups Instance;

	// Use this for initialization
	void Start () {

		//Initializes the Singleton
		Instance = this;

		power = 8;
	}
	
	// Update is called once per frame
	void Update () {

		//Updates the onscreen lives counter
		livesCountText.GetComponent<Text>().text = "Lives x " + lives; // TODO: add ".ToString()" on lives

		//Updates the coin score counter
		coinCountText.GetComponent<Text>().text = "Coins x " + coinScore; // TODO: add ".ToString()" on coinScore

		//Updates the onscreen stars counter
		starCountText.GetComponent<Text>().text = "Stars x " + stars; // TODO: add ".ToString()" on stars

		//Checks to see if the player's power is less than maximum and
		//if the power meter should appear onscreen
		if (power < 8) {
			powerMeter.SetActive (true);
			powerMeter.GetComponent<Text>().text = "Power: " + power; // TODO: add ".ToString()" on power
		} else {
			powerMeter.SetActive (false);
		}

		//Checks to see if the player has died
		if (power == 0) {
			lives--;
			power = 8;
			Debug.Log ("You died!");

			//Reloads the level (the current number of lives will carry over)
			SceneManager.LoadScene ("WhompFortress");
		}

		//Checks to see if the player has lost all their lives
		if (lives < 0) {
			Debug.Log ("Game over!");
			// TODO: write code to actually handle this, and end the game?
		}
	}

	void OnTriggerEnter(Collider col) {
        print("collide");
		//Checks to see if the player has collected the star
		if (col.gameObject.name == "Star") { // TODO: use tags instead of gameObject.name, this is dangerous
			stars++;
			Destroy (col.gameObject); // TODO: "GameObject.Destroy" can be simplified to "Destroy"
			Debug.Log ("You win!");
		}

		//Checks to see if the player has collected a regular coin
		if (col.gameObject.name == "Coin") { // TODO: use tags instead of gameObject.name, this is dangerous... like, what if the gameobject was named "Coin (Clone)" or "Coin (2)"?

			coinScore++;

			if (power < 8) {
				power++;
			}

			Debug.Log ("Power: " + power);

			GameObject.Destroy (col.gameObject); // TODO: "GameObject.Destroy" can be simplified to "Destroy"
		}

		//Checks to see if the player has collected a red coin
		if (col.gameObject.name == "Red Coin") { // TODO: use tags instead of gameObject.name, this is dangerous

			coinScore += 2;

			if (power < 8) {
				power++;	//NOTE: Do red coins add more power than that?
			}

			redCoinCount++;

			Debug.Log("Red Coins Collected: " + redCoinCount);

			GameObject.Destroy (col.gameObject); // TODO: "GameObject.Destroy" can be simplified to "Destroy"
	}
}
}
