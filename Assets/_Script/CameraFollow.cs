using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject mario;
//    public GameObject zoomOut;
//    float scrollSensitivity = 100f;
    //Vector3 cameraDistance = new Vector3(10,1,10);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        transform.position = mario.transform.position;
	}
}
