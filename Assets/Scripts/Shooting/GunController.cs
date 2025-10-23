using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private Camera mainCamera;

    [Header("Parameters")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float speedForce = 20f;
    [SerializeField] private float shootCooldown = 0.55f;

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
        if (Time.time - _lastShootTime < shootCooldown)
        {
            return;
        }

        Vector2 direction = (_mousePosition - muzzle.position).normalized;
        Bullet bullet = BulletManager.Instance.Get();
        bullet.Fire(muzzle.position, direction, speedForce, damage);

        _lastShootTime = Time.time;
    }
}
