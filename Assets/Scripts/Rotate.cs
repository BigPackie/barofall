using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public int degreesPerSecond = 180;  //positive number gives clockwise rotation
    public Vector3 rotationAxix = new Vector3(0, 1, 0);
    float rotation;

	void Start () {
        rotation = degreesPerSecond / 60f ; //divide by 60 to get rotation per second
    }
	

	void FixedUpdate () {
        transform.Rotate(rotationAxix*rotation, Space.Self);
	}


}
