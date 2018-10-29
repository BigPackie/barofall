using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    public float speed = 5f;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}


    void FixedUpdate()
    {
        Controll();
    }

    // Update is called once per frame
    void Update () {
		
	}

    
    private void Controll()
    {
        //TODO make check based on platform (mobile, PC)

        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical),ForceMode.Force);

    }
}
