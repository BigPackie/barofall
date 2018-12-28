using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool isLevelStart;
    public bool isLevelEnd; //if end show scoreboard (basically pause menu)
    public bool visited = false; // when already visited, and we do restart from this checkpoint, we don't trigger some things.
    public Vector3 spawnOffset = new Vector3(0,2f,0);
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
            EventManager.TriggerEvent("checkpoint", gameObject);
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
        this.levelTimeStamp = Game.instance.levelTime;
        this.visited = true;

        Game.visitedCheckpoints.Push(this);
        if (isLevelStart)
        {
            EventManager.TriggerEvent("OnLevelChange");
            Game.instance.NewLevel(this.level);
            
        }

        if (!isLevelStart && isLevelEnd)
        {
            //TODO: last checkpoint reached, end game.
        }
        
    }
}
