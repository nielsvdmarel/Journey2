using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 6f;            // Player's speed when walking.
    [SerializeField]
    float rotationSpeed = 6f;
    [SerializeField]
    float jumpHeight = 10f;         // How high Player jumps

    Vector3 moveDirection;

    Rigidbody rb;

    // Using the Awake function to set the references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float test = Input.GetAxis("PS4_R2");
        float hAxis = Input.GetAxis("Horizontal");
        Debug.Log(test);
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);
        rb.position += movement * moveSpeed * Time.deltaTime;
    }

}