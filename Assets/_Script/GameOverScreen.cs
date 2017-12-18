using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    public GameObject panel;
	// Use this for initialization
	void Start () {
        panel.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerHealthAndPickups.lives == 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1.0f;
                PlayerHealthAndPickups.lives = 3;
                SceneManager.LoadScene(0);
            }
        }
	}
}
