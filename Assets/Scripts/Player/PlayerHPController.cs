using UnityEngine;

public class PlayerHPController : HealthController
{
    public static event System.Action<int, int> OnHPChanged;

    private float _invincibilityDuration = 1.0f;
    private float _invincibilityTimer = 0.0f;

    void Start()
    {
        OnHPChanged?.Invoke(entityHP, maxHP);
    }

    void Update()
    {
        if (_invincibilityTimer > 0)
        {
            _invincibilityTimer -= Time.deltaTime;
        }
    }

    public override void Heal(int hitPoints)
    {
        base.Heal(hitPoints);
        OnHPChanged?.Invoke(entityHP, maxHP);
    }

    public override void Damage(int hitPoints)
    {
        if (_invincibilityTimer == 0)
        {
            // Reduce player HP
            base.Damage(hitPoints);

            // Start invincibility frames
            _invincibilityTimer = _invincibilityDuration;

            // Notify listeners about HP change
            OnHPChanged?.Invoke(entityHP, maxHP);
        }
    }

    public override void Die()
    {
        GameManager.Instance.LoseLevel();
    }
}
