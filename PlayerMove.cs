using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float moveSpeed = 5f;          // Speed of the player

    [Header("Rotation Parameters")]
    public bool enableRotation = true;    // Toggle rotation to face movement direction
    public float rotationSpeed = 720f;    // Degrees per second

    // Private variables
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from keyboard (WASD or Arrow Keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement vector to ensure consistent speed when moving diagonally
        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }

        // Store the last non-zero movement direction for rotation
        if (movement != Vector2.zero)
        {
            lastMovement = movement;
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }

    void LateUpdate()
    {
        if (enableRotation && lastMovement != Vector2.zero)
        {
            // Calculate the target rotation based on last movement direction
            float targetAngle = Mathf.Atan2(lastMovement.y, lastMovement.x) * Mathf.Rad2Deg - 90f; // Adjusted for sprite orientation
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Optional: Visualize movement direction in the Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(lastMovement.x, lastMovement.y, 0));
    }
}
