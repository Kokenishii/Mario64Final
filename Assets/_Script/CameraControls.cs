using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
    //make a control scheme for the camera such that you can:
    //zoom the camera in and out from mario
    //rotate the camera 90 degrees along mario
    //make mario unable to move and then you can move the camera around freely?
	// Use this for initialization
    public GameObject mario;
    float startDistanceFromMario;
    float currentDistanceFromMario;
    public GameObject zoomIn;
    public GameObject zoomOut;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject defualtZoom;
    Vector3 startPosition;
    Vector3 startRotation;
	void Start () {
        startDistanceFromMario = gameObject.transform.position.z - mario.transform.position.z;
        startRotation = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //zoom in on mario
        Vector3 marioRotation = new Vector3(mario.transform.rotation.x,mario.transform.rotation.y,mario.transform.rotation.z);
        currentDistanceFromMario = gameObject.transform.position.z - mario.transform.position.z;
       if (startDistanceFromMario == currentDistanceFromMario)
       {
            
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                
                transform.SetPositionAndRotation(zoomIn.transform.position, Quaternion.Euler(marioRotation));
        }

        //zoom out from mario
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
                transform.SetPositionAndRotation(zoomOut.transform.position, Quaternion.Euler(marioRotation));
        }
       }
        if (startDistanceFromMario != currentDistanceFromMario)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                transform.SetPositionAndRotation(defualtZoom.transform.position, Quaternion.Euler(marioRotation));
            }

            //zoom out from mario
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                transform.SetPositionAndRotation(defualtZoom.transform.position, Quaternion.Euler(marioRotation));
            }
        }
         //roate to the left along mario 90 degrees
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                transform.SetPositionAndRotation(leftSide.transform.position, Quaternion.Euler(0,-90,0));
            }

        //rotate to the right along mario 90 degrees
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                transform.SetPositionAndRotation(rightSide.transform.position, Quaternion.Euler(0,90,0));
            }

	}
}
