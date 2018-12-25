using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//3D mesh model by sirkitree https://sketchfab.com/models/2d4f7dde4bda4987960f73a8a711ace7

public class Player : MonoBehaviour {

    public Rigidbody rb;
    Renderer renderer;

    public Material opaque;
    public Material fade;

    float turnSpeed = 5f;
    Color color;

    public float rollTurnSpeed = 5f;
    public float fallTurnSpeed = 50f;

    private float effectDurationCounter;
    private float effectDuration;
    private bool isActiveEffect = false;

    public bool inverted = false;
    public bool beamImmune = false;
    public bool isTimeSlow = false;


    //this drag is used to simulate friction on platform surfaces
    public float ballDrag = 0.2f;

    public float massOriginal { get; private set; }
    public float rollTurnSpeedOriginal { get; private set; }
    public float fallTurnSpeedOriginal { get; private set; }
    public Vector3 gravityOriginal { get; private set; }


    // Use this for initialization
    void Start () {
        turnSpeed = rollTurnSpeed;
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;
    
        rb.drag = ballDrag;
        rb.angularDrag = ballDrag;
        massOriginal = rb.mass;
        fallTurnSpeedOriginal = this.fallTurnSpeed;
        rollTurnSpeedOriginal = this.rollTurnSpeed;
        gravityOriginal = Physics.gravity;
	}


    void FixedUpdate()
    {
        Controll();
    }

    // Update is called once per frame
    void Update () {
        this.CalculateEffectDuration();
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

    private void CalculateEffectDuration()
    {
        if (!this.isActiveEffect)
        {
            return;
        }

        if (this.effectDurationCounter >= this.effectDuration)
        {
            this.ResetDefaultState();
            Debug.Log("Effect Duration Over");
            EventManager.TriggerEvent("effect"); //clear the hud 
        }
        else
        {
            this.effectDurationCounter += Time.deltaTime;
        }
    }

    public void ActivateEffect(float duration)
    {
        Debug.Log("Activating effect");
        Debug.Log("Actual durationCounter: " + this.effectDurationCounter);
        Debug.Log("Actual duration: " + this.effectDuration);

        this.ResetDefaultState();
        this.effectDurationCounter = 0f;
        this.effectDuration = duration;
        this.isActiveEffect = true;
    }

    /// <summary>
    /// resets default state of the ball, mass, drag....
    /// </summary>
    public void ResetDefaultState()
    {
        Debug.Log("Resetting default state.");
        this.isActiveEffect = false;
        this.effectDurationCounter = 0f;
        this.effectDuration = 0f;

        this.rb.mass = this.massOriginal;
        this.inverted = false;
        this.beamImmune = false;
        this.isTimeSlow = false;
        this.fallTurnSpeed = this.fallTurnSpeedOriginal;
        this.rollTurnSpeed = this.rollTurnSpeedOriginal;
        Physics.gravity = gravityOriginal;
        EventManager.TriggerEvent("timeSlow", gameObject);
        EventManager.TriggerEvent("speedReset");

        //send message of slowtime
    }

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
                //Debug.Log("Surface" + surface.type);
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

        float moveHorizontal = Input.GetAxis("Horizontal") * turnSpeed * (inverted ? -1 : 1);
        float moveVertical = Input.GetAxis("Vertical") * turnSpeed * (inverted ? -1 : 1);

        rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical),ForceMode.Force);

    }

  


  
}
