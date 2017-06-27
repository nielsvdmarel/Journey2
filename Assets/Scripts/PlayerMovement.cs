using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour
{
    private InputDevice Controller;
    [SerializeField]
    private float moveSpeed = 6f;            
    [SerializeField]
    private float rotationSpeed = 6f;
    [SerializeField]
    private float jumpHeight = 10f;         
   
    Rigidbody rb;
    [SerializeField]
    private bool isGrounded;
    private Vector3 jump = new Vector3(0,1,0);
    [SerializeField]
    float x;
    [SerializeField]
    float z;


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
        x =Mathf.Round(Controller.LeftStickX *moveSpeed);
        z = Mathf.Round(Controller.LeftStickY * moveSpeed);
        rb.AddForce(x/20, 0, z/20, ForceMode.Impulse);
        
        if (Controller.Action1 && isGrounded)
        {
            rb.AddForce(jump * jumpHeight, ForceMode.Impulse);
            
        }
        
        
    }

    //If collision player is grounded
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    //If no collision player is not grounded
    void OnCollisionExit()
    {
        isGrounded = false;
    }

}