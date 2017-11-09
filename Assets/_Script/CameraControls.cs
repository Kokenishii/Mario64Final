using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
    	//make a control scheme for the camera such that you can:
    	//zoom the camera in and out from mario
    	//rotate the camera freely degrees along mario
    	//make mario unable to move and then you can move the camera around freely?
	// Use this for initialization
    public GameObject mario;
    public GameObject zoomIn;
    public GameObject zoomOut;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject defaultZoom; 
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
			transform.SetPositionAndRotation(defaultZoom.transform.position, defaultZoom.transform.rotation);
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
                        transform.SetPositionAndRotation(defaultZoom.transform.position, defaultZoom.transform.rotation);
                        zoomDistance = 0;
                    }

       }

        //rotate around mario freely
            if(Input.GetKey(KeyCode.Alpha3))
            {
		    
		   
            transform.RotateAround(mario.transform.position, Vector3.up, 150f * Time.deltaTime); 
            	//transform.SetPositionAndRotation(leftSide.transform.position, leftSide.transform.rotation);
            }

        //rotate around mario freely the other way
            if(Input.GetKey(KeyCode.Alpha4))
            {
            transform.RotateAround(mario.transform.position, Vector3.up, -150f * Time.deltaTime);
            	//transform.SetPositionAndRotation(rightSide.transform.position, rightSide.transform.rotation);
            }

	}
}
