using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game instance;

    public enum LevelPhase { PLATFORM, TUNNEL };

    public float levelTime = 0f;
    private int currentLevel = 0;
    private GameObject levelStart;
    private float lastCheckpointTime = 0f;
    private GameObject lastCheckpoint;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //maybe not needed as I only going to have 1 scene
        DontDestroyOnLoad(gameObject); // this is to preserve this isntance through different scenes

        InitGame();
    }

    //getting references here
    void InitGame()
    {
        Debug.Log("starting the game");
        EventManager.TriggerEvent("gameStart");
    }

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        levelTime += Time.deltaTime;
    }


    // Update is called once per frame
    void Update () {
	}


    public void test()
    {

    }
}
