using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public int degreesPerSecond = 180;  //positive number gives clockwise rotation
    Vector3 eulers;

	void Start () {
        eulers = new Vector3(0, degreesPerSecond / 60f, 0); //divide by 60 to get rotation per second
    }
	

	void FixedUpdate () {
        transform.Rotate(eulers, Space.Self);
	}


}
