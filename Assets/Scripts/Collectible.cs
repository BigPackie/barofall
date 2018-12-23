using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public float duration = 5f;
    Rigidbody rb;

    int rotX;
    int rotY;
    int rotZ;
    Vector3 rotation;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rotX = Random.Range(-3, 3);
        rotY = Random.Range(-3, 3);
        rotZ = Random.Range(-3, 3);
        rotation = new Vector3(rotX, rotY, rotZ);
    }

    private void FixedUpdate()
    {
        // rb.rotation = Random.rotation;
        transform.Rotate(rotation, Space.Self);
    }

    // Update is called once per frame
    void Update () {
		
	}

    //this gets called before the other onTriggerEnter methods from differents scripts
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        other.gameObject.GetComponent<Player>().ActivateEffect(this.duration);
        Debug.Log("Destroying Collectible.");
        Destroy(gameObject);
        Debug.Log("Collectible destroyed.");
    }
}
