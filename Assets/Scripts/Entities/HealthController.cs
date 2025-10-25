using UnityEngine;

public class HealthController : MonoBehaviour, IHealable, IDamageable
{
    protected int entityHP;
    protected int maxHP;

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

    public virtual int GetCurrentHP()
    {
        return entityHP;
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
