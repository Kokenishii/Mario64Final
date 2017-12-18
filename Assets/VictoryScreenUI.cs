using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreenUI : MonoBehaviour {
    public GameObject winScreen;
    private WinTrigger myTrigger;
	// Use this for initialization
	void Start () {
        winScreen.SetActive(false);
        myTrigger = GameObject.Find("Screen Wipe").GetComponent<WinTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
        if (myTrigger.finishAnimation == true)
        {
            winScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            if(Input.GetKeyDown(KeyCode.Space))
                {
                    Time.timeScale = 1.0f;
                    SceneManager.LoadScene (0);
                }
        }


	}
    public void RestartScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene (0);
    }
}
