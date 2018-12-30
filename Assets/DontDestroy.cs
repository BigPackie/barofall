using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    // Use this for initialization

    private static DontDestroy instance;

    private DontDestroy() { }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
      
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
