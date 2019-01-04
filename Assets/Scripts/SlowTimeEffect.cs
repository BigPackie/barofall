using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }


        var player = other.gameObject.GetComponent<Player>();
        player.isTimeSlow = true;
        EventManager.TriggerEvent("timeSlow", other.gameObject);
        Debug.Log("Entered slowtime collectible trigger");

    }
}
