using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour {

    public GameObject platformCamera;
    public GameObject tunnelCamera;


    // Use this for initialization
    void Start () {
        SwitchCamera(Game.instance.gameState.levelPhase);
	}
	
    public void SwitchCamera(Game.LevelPhase lp)
    {
        if(lp == Game.LevelPhase.PLATFORM)
        {
            platformCamera.SetActive(true);
            tunnelCamera.SetActive(false);
        }
        else
        {
            tunnelCamera.SetActive(true);
            platformCamera.SetActive(false);
        }
        
        
    }

    // Update is called once per frame
    void Update () {
	}
}
