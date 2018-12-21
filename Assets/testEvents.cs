using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testEvents : MonoBehaviour {

    public int number;

    private UnityAction<GameObject> someListener;

    private void Awake()
    {
        someListener = new UnityAction<GameObject>((obj) => Debug.Log("Fired event test " + number));
    }
    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        EventManager.StartListening("test", someListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("test", someListener);
    }

    // Update is called once per frame

}

