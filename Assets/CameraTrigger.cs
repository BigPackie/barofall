using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    public CameraController cameraController;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            cameraController.SwitchCamera();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
