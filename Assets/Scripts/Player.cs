using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1337;
    [SerializeField] float jumpForce = 10;

    [SerializeField] LayerMask jumpableLayers;
    [SerializeField] Vector3 groundCheckPosition;
    [SerializeField] float groundCheckRadius;

    bool isGrounded;
    Vector3 moveDirection;
    Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CalculateMovement();

        isGrounded = Physics.CheckSphere(transform.position + groundCheckPosition, groundCheckRadius, jumpableLayers);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void CalculateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moveDirection = Camera.main.transform.forward * z + Camera.main.transform.right * x;
        moveDirection = transform.forward * z + transform.right * x;
    }

    void ApplyMovement()
    {
        //playerRigidbody.velocity = moveDirection * playerSpeed * Time.deltaTime;
        playerRigidbody.velocity = new Vector3(moveDirection.normalized.x * playerSpeed * Time.deltaTime, playerRigidbody.velocity.y, moveDirection.normalized.z * playerSpeed * Time.deltaTime);
        if (isGrounded && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    void Jump()
    {
        playerRigidbody.velocity += new Vector3(0, jumpForce, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + groundCheckPosition, groundCheckRadius);
    }
}