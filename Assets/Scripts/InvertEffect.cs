using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertEffect : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }


        var player = other.gameObject.GetComponent<Player>();
        player.inverted = true;
        Debug.Log("Entered inverted collectible trigger");

    }
}
