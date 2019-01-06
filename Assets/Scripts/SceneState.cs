using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState: MonoBehaviour {

    public static SceneState instance = null;
    public bool continueGame = false;
    public bool ignoreFirstCheckpoint = false; //cause if we continue game the scene is reloaded and we activate the same checkpoint, we don't want that
    public bool fromLevel = false;
    public bool newGame = false;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;         
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
