using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelCamera : MonoBehaviour {

    public float tunnelCameraOffset = 5f;
    public Transform target;
    // Use this for initialization
    void Start () {
        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        Tunnel();
    }

    void Tunnel()
    {
        if (target == null) return;

        transform.position = new Vector3(target.position.x, target.position.y + tunnelCameraOffset, target.position.z);
    }
}
