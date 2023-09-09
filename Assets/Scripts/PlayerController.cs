using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 5;
    [SerializeField] float sprintSpeed = 20;
    [SerializeField] float acceleration = 10;
    [SerializeField] float deceleration = 100;
    [SerializeField] float jumpForce = 10;

    [SerializeField] float ghostFlySpeed = 20;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    Rigidbody rb;
    CapsuleCollider capsuleCollider;

    bool ghostMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        ghostMode = false;
    }

    void Update()
    {
        if (!ghostMode && Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ghostMode = !ghostMode;
            rb.useGravity = !ghostMode;
            capsuleCollider.enabled = !ghostMode;
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 target = rb.velocity.y * Vector3.up + (x * transform.right + z * transform.forward).normalized * speed;

        //GHOST MODE INPUT
        if (ghostMode)
        {
            if (Input.GetKey(KeyCode.Space))
                target.y = ghostFlySpeed;
            else if (Input.GetKey(KeyCode.LeftControl))
                target.y = -ghostFlySpeed;
            else
                target.y = 0;
        }



        rb.velocity = Vector3.Lerp(rb.velocity, target, acceleration * Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position + Vector3.up * 0.2f, 0.4f, groundLayer);
    }
}
