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
	
    //have this object follow mario
	// Update is called once per frame
	void LateUpdate () {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if (DeathBox.resetting == false)
        {
            transform.position = mario.transform.position;
        }
	}
//    public void LookDown()
//    {
//        transform.position = transform.position;
//    }

}
