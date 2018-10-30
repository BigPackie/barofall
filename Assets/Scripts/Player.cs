using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    public float speed = 5f;

    //this drag is used to simulate friction on platform surfaces
    public float ballDrag = 0.2f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
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
                Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
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

    private void Controll()
    {
        //TODO make check based on platform (mobile, PC)

        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical),ForceMode.Force);

    }
}
