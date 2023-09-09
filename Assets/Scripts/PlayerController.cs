using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5;
    [SerializeField] float sprintSpeed = 20;
    [SerializeField] float acceleration = 10;
    [SerializeField] float deceleration = 100;
    [SerializeField] float jumpForce = 10;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = Vector3.up * jumpForce;
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 target = rb.velocity.y * Vector3.up + (x * transform.right + z * transform.forward).normalized * speed;
        rb.velocity = Vector3.Lerp(rb.velocity, target, acceleration * Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position + Vector3.up * 0.2f, 0.4f, groundLayer);
    }
}
