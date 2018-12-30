using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    public CameraController cameraController;

    public Game.LevelPhase nextPhase;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if(cameraController == null)
        {
            return;
        }

        if (Game.instance.gameState.levelPhase == nextPhase)
        {
            return;
        }

        if (other.CompareTag("Player")){
            Game.instance.gameState.levelPhase = nextPhase;   
            cameraController.SwitchCamera(nextPhase);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
