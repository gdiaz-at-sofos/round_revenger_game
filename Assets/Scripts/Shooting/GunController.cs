using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerParameters playerParams;

    private float _lastShootTime = -0.55f;
    private Vector3 _mousePosition;

    private void Update()
    {
        // Update mouse position
        _mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // TODO: Change to Input System

        // Rotate gun towards mouse position
        Vector3 rotation = _mousePosition - transform.position;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Shoot()
    {
        // Don't shoot if the gun is still in cooldown
        if (Time.time - _lastShootTime < playerParams.gunShootCooldown)
        {
            return;
        }

        Vector2 direction = (_mousePosition - muzzle.position).normalized;
        Bullet bullet = BulletManager.Instance.Get();
        bullet.Fire(muzzle.position, direction, playerParams.gunSpeedForce, playerParams.gunDamage);

        _lastShootTime = Time.time;
    }
}
