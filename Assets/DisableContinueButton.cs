using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableContinueButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Persistance.Load() == null)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
