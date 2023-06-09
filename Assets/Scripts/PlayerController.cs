using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Movement variables
    public float moveSpeed = 5.0f;
    public float jumpForce = 500.0f;
    public bool canMove = true;

    private Rigidbody rigidBody;
    private Animator animator;
    private Camera mainCamera;

    public static PlayerController Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the input axes
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        Vector3 movementDirection = cameraForward * verticalAxis + cameraRight * horizontalAxis;
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1.0f);

        if(canMove)
        {
            // Calculate the movement vector
            Vector3 movementVector = movementDirection * moveSpeed;

            // Apply the movement vector to the rigidbody
            rigidBody.velocity = new Vector3(movementVector.x, rigidBody.velocity.y, movementVector.z);

            // Set the animator parameters
            // animator.SetFloat("Horizontal", horizontalAxis);
            // animator.SetFloat("Vertical", verticalAxis);
            // animator.SetBool("IsGrounded", IsGrounded());

            // Check if the jump button is pressed
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                // Apply the jump force to the rigidbody
                rigidBody.AddForce(new Vector3(movementVector.x, jumpForce + rigidBody.velocity.y, movementVector.z));
                GameManager.Instance.PlayVE(1);
            }
        }
    }

    bool IsGrounded()
    {
        // Check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 1.0f);
    }
}

