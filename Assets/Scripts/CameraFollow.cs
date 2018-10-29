using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public Game.LevelPhase phase = Game.LevelPhase.PLATFORM;

    public float tunnelCameraOffset = 5f;
    public Vector3 platformCameraOffset = new Vector3(0,10f,0);

    float angle = 1;
    float tiltLimit = 30f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

        var moveDirection = new Vector3(-moveVertical, 0, moveHorizontal);

        transform.RotateAround(target.position, moveDirection, angle); // this tilts the camera toward ball movement direction

        Debug.Log(transform.eulerAngles);

        //this rotates the camera  around the ball, basically on an imaginary sphere within some distance around the ball
        platformCameraOffset = Quaternion.AngleAxis(angle, moveDirection) * platformCameraOffset;
        transform.position = target.position  + platformCameraOffset;

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
