using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraFollow : MonoBehaviour {

    private InputDevice Controller;
    public float CameraMoveSpeed = 120.0f;
	public GameObject CameraFollowObj;
	Vector3 FollowPOS;
	public float clampAngle = 80.0f;
	public float inputSensitivity = 150.0f;
    Vector3 velocity = Vector3.zero;
    public float camDistanceXToPlayer;
	public float camDistanceYToPlayer;
	public float camDistanceZToPlayer;
	public float mouseX;
	public float mouseY;

	private float finalInputX;
	private float finalInputZ;
	
	private float rotY = 0.0f;
	private float rotX = 0.0f;


    void Awake()
    {
        Controller = InputManager.ActiveDevice;
    }

    void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	
	void Update () {

		// We setup the rotation of the sticks here
		//float inputX = Input.GetAxis ("RightStickHorizontal");
        float inputX = Controller.RightStick.X;
		//float inputZ = Input.GetAxis ("RightStickVertical");
        float inputZ = Controller.RightStick.Y;
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;

		rotY += finalInputX * inputSensitivity * Time.deltaTime;
		rotX += finalInputZ * inputSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
		transform.rotation = localRotation;


	}

	void LateUpdate () {
		CameraUpdater ();
	}

	void CameraUpdater() {
		// set the target object to follow
		Transform target = CameraFollowObj.transform;

		//move towards the game object that is the target
		float step = CameraMoveSpeed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
        transform.position = Vector3.SmoothDamp(transform.position, target.position,
                                             ref velocity, 1f);
    }
}
