using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public enum LevelPhase { PLATFORM,TUNNEL };

    public static Game instance;

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
        DontDestroyOnLoad(gameObject); // this is to preserve this isntanc though different scenes

        InitGame();
    }

    //getting references here
    void InitGame()
    {
        Debug.Log("starting the game");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
