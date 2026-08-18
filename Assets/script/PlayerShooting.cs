using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxAmmo = 5;
    [SerializeField] private TMPro.TextMeshProUGUI ammoText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoText.text = "Ammo: " + currentAmmo;
    }

    void Update()
    {
        RotateTowardsMouse();

        if ((Mouse.current.leftButton.wasPressedThisFrame ||
            Keyboard.current.spaceKey.wasPressedThisFrame) &&
            currentAmmo > 0)
        {
            Shoot();
        }
    }

    void RotateTowardsMouse()
    {
        Vector2 direction = GetDirectionToMouse();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    Vector2 GetDirectionToMouse()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition =
            Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.z = 0f;

        return (mouseWorldPosition - transform.position).normalized;
    }

    void Shoot()
    {
        currentAmmo--;
        audioSource.PlayOneShot(shootSound);

        ammoText.text = "Ammo: " + currentAmmo;

        Vector2 direction = GetDirectionToMouse();

        GameObject bullet =
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        BulletMovement bulletMovement =
            bullet.GetComponent<BulletMovement>();

        bulletMovement.SetDirection(direction);
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;

        ammoText.text = "Ammo: " + currentAmmo;
    }
}