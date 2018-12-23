using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassEffect : MonoBehaviour {

    public float massMultiplier = 3f;


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
        player.rb.mass = player.massOriginal * massMultiplier;
        Debug.Log("Entered mass collectible trigger");

    }
}
