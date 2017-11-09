﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
    	//make a control scheme for the camera such that you can:
    	//zoom the camera in and out from mario
    	//rotate the camera 90 degrees along mario
    	//make mario unable to move and then you can move the camera around freely?
	// Use this for initialization
    public GameObject mario;
    public GameObject zoomIn;
    public GameObject zoomOut;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject defualtZoom; // YOU SHOULD FIX THIS VARIABLE NAME... right-click on it in MonoDevelop, and select Refactor > Rename
    int zoomDistance = 0;


	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        	//zoom in on mario
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
		    if (zoomDistance == 0)
		    {
			transform.SetPositionAndRotation(zoomIn.transform.position, zoomIn.transform.rotation);
			zoomDistance = 1;
		    }
		    else
		    {
			transform.SetPositionAndRotation(defualtZoom.transform.position, defualtZoom.transform.rotation);
			zoomDistance = 0;
		    }
		}

        //zoom out from mario
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (zoomDistance == 0)
            {
                transform.SetPositionAndRotation(zoomOut.transform.position, zoomOut.transform.rotation);
                zoomDistance = 2;
            }
                    else
                    {
                        transform.SetPositionAndRotation(defualtZoom.transform.position, defualtZoom.transform.rotation);
                        zoomDistance = 0;
                    }

       }

         //roate to the left along mario 90 degrees
            if(Input.GetKey(KeyCode.Alpha3))
            {
		    // TODO: make this framerate independent, and btw, this doesn't actually rotate 90 degrees
		    // also, the 2nd parameter defines an axis, not in degrees, see https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html
            	transform.RotateAround(mario.transform.position, Vector3.up, 5f); 
            	//transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
            }

        //rotate to the right along mario 90 degrees
            if(Input.GetKey(KeyCode.Alpha4))
            {
            	transform.RotateAround(mario.transform.position, Vector3.up, -5f);
            	//transform.SetPositionAndRotation(rightSide.transform.position, rightSide.transform.rotation);
            }

	}
}
