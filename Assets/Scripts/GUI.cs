using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GUI : MonoBehaviour {


    //gui components
    public Text actualLevelTime;
    public Text effect;
    public Text scoreLevel;
    public Text scoreRestarts;
    public Text scoreLevelTime;
    public Text scoreTotalTime;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject hud;
    public GameObject checkpointLabel;
    //gui components end

    public float checkpointTimeout = 3f;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        EventManager.StartListening("pause", OnPause);
        EventManager.StartListening("continue", OnContinue);
        EventManager.StartListening("restartFromCheckPoint", OnRestartFromCheckPoint);
        EventManager.StartListening("restartLevel", OnRestartLevel);
        EventManager.StartListening("mainMenu", OnMainMenu);
        EventManager.StartListening("effect", OnEffect);
        EventManager.StartListening("OnLevelEnd", OnLevelEnd);
        EventManager.StartListening("checkpoint", OnCheckpoint);
    }


    private void OnDisable()
    {
        EventManager.StopListening("pause", OnPause);
        EventManager.StopListening("continue", OnContinue);
        EventManager.StopListening("restartFromCheckPoint", OnRestartFromCheckPoint);
        EventManager.StopListening("restartLevel", OnRestartLevel);
        EventManager.StopListening("mainMenu", OnMainMenu);
        EventManager.StopListening("effect", OnEffect);
        EventManager.StopListening("OnLevelEnd", OnLevelEnd);
        EventManager.StopListening("checkpoint", OnCheckpoint);
    }


    // Use this for initialization
    void Start () {
    }

    private void FixedUpdate()
    {
        actualLevelTime.text = TimeFormatter.FormatTime(Game.instance.levelTime);
    }

    // Update is called once per frame
    void Update () {
    }


    public void OnPause(GameObject go)
    {
        Debug.Log("Pause");     
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        hud.SetActive(false);
        RefreshScore();
        Game.instance.Pause();
    }

    public void OnContinue(GameObject go)
    {
        Debug.Log("UnPause");      
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        hud.SetActive(true);
        Game.instance.UnPause();
    }

    private void OnMainMenu(GameObject go)
    {
        Debug.Log("Navigate to MainMenu");
        Game.instance.GoToMainMenu();
    }

    //maybe we can do the same way as continue from main menu, which mean realoading the scene
    //if we do it that way the collectibles and ball state are reset too, which is good;
    private void OnRestartLevel(GameObject go)
    {
        Debug.Log("RestartLevel");
        this.OnContinue(go);
        SceneState.instance.continueGame = true;
        SceneState.instance.fromLevel = true;    
        SceneManager.LoadScene(1);       
    }

    private void OnRestartFromCheckPoint(GameObject go)
    {
        Debug.Log("RestartFromCheckpoint");
        this.OnContinue(go);
        SceneState.instance.continueGame = true;
        SceneState.instance.ignoreFirstCheckpoint = true;
        SceneManager.LoadScene(1);       
    }

    private void OnEffect(GameObject go)
    {
        if(go == null)
        {
            effect.text = "";
        }
        else
        {
            effect.text = go.GetComponent<Collectible>().lable;
        }
        
    }

    private void OnLevelEnd(GameObject go)
    {
        Debug.Log("Level ended");
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        hud.SetActive(false);
        RefreshScore(true);
        Game.instance.Pause();       
    }


    private void OnCheckpoint(GameObject go)
    {
        Debug.Log("New checkpoint reached.");
        checkpointLabel.SetActive(true);
        StartCoroutine(HideCheckpointText());
    }

    private IEnumerator HideCheckpointText()
    {
        yield return new WaitForSeconds(checkpointTimeout);
        checkpointLabel.SetActive(false);
    }

    private void RefreshScore(bool levelend = false)
    {
        var level = Game.instance.gameState.currentLevel;
        level = levelend ? level - 1 : level;
        scoreLevelTime.text = actualLevelTime.text;
        scoreTotalTime.text = TimeFormatter.FormatTime(Game.instance.gameState.totalTime + Game.instance.levelTime); //actual total time;
        scoreLevel.text = "Level " + level;
        scoreRestarts.text = Game.instance.gameState.restarts.ToString();
    }

}
