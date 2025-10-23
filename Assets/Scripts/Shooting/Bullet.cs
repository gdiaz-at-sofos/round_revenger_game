using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private int _damageHP = 0;
    private float _lifetime = 5f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Decrease lifetime
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0f)
        {
            _rb.velocity = Vector2.zero;
            BulletManager.Instance.Return(this);
        }
    }

    public void Fire(Vector3 position, Vector2 direction, float speedForce, int gunDamage)
    {
        // Apply force to the bullet in the specified direction
        Vector2 moveForce = speedForce * direction.normalized;
        _rb.AddForce(moveForce, ForceMode2D.Impulse);

        // Set the bullet's position
        transform.position = position;

        // Set the damage
        _damageHP = gunDamage;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(_damageHP);
        }

        _rb.velocity = Vector2.zero;
        BulletManager.Instance.Return(this);
    }
}
