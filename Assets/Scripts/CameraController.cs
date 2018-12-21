using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour {

    public GameObject platformCamera;
    public GameObject tunnelCamera;


    // Use this for initialization
    void Start () {
        platformCamera.SetActive(true);
        tunnelCamera.SetActive(false);
	}
	
    public void SwitchCamera()
    {
        platformCamera.SetActive(!platformCamera.active);
        tunnelCamera.SetActive(!tunnelCamera.active);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
	}
}
