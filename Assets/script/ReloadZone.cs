using UnityEngine;

public class ReloadZone : MonoBehaviour
{
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();
            audioSource.PlayOneShot(reloadSound);
            playerShooting.Reload();
        }
    }
}