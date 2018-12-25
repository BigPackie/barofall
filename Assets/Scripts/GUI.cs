using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GUI : MonoBehaviour {


    //gui components
    public Text totalTime;
    public Text effect;
    public GameObject pauseMenu;
    public GameObject pauseButton;
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
        totalTime.text = Game.instance.levelTime.ToString("00:00.00");
    }

    // Update is called once per frame
    void Update () {
     
    }


    public void OnPause(GameObject go)
    {
        Debug.Log("Pause");     
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Game.instance.Pause();
    }

    public void OnContinue(GameObject go)
    {
        Debug.Log("UnPause");      
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Game.instance.UnPause();
    }

    private void OnMainMenu(GameObject go)
    {
        Debug.Log("Navigate to MainMenu");
        Game.instance.GoToMainMenu();
    }

    private void OnRestartLevel(GameObject go)
    {
        Debug.Log("RestartLevel");
    }

    private void OnRestartFromCheckPoint(GameObject go)
    {
        Debug.Log("RestartFromCheckpoint");
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

}
