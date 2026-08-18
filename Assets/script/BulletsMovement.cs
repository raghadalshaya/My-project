using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Bullet speed

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        // Get the Rigidbody 2D attached to the bullet
        rb = GetComponent<Rigidbody2D>();
    }

    // Called right after the bullet is created, to set its direction
    public void SetDirection(Vector2 dir)
    {
        // Store the direction of the bullet
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FixedUpdate()
    {
        // Move the bullet using Rigidbody 2D
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    // Called automatically by Unity when this Trigger touches another Collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision with the player (the one who shot it)
        if (other.CompareTag("Player"))
            return;

        // Check if the object we hit has an EnemyHealth component
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        // If the object has EnemyHealth, deal 1 damage to it
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(1);
        }

        // Destroy the bullet whenever it touches anything
        Destroy(gameObject);
    }
}