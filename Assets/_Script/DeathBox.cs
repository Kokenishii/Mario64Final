using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour {

	public MarioVoice voice;
    public Animator deathWipe;
	float resetDelay = 3;
	public static bool resetting = false;

    //when mario triggers this deathbox, scene resets
	// Use this for initialization
	void Start () {
		resetDelay = 3;
		resetting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (resetting && resetDelay > 0) {
			resetDelay -= Time.deltaTime;
		} else if (resetting && resetDelay <= 0) {
            PlayerHealthAndPickups.lives--;
            PlayerHealthAndPickups.deadFromEnemies = false;
			SceneManager.LoadScene (0);
		}
	}
    void OnTriggerEnter(Collider c)
    {
		if (c.gameObject.tag == "Player") { //If the player falls out of bounds, cause the falling sound and reset the scene after 3 seconds.
			voice.fallToDeath ();
            deathWipe.SetBool("gotDead", true);
			resetting = true;
		}
    }
}
