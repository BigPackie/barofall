﻿using System;
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
    //gui components end



    private UnityAction<GameObject> someListener;

    private void Awake()
    {
        someListener = (go) => Debug.Log("Fired event test from gui ");
    }

    private void OnEnable()
    {
        EventManager.StartListening("test", someListener);
        EventManager.StartListening("pause", OnPause);
        EventManager.StartListening("continue", OnContinue);
        EventManager.StartListening("restartFromCheckPoint", OnRestartFromCheckPoint);
        EventManager.StartListening("restartLevel", OnRestartLevel);
        EventManager.StartListening("mainMenu", OnMainMenu);
        EventManager.StartListening("effect", OnEffect);
        EventManager.StartListening("OnLevelStart", OnLevelChange);
        EventManager.StartListening("OnLevelEnd", OnLevelEnd);
    }


    private void OnDisable()
    {
        EventManager.StopListening("test", someListener);
        EventManager.StopListening("pause", OnPause);
        EventManager.StopListening("continue", OnContinue);
        EventManager.StopListening("restartFromCheckPoint", OnRestartFromCheckPoint);
        EventManager.StopListening("restartLevel", OnRestartLevel);
        EventManager.StopListening("mainMenu", OnMainMenu);
        EventManager.StopListening("effect", OnEffect);
        EventManager.StopListening("OnLevelChange", OnLevelChange);
        EventManager.StopListening("OnLevelEnd", OnLevelEnd);

    }


    private void TestFunction(GameObject go)
    {
        Debug.Log("Fired event test from gui without UnityAction");
    }

    // Use this for initialization
    void Start () {
    }

    private void FixedUpdate()
    {
        actualLevelTime.text = Game.instance.levelTime.ToString("00:00.00");
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

    private void OnLevelChange(GameObject go)
    {
        //
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

    private void RefreshScore(bool levelend = false)
    {
        var level = Game.instance.gameState.currentLevel;
        level = levelend ? level - 1 : level;
        scoreLevelTime.text = actualLevelTime.text;
        scoreTotalTime.text = (Game.instance.gameState.totalTime + Game.instance.levelTime).ToString("00:00.00"); //actual total time;
        scoreLevel.text = "Level " + level;
        scoreRestarts.text = Game.instance.gameState.restarts.ToString();
    }

}
