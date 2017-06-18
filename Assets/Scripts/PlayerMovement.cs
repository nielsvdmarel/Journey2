using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour
{
    private InputDevice Controller;
    [SerializeField]
    float moveSpeed = 6f;            // Player's speed when walking.
    [SerializeField]
    float rotationSpeed = 6f;
    [SerializeField]
    float jumpHeight = 10f;         // How high Player jumps

    Vector3 moveDirection;

    Rigidbody rb;

   
    void Awake()
    {
        Controller = InputManager.ActiveDevice;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float x = Controller.LeftStickX;
        float y = 0;
        float z = Controller.LeftStickY;
        transform.position += new Vector3(x,y,z);

        if (Controller.Action1)
        {
            transform.position += new Vector3(0, 1, 0);
        }
     
        
    }

}