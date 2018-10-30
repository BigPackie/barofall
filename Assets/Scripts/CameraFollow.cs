using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public Game.LevelPhase phase = Game.LevelPhase.PLATFORM;

    public float tunnelCameraOffset = 5f;
    public Vector3 platformCameraOffset = new Vector3(0, 10f, 0);
   

    public float angleStep = 2f;

    Vector3 moveDirection;
    Quaternion defaultRotation;
    Quaternion previousRotation;
    public float rotationLimit = 30f;

    Vector3 orbitPositionOffset;
    Vector3 previousOrbitPositionOffset;
    Vector3 previousPosition;


    // Use this for initialization
    void Start()
    {
        orbitPositionOffset = platformCameraOffset;
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
   
    }

    private void LateUpdate()
    {
        if (phase == Game.LevelPhase.PLATFORM)
        {
            Platform();
        }
        else if (phase == Game.LevelPhase.TUNNEL)
        {
            Tunnel();
        }

    }

    void Platform()
    {
        if (target == null) return;


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(-moveVertical, 0, moveHorizontal);


        previousRotation = transform.rotation;
        previousOrbitPositionOffset = orbitPositionOffset;
        previousPosition = transform.position;

        //the order of these opeartion is important
        //also multiplying Quaternion by Vector is a shortcut for doing a rotation on Vector.
        //first get to the correct orbit postion around the ball
        orbitPositionOffset = Quaternion.AngleAxis(angleStep, moveDirection) * orbitPositionOffset;
        transform.position = target.position + orbitPositionOffset;
        //now turn the camera towards the ball , the second parameter "Upwards direction" is important, if specified badly, camera will swap one of its axes orintation
        //first param is the worward vector, which way we have to look
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0, 0, 1f));
       
        float angle = Quaternion.Angle(defaultRotation, transform.rotation);

        if (angle > rotationLimit) {
            transform.rotation = previousRotation;
            orbitPositionOffset = previousOrbitPositionOffset;
            transform.position = target.position + previousOrbitPositionOffset;
        }
       
    }


    void Tunnel()
    {
        if (target == null) return;

        var cameraAdjustVector = new Vector3(transform.position.x, target.position.y + tunnelCameraOffset, transform.position.z);
        transform.position = cameraAdjustVector;
    }


    public void SetCameraStartingPosition(Vector3 position)
    {
        //TODO: invoke this method based on a trigger for each level phase. Basically get the starting position of level phase
        //and adjust the starting postion of the camare for each phase.
    }
}
