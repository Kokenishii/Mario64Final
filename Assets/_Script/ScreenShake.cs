using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
    public static float shakeStrength = 0f; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shakeStrength = 1f;
        }
	

   
        Vector3 shakeOffset = Vector3.right * Mathf.Sin(Time.time *27f) * 0.25f;
        shakeOffset += Vector3.up * Mathf.Sin(Time.time * 8f) * 0.25f;

        transform.localPosition = shakeOffset * shakeStrength; 

        shakeStrength = Mathf.Clamp(shakeStrength - Time.deltaTime  , 0f, 10f);
    }
}
