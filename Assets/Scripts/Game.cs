using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game instance;

    public enum LevelPhase { PLATFORM, TUNNEL };

    public float timeSlowScale = 0.5f;
    public float levelTime = 0f;
    private int currentLevel = 0;
    private float timeCounterMultiplier = 1f;
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

    private void OnEnable()
    {
        EventManager.StartListening("timeSlow", OnTimeSlow);
        EventManager.StartListening("speed", OnSpeed);
        EventManager.StartListening("speedReset", OnSpeedReset);
    }

    private void OnTimeSlow(GameObject go)
    {
        if (go.GetComponent<Player>().isTimeSlow)
        {
            Debug.Log("Slowing down time");
            Time.timeScale = this.timeSlowScale;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Also decreasing total time when SpeedEffect is applied
    /// </summary>
    /// <param name="go"></param>
    private void OnSpeed(GameObject go)
    {
     
        Debug.Log("OnSpeedEffect");
        var speedMultiplier = go.GetComponent<SpeedEffect>().speedMultiplier;
        this.timeCounterMultiplier = 1f / speedMultiplier;

    }

    private void OnSpeedReset(GameObject go)
    {  
        Debug.Log("OnSpeedReset");
        this.timeCounterMultiplier = 1f;
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
        levelTime += ( Time.deltaTime * timeCounterMultiplier);
    }


    // Update is called once per frame
    void Update () {
	}



}
