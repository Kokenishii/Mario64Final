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
    public Sprite[] powerMeterImages = new Sprite[9];
    public GameObject powerSprite;
   

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
        if (power > 8)
        {
            power = 8;
        }
        Healthbar();
		//Updates the onscreen lives counter
		livesCountText.GetComponent<Text>().text = "Lives x " + lives.ToString(); 

		//Updates the coin score counter
		coinCountText.GetComponent<Text>().text = "Coins x " + coinScore.ToString();

		//Updates the onscreen stars counter
		starCountText.GetComponent<Text>().text = "Stars x " + stars.ToString();

		//Checks to see if the player's power is less than maximum and
		//if the power meter should appear onscreen
		if (power < 8) {
			powerMeter.SetActive (true);
            powerSprite.SetActive(true);
			powerMeter.GetComponent<Text>().text = "Power: " + power.ToString();

		} else {
			powerMeter.SetActive (false);
            powerSprite.SetActive(false);
		}

		//Checks to see if the player has died
        if (power == 0 || power < 0) {
			lives--;
			power = 8;
			Debug.Log ("You died!");

			//Reloads the level (the current number of lives will carry over)
			SceneManager.LoadScene (0);
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
		if (col.gameObject.tag == "Star") {
			stars++;
			Destroy (col.gameObject);
			Debug.Log ("You win!");
		}

		//Checks to see if the player has collected a regular coin
		if (col.gameObject.tag == "Coin") {

			coinScore++;

			if (power < 8) {
				power++;
			}

			Debug.Log ("Power: " + power);

			Destroy (col.gameObject);
		}

		//Checks to see if the player has collected a red coin
		if (col.gameObject.tag == "Red Coin") {

			coinScore += 2;

			if (power < 8) {
				power += 2;
			}

			redCoinCount++;

			Debug.Log("Red Coins Collected: " + redCoinCount);

			Destroy (col.gameObject);
	}
}
    void Healthbar(){
        Image health;
        health = powerSprite.GetComponent<Image>();
        health.sprite = powerMeterImages[power];

    }
}
