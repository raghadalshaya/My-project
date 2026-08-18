using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Enemy movement speed
    [SerializeField] private Transform player; // Reference to the player

    private Rigidbody2D rb;

    private float damageTimer = 0f; // Timer for damage cooldown
    private bool touchingPlayer = false; // Checks if enemy is touching the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Calculate direction from enemy to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move enemy towards the player
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // Rotate enemy to face the player (sprite faces DOWN by default, hence +90 offset)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90f;
    }

    void Update()
    {
        // Check if the enemy is touching the player
        if (touchingPlayer)
        {
            // Increase the timer
            damageTimer += Time.deltaTime;

            // Deal damage every 3 seconds
            if (damageTimer >= 3f)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                // Check if the player has PlayerHealth
                if (playerHealth != null)
                {
                    // Deal 1 damage to the player
                    playerHealth.TakeDamage(1);
                }

                // Reset the timer
                damageTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get PlayerHealth from the object we collided with
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        // Check if the object is the player
        if (playerHealth != null)
        {
            // Deal 1 damage immediately
            playerHealth.TakeDamage(1);

            // Start the damage timer
            touchingPlayer = true;
            damageTimer = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Get PlayerHealth from the object we stopped touching
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        // Check if the object is the player
        if (playerHealth != null)
        {
            // Stop dealing damage
            touchingPlayer = false;

            // Reset the timer
            damageTimer = 0f;
        }
    }
}