using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour {

    public static Game instance;

    public Stack<Checkpoint> visitedCheckpoints = new Stack<Checkpoint>();

    public bool paused = false; 

    public enum LevelPhase { PLATFORM, TUNNEL };
    public GameObject player;

   
    public Vector3 startPosition = new Vector3(0, 2, 0);

    private float timeScaleBeforePause = 1f; 
    public float timeSlowScale = 0.5f;
    public float levelTime { get; private set; }

    public bool afterLevelRestart = false;

    private float timeCounterMultiplier = 1f;
    //private GameObject levelStart;
    // private float lastCheckpointTime = 0f;
    //private GameObject lastCheckpoint;

    public GameStatePersisted gameState = new GameStatePersisted();

    private Game()
    {

    }

    private void Awake()
    {
        /*
        if(instance != null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // this is to preserve this isntance through different scenes
        }
        */

        //only singleton, does not preserve through scenes
        if(instance != null)
        {        
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    private void OnEnable()
    {
        EventManager.StartListening("timeSlow", OnTimeSlow);
        EventManager.StartListening("speed", OnSpeed);
        EventManager.StartListening("speedReset", OnSpeedReset);
    }

    // Use this for initialization
    void Start()
    {
        //this is needed in the Main Menu Scene
        if (SceneState.instance.continueGame)
        {
            SceneState.instance.continueGame = false;
            this.ContinueGame();
        }
        
        if (SceneState.instance.newGame)
        {
            SceneState.instance.newGame = false;
            this.NewGame();
        }

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
    private void NewGame()
    {
        Debug.Log("Starting new game");
        //load the defalut game state;
        //EventManager.TriggerEvent("gameStart");
        this.removeGameState();
    }

    private void ContinueGame()
    {
        Debug.Log("Continue the game");

        this.LoadGameState();

        if (SceneState.instance.fromLevel)
        {
            SceneState.instance.fromLevel = false;
            RestartLastLevel();
        }
        else
        {
            RestartFromCheckPoint();
        }

    }



    private void FixedUpdate()
    {
        if(gameState.currentLevel > 0)
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
            if (paused)
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
        if(gameState != null && gameState.lastCheckpoint != null)
        {
            this.RevertTime(gameState.lastCheckpoint);
            this.MovePlayerToCheckpoint(gameState.lastCheckpoint);
            gameState.restarts++;
        }    
    }

    public void RestartLastLevel()
    {
       
        if (gameState != null  && gameState.lastLevelCheckpoint != null)
        {
            this.afterLevelRestart = true;
            this.RevertTime(gameState.lastLevelCheckpoint);
            this.MovePlayerToCheckpoint(gameState.lastLevelCheckpoint);
            gameState.restarts++;
        }       
        
    }

    private void RevertTime(CheckpointState cp)
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

    public void MovePlayerToCheckpoint(CheckpointState cp)
    {
        this.player.transform.position = new Vector3(cp.position.x + cp.spawnOffset.x, cp.position.y + cp.spawnOffset.y, cp.position.z + cp.spawnOffset.z); 
        this.player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    public void NewLevel(Checkpoint cp)
    {
        if(cp.level == gameState.currentLevel)
        {
            return;
        }

        gameState.currentLevel = cp.level;
        gameState.totalTime += cp.levelTimeStamp;        
        //zero out time, cause new level begins
        this.levelTime = 0f;
    }


    public void LevelFinished(Checkpoint cp)
    {
        //saving score for the level
        var levelScore = new LevelScore();
        levelScore.levelFinished = cp.level - 1;
        levelScore.levelTime = cp.levelTimeStamp;
        levelScore.totalTime = gameState.totalTime;
        levelScore.restarts = gameState.restarts;
        gameState.levelScores.Add(levelScore);
    }

    public void SaveGameState()
    {

        Debug.Log("Saving game state");
        var gs = new GameStatePersisted();

        //saving persistante attributes
        gs.levelPhase = gameState.levelPhase;
        gs.totalTime = gameState.totalTime;
        gs.currentLevel = gameState.currentLevel;
        gs.restarts = gameState.restarts;

        //saving score
        gs.levelScores = gameState.levelScores;

        //saving checkpoints
        Checkpoint lastCheckpoint = visitedCheckpoints.Peek();
        if(lastCheckpoint != null)
        {
            var cp = new CheckpointState();
            cp.levelTimeStamp = lastCheckpoint.levelTimeStamp;
            cp.position.x = lastCheckpoint.transform.position.x;
            cp.position.y = lastCheckpoint.transform.position.y;
            cp.position.z = lastCheckpoint.transform.position.z;
            cp.spawnOffset.x = lastCheckpoint.spawnOffset.x;
            cp.spawnOffset.y = lastCheckpoint.spawnOffset.y;
            cp.spawnOffset.z = lastCheckpoint.spawnOffset.z;
            cp.isLevelStart = lastCheckpoint.isLevelStart;
            gs.lastCheckpoint = cp;
        }

       

        Checkpoint lastLevelCheckpoint = null;

        List<Checkpoint> list = new List<Checkpoint>(visitedCheckpoints);
        //list.Reverse();

         foreach (var cp in list) {
            if (cp.isLevelStart) {
                lastLevelCheckpoint = cp;
                break;
            }
         }
         
        if(lastLevelCheckpoint != null)
        {
            var cp = new CheckpointState();
            cp.levelTimeStamp = lastLevelCheckpoint.levelTimeStamp;
            cp.position.x = lastLevelCheckpoint.transform.position.x;
            cp.position.y = lastLevelCheckpoint.transform.position.y;
            cp.position.z = lastLevelCheckpoint.transform.position.z;
            cp.spawnOffset.x = lastLevelCheckpoint.spawnOffset.x;
            cp.spawnOffset.y = lastLevelCheckpoint.spawnOffset.y;
            cp.spawnOffset.z = lastLevelCheckpoint.spawnOffset.z;
            cp.isLevelStart = lastLevelCheckpoint.isLevelStart;
            gs.lastLevelCheckpoint = cp;
        }

        gameState = Persistance.Merge(gs);
        Persistance.Save(gameState);
    }

    public void LoadGameState()
    {
        Debug.Log("Loading game state.");
        var gs = Persistance.Load();

        if (gs == null)
        {
            Debug.Log("No game state to load.");
            return;
        }

        this.gameState = gs;

    }

    void removeGameState()
    {
        Debug.Log("Deleting game state.");
        this.gameState = new GameStatePersisted();
        Persistance.Remove();        
    }

}

[System.Serializable]
public class GameStatePersisted
{
    //public List<CheckpointState> visitedCheckpoints = new List<CheckpointState>();
    public CheckpointState lastLevelCheckpoint = null;
    public CheckpointState lastCheckpoint = null;
    public Game.LevelPhase levelPhase = Game.LevelPhase.PLATFORM;
    public float totalTime = 0f; //saved only when finishing a level;
    public int currentLevel = 0;
    public int restarts = 0;
    public List<LevelScore> levelScores = new List<LevelScore>();

}

[System.Serializable]
public class CheckpointState
{
    public bool isLevelStart;
    public float levelTimeStamp;
    public Vector3Ser spawnOffset = new Vector3Ser();
    public Vector3Ser position = new Vector3Ser();
}

[System.Serializable]
public class LevelScore
{
    public int restarts = 0;
    public int levelFinished = 0;
    public float levelTime = 0f;
    public float totalTime = 0f;

}

[System.Serializable]
public class Vector3Ser
{
    public float x, y, z;
}

