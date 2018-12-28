using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static Game instance;

    public static Stack<Checkpoint> visitedCheckpoints = new Stack<Checkpoint>();

    public static bool paused = false; //the hole class static, this does not have to be anymore TODO: redo

    public enum LevelPhase { PLATFORM, TUNNEL };
    public GameObject player;

    private float timeScaleBeforePause = 1f; 
    public float timeSlowScale = 0.5f;
    public float levelTime { get; private set; }
    public float totalTime { get; private set; } //saved only when finishing a level;
    public int restarts { get; private set; }
    public int currentLevel = 0;
    private float timeCounterMultiplier = 1f;
    //private GameObject levelStart;
   // private float lastCheckpointTime = 0f;
    //private GameObject lastCheckpoint;


    private Game()
    {

    }

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
        if(currentLevel > 0)
        {
            levelTime += (Time.deltaTime * timeCounterMultiplier);
        }   
    }


    // Update is called once per frame
    void Update () {
        MenuControll();

	}

    /// <summary>
    /// Use this methods through GUI as there are some gui elements depending on it
    /// </summary>
    public void Pause()
    {
        paused = true;
        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Use this methods through GUI as there are some gui elements depending on it
    /// </summary>
    public void UnPause()
    {
        paused = false;
        Time.timeScale = timeScaleBeforePause;
    }

    public void GoToMainMenu()
    {
        UnPause();
        SceneManager.LoadScene(0);
    }

  
    private void MenuControll()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Game.paused)
            {
                EventManager.TriggerEvent("continue");
            }
            else
            {
                EventManager.TriggerEvent("pause");
            }
        }
    }

    public void RestartFromCheckPoint()
    {
        if(visitedCheckpoints.Count > 0)
        {
            var lastCheckpoint = visitedCheckpoints.Peek();

            this.RevertTime(lastCheckpoint);
            this.MovePlayerToCheckpoint(lastCheckpoint);
            restarts++;
        }
      
    }

    public void RestartLastLevel()
    {
        Checkpoint startCp;
        while (visitedCheckpoints.Count > 0)
        {
            startCp = visitedCheckpoints.Peek();
            if (startCp.isLevelStart)
            {
                this.RevertTime(startCp);
                this.MovePlayerToCheckpoint(startCp);
                break;
            }
            else
            {
                startCp.ResetCheckpoint();
                visitedCheckpoints.Pop();
            }
        }
        restarts++;
    }

    private void RevertTime(Checkpoint cp)
    {
        if (cp.isLevelStart)
        {
            this.levelTime = 0f; //because levels start end level end is same checopint, and I remeber the last current level timestamp at this
        }
        else
        {
            this.levelTime = cp.levelTimeStamp;
        }

    }

    public void MovePlayerToCheckpoint(Checkpoint cp)
    {
        this.player.transform.position = cp.transform.position + cp.spawnOffset;
        this.player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    public void NewLevel(int lvl)
    {
        this.currentLevel = lvl;
        this.totalTime += this.levelTime;
        this.levelTime = 0f;
    }
}
