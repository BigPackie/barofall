using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventTrigger : MonoBehaviour
{

    public string hello = "I am the event Trigger";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed key 'E'");
            EventManager.TriggerEvent("test");
            EventManager.TriggerEvent("test2");
            EventManager.TriggerEvent("testWithGO", this.gameObject);
        }

    }

}
