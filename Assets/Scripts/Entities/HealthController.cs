using UnityEngine;

public class HealthController : MonoBehaviour, IHealable, IDamageable
{
    [SerializeField] protected int entityHP = 100;
    [SerializeField] protected int maxHP = 100;

    public virtual void Heal(int hitPoints)
    {
        entityHP = Mathf.Min(entityHP + hitPoints, maxHP);
    }

    public virtual void Damage(int hitPoints)
    {
        entityHP -= hitPoints;

        if (entityHP <= 0)
        {
            Die();
        }
    }

    public virtual int GetMaxHP()
    {
        return maxHP;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
