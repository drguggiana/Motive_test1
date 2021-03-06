using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayControl_rat : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 3.0f;
    public Transform camObj;
    public float udRange = 60.0f;
    public float jumpSpeed = 20;

    float rotUD = 0;

    float vertVel = 0;
	private float old_rotation;

    CharacterController cc;


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();
		old_rotation = camObj.localRotation.eulerAngles.y;
		Camera cam = GetComponentInChildren<Camera>();
		cam.nearClipPlane = 0.0001f;


    }

    // Update is called once per frame
    void Update()
    {

        // Rotation
        float rotLR = Input.GetAxis("Mouse X") * mouseSensitivity;

        transform.Rotate(0, rotLR, 0);

        rotUD -= Input.GetAxis("Mouse Y");
        rotUD = Mathf.Clamp(rotUD, -udRange, udRange);

//        camObj.localRotation = Quaternion.Euler(rotUD, 0, 0);
		camObj.localRotation = Quaternion.Euler(rotUD, old_rotation, 0);

        // Movement

        float forwardSpeed = -Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

//		Debug.Log (forwardSpeed);

        vertVel += Physics.gravity.y * Time.deltaTime;

//		Debug.Log (vertVel);

//        if (cc.isGrounded && Input.GetButtonDown("Jump"))
//        {
//            vertVel = jumpSpeed;
//        }

		Vector3 speed = new Vector3(forwardSpeed, vertVel, sideSpeed);

//		Debug.Log (Physics.gravity.y);

        speed = transform.rotation * speed;

//		Debug.Log (speed);


//        cc.SimpleMove(speed);

        cc.Move(speed * Time.deltaTime);

    }
}
