using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour {

    public bool isLevelStart;
    public bool isLevelEnd; //if end show scoreboard (basically pause menu)
    public bool visited = false; // when already visited, and we do restart from this checkpoint, we don't trigger some things.
    public Vector3 spawnOffset = new Vector3(0, 2f, 0);
    public float levelTimeStamp { get; private set; }
    public int level; //current or starting level,  end checkpoint of one level is also the start checkpoint of the next, in this case level nubmer represents the next level

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !visited)
        {
            SaveCheckpoint();
        }
    }

    public void ResetCheckpoint()
    {
        visited = false;
        levelTimeStamp = 0f;
    }

    private void SaveCheckpoint()
    {

        Debug.Log("Checkpoint reached.");
        if (SceneState.instance.ignoreFirstCheckpoint)
        {
            //because if the checkopoints are refreshed after loading the scene again, we would rewrite our save.
            Debug.Log("Ignoring first checkpoint");
            SceneState.instance.ignoreFirstCheckpoint = false;
            return;
        }

        this.levelTimeStamp = Game.instance.levelTime;
        this.visited = true;

        Game.instance.visitedCheckpoints.Push(this); //saving checkpoint into not persistant memory


        if (isLevelStart)
        {
            Game.instance.NewLevel(this);          
        }

        if (!isLevelStart && !isLevelEnd)
        {
            EventManager.TriggerEvent("checkpoint", gameObject);
        }

        if (isLevelEnd)
        {
            //we don't want to trigger level end event after level restart;
            if (Game.instance.afterLevelRestart)
            {
                Game.instance.afterLevelRestart = false;
            }
            else
            {
                Game.instance.LevelFinished(this);
                Game.instance.SaveGameState(); //savign state on every checkpoint reached 
                EventManager.TriggerEvent("OnLevelEnd");
                return;
            }
            
        }

        Game.instance.SaveGameState(); //savign state on every checkpoint reached 
    }
}
