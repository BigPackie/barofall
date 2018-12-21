using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GUI : MonoBehaviour {

    public GameObject management;

    //gui components
    public Text totalTime;
    //gui components end

    private Game myGame;

    private UnityAction<GameObject> someListener;

    private void Awake()
    {
        someListener = (go) => Debug.Log("Fired event test from gui ");
    }

    private void OnEnable()
    {
        EventManager.StartListening("test", someListener);
        EventManager.StartListening("test2", TestFunction);

        //listening to event with gameObject parameter
        EventManager.StartListening("testWithGO", OnTestWithGO);

    }


    private void OnDisable()
    {
        EventManager.StopListening("test", someListener);
        EventManager.StopListening("test2", TestFunction);

        //cancel listening to event with gameObject parameter
        EventManager.StopListening("testWithGO", OnTestWithGO);

    }

    private void TestFunction(GameObject go)
    {
        Debug.Log("Fired event test from gui without UnityAction");
    }

    public void OnTestWithGO(GameObject sender)
    {
        Debug.Log("Handling 'testWithGO' event in GUI from sender GameObject: " +sender.name);
        Debug.Log("sender saying hello: " + sender.GetComponent<EventTrigger>().hello);
    }

    // Use this for initialization
    void Start () {
        myGame = management.GetComponent<Game>();
    }

    private void FixedUpdate()
    {
        totalTime.text = myGame.levelTime.ToString("00:00.00");
    }

    // Update is called once per frame
    void Update () {
     
    }

}
