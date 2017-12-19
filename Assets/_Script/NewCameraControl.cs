using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraControl : MonoBehaviour {
   float mouseSensitivity = 25f;
    public GameObject mario;
    //public GameObject zoomOut;
    //float scrollSensitivity = 100f;

    //new camera script that relies only on the mouse instead of buttons
	void Update () {
       float mouseX = Input.GetAxis("Mouse X");
        //transform.position = Vector3.Lerp(zoomOut.transform.position, mario.transform.position, Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity *Time.deltaTime);

        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.RotateAround(mario.transform.position, Vector3.up, mouseX * mouseSensitivity* Time.deltaTime);
        }
       
        //if click, lock mouse into screen
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        //if press escape unlock mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 20)
            {
                Camera.main.fieldOfView-=2;
            }
        }
        //zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView < 100)
            {
                Camera.main.fieldOfView+=2;
            }
        }
        //if falling to death rotate camera so facing downward
        if (DeathBox.resetting
			&& PlayerHealthAndPickups.deadFromEnemies == false)
        {
           // transform.eulerAngles.Set(90f,transform.rotation.y, transform.rotation.z);
            transform.rotation = Quaternion.Euler(90,transform.rotation.y,transform.rotation.z);
        }
       
	}
}
