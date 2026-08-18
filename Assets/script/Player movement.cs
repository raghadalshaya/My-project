using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Player movement speed

    private Rigidbody2D rb; // Reference to the player's Rigidbody 2D
    private Vector2 movement; // Stores the direction of movement

    void Start()
    {
        // Get the Rigidbody 2D attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Reset movement every frame
        movement = Vector2.zero;

        // Check if the player is moving right
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            movement.x = 1f;

        // Check if the player is moving left
        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            movement.x = -1f;

        // Check if the player is moving up
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            movement.y = 1f;

        // Check if the player is moving down
        else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            movement.y = -1f;
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody 2D
        // FixedUpdate is used for physics-related movement
        rb.MovePosition(
            rb.position + movement * speed * Time.fixedDeltaTime
        );
    }
}