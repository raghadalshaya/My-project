using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Damage cooldown
    private float damageCooldown = 0.5f;
    private float damageTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        healthText.text = "Health: " + health;
    }

    void Update()
    {
        // Count down the damage cooldown
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        // Don't take damage if 0.5 seconds haven't passed
        if (damageTimer > 0f)
        {
            return;
        }

        // Apply damage
        health -= damage;

        // Start 0.5 second cooldown
        damageTimer = damageCooldown;

        // Play hurt sound
        audioSource.PlayOneShot(hurtSound);

        // Update health text
        healthText.text = "Health: " + health;

        // Flash red
        StartCoroutine(FlashRed());

        // Check if player died
        if (health <= 0)
        {
            GameManager.Instance.PlayerDied();
            Destroy(gameObject);
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = originalColor;
    }
}