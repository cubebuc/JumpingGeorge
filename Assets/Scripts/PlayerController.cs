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
    [SerializeField] float groundCheckRadius = 0.4f;

    [Header("Wallrun Check")]
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckRadius = 0.55f;
    [SerializeField] float wallrunDelay = 0.5f;
    [SerializeField] float walljumpForce = 5f;

    Rigidbody rb;
    CapsuleCollider capsuleCollider;

    bool canWallrun;
    public bool ghostMode = false;

    public void SetGhostMode(bool newState)
    {
        rb.useGravity = !newState;
        capsuleCollider.enabled = !newState;
        ghostMode = newState;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        ghostMode = false;
        canWallrun = true;
    }

    void Update()
    {
        Collider wallCollider = IsWallrunning();
        if (!ghostMode && Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || wallCollider))
        {
            rb.velocity = Vector3.up * jumpForce;

            if (wallCollider)
            {
                Vector3 force = wallCheck.position - wallCollider.ClosestPointOnBounds(wallCheck.position);
                rb.velocity += force * walljumpForce;

                StartCoroutine(IWallrunDelay());
            }
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 target = rb.velocity.y * Vector3.up + (x * transform.right + z * transform.forward).normalized * speed;

        if (canWallrun && IsWallrunning())
        {
            target.y *= 0.2f;
        }
            
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
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    Collider IsWallrunning()
    {
        Collider[] colliders = Physics.OverlapSphere(wallCheck.position, wallCheckRadius, wallLayer);
        return colliders.Length == 0 ? null : colliders[0];
    }

    IEnumerator IWallrunDelay()
    {
        canWallrun = false;
        yield return new WaitForSeconds(wallrunDelay);
        canWallrun = true;
    }
}
