using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraControl : MonoBehaviour {
   float mouseSensitivity = 125f;
    public GameObject mario;
    //public GameObject zoomOut;
    //float scrollSensitivity = 100f;


	void Update () {
       float mouseX = Input.GetAxis("Mouse X");
        //transform.position = Vector3.Lerp(zoomOut.transform.position, mario.transform.position, Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity *Time.deltaTime);

        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.RotateAround(mario.transform.position, Vector3.up, mouseX * mouseSensitivity* Time.deltaTime);
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 20)
            {
                Camera.main.fieldOfView-=2;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView < 100)
            {
                Camera.main.fieldOfView+=2;
            }
        }
	}
}
