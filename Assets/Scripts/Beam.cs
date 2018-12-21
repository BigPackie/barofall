using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    public int force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        BeamCollision(other);
    }

    //triggered as often as FixedUpdate
    private void OnTriggerStay(Collider other)
    {
        BeamCollision(other);
    }


    void BeamCollision(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Force); //up is forward for the beam
        }
    }
}
