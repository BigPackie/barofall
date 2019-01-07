using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour {

    //affects speed on platform and tunnel, also affects gravity
    public float speedMultiplier = 2f;

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
        player.rollTurnSpeed = player.rollTurnSpeedOriginal * speedMultiplier;
        player.fallTurnSpeed = player.fallTurnSpeedOriginal * speedMultiplier;
        if (Game.instance.gameState.levelPhase == Game.LevelPhase.PLATFORM)
        {
            player.turnSpeed = player.rollTurnSpeed;
        }
        else
        {
            player.turnSpeed = player.fallTurnSpeed;
        }
        Physics.gravity = player.gravityOriginal * speedMultiplier;
        EventManager.TriggerEvent("speed", this.gameObject);
        Debug.Log("Entered speed collectible trigger");

    }
}
