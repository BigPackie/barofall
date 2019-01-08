using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var gs = Persistance.Load();

        if (gs == null || gs.levelScores.Count == 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
