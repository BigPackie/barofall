using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    Renderer renderer;

    public Material opaque;
    public Material fade;

    float turnSpeed = 5f;
    Color color;

    public float rollTurnSpeed = 5f;
    public float fallTurnSpeed = 50f;

    //this drag is used to simulate friction on platform surfaces
    public float ballDrag = 0.2f;

	// Use this for initialization
	void Start () {
        turnSpeed = rollTurnSpeed;
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;
    
        rb.drag = ballDrag;
        rb.angularDrag = ballDrag;
	}


    void FixedUpdate()
    {
        Controll();
    }

    // Update is called once per frame
    void Update () {
		
	}

    /*

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            var surface = collision.gameObject.GetComponent<Surface>();
            if (surface)
            {
                Debug.Log("Surface enter" + surface.type);
                rb.drag = ballDrag * surface.drag;
            }
            
        }

    }

    */

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            var surface = collision.gameObject.GetComponent<Surface>();
            if (surface)
            {
                Debug.Log("Surface exit" + surface.type);
                rb.drag = ballDrag;
                rb.angularDrag = ballDrag;
            }

        }
    }

    

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {

            foreach (ContactPoint contact in collision.contacts)
            {
              //  Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
            }

            var surface = collision.gameObject.GetComponent<Surface>();
            if (surface)
            {
                Debug.Log("Surface" + surface.type);
                rb.drag = ballDrag * surface.drag;
                rb.angularDrag = ballDrag * surface.drag;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: CameraTrigger is maybe not the best name for this
        if (other.gameObject.CompareTag("CameraTrigger"))
        {
            if(other.gameObject.GetComponent<CameraTrigger>().nextPhase == Game.LevelPhase.TUNNEL)
            {
                Debug.Log("Setting fallspeed");
                turnSpeed = fallTurnSpeed;

                renderer.material = fade;
            }

            if (other.gameObject.GetComponent<CameraTrigger>().nextPhase == Game.LevelPhase.PLATFORM)
            {
                Debug.Log("Setting rollSpeed");
                turnSpeed = rollTurnSpeed;

                renderer.material = opaque;
            }

        }
    }

    private void Controll()
    {
        //TODO make check based on platform (mobile, PC)

        float moveHorizontal = Input.GetAxis("Horizontal") * turnSpeed;
        float moveVertical = Input.GetAxis("Vertical") * turnSpeed;

        rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical),ForceMode.Force);

    }
}
