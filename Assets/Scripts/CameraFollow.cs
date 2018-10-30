using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public Game.LevelPhase phase = Game.LevelPhase.PLATFORM;

    public float tunnelCameraOffset = 5f;
    public Vector3 platformCameraOffset = new Vector3(0, 10f, 0);
   

    float angle = 2;

    Vector3 moveDirection;
    float[] moveHorizontal;
    float[] moveVertical;

    Stack directionHistory = new Stack();
    Quaternion defaultRotation = new Quaternion(0.7f, 0, 0, 0.7f);
    float rotationLimit = 0.200f;
    Quaternion oldRotation;
    Quaternion newRotation;

    Vector3 orbitPositionOffset;
    Vector3 oldOrbitPositionOffset;
    Vector3 newOrbitPositionOffset;



    struct KeyCounter
    {
        public int vertical;
        public int horizontal;
    };

    KeyCounter keyCounter;

    // Use this for initialization
    void Start()
    {

        keyCounter.vertical = 0;
        keyCounter.horizontal = 0;
        orbitPositionOffset = platformCameraOffset;

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


        Vector3 oldMoveDirection = moveDirection;

        moveDirection = new Vector3(-moveVertical, 0, moveHorizontal);

        bool horFlag = false;
        bool verFlag = false;
    

        oldRotation = transform.rotation;
        newRotation = transform.rotation;

        /*
        //this is still bugged, cause dues the axises warp on them self, but if I only use Rotatearound than it is not looking at the object.

        //looking in the direction of the ball;

        //transform.rotation.x, and transform.rotation.y are the first two postions in quaternion, it does not necessary mean it represents axis,
        //cause x and w (4th postion) are changing at the same rate but in different direction(++ or --)
        // y,z are changing at the same rate in the same direction

        //horizontal movement
        if (transform.rotation.y < defaultRotation.y + rotationLimit)
        {
           if(moveHorizontal > 0)
            {
                // transform.Rotate(0, 0, moveHorizontal * angle, Space.World);
                 transform.RotateAround(transform.position, new Vector3(0, 0, moveHorizontal), angle);
                //transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0,0,-1f));
                //this rotates the camera  around the ball (makes orbit, basically on an imaginary sphere within some distance around the ball  
                orbitPositionOffset = Quaternion.AngleAxis(angle, new Vector3(0, 0, moveHorizontal)) * orbitPositionOffset;
            }
       
        }


        if (transform.rotation.y > defaultRotation.y - rotationLimit)
        {
            if (moveHorizontal < 0)
            {
                //transform.Rotate(0, 0, moveHorizontal * angle, Space.World);
                transform.RotateAround(transform.position, new Vector3(0, 0, moveHorizontal), angle);
              //  transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0, 0, -1f));
                //this rotates the camera  around the ball (makes orbit, basically on an imaginary sphere within some distance around the ball  
                orbitPositionOffset = Quaternion.AngleAxis(angle, new Vector3(0, 0, moveHorizontal)) * orbitPositionOffset;
            }
        }

        //vertical movement

        if (transform.rotation.x < defaultRotation.x + rotationLimit)
        {
            if (moveVertical < 0)
            {
                //transform.Rotate(-moveVertical * angle, 0, 0, Space.Self);
                transform.RotateAround(transform.position, new Vector3(-moveVertical, 0, 0), angle);
             //   transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0, 0, -1f));
                //this rotates the camera  around the ball (makes orbit, basically on an imaginary sphere within some distance around the ball  
                orbitPositionOffset = Quaternion.AngleAxis(angle, new Vector3(-moveVertical, 0, 0)) * orbitPositionOffset;
            }

        }

        if (transform.rotation.x > defaultRotation.x - rotationLimit)
        {
            if (moveVertical > 0)
            {
                //transform.Rotate(-moveVertical * angle, 0, 0, Space.Self);
                 transform.RotateAround(transform.position, new Vector3(-moveVertical, 0, 0), angle);
              //  transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0, 0, -1f));
                //this rotates the camera  around the ball (makes orbit, basically on an imaginary sphere within some distance around the ball  
                orbitPositionOffset = Quaternion.AngleAxis(angle, new Vector3(-moveVertical, 0, 0)) * orbitPositionOffset;
            }
        }

    */

        //This fucking works very nice below, but how to limit it?
        //also the order of these opeartion is important
        //also multiplyinh Quaternion by Vector is a shortcut for doing a rotation on Vector.
        //first get to the correct orbit postion around the ball
        orbitPositionOffset = Quaternion.AngleAxis(angle, moveDirection) * orbitPositionOffset;
        transform.position = target.position + orbitPositionOffset;
        //now turn the camera towards the ball , the second parameter "Upwards direction" is important, if specified badly, camera will swap one of its axes orintation
        //first param is the worward vector, which way we have to look
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0, 0, 1f));
    
       
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
