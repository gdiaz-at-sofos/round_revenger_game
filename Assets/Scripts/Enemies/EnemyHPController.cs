using UnityEngine;

public class EnemyHPController : HealthController
{
  public event System.Action<int, int> OnHPChanged;

  [Header("References")]
  [SerializeField] private EnemyParameters enemyParams;

  void Start()
  {
    entityHP = enemyParams.entityHP;
    maxHP = enemyParams.maxHP;
    OnHPChanged?.Invoke(entityHP, maxHP);
  }

  public override void Damage(int hitPoints)
  {
    base.Damage(hitPoints);
    OnHPChanged?.Invoke(entityHP, maxHP);
  }

  public override void Die()
  {
    base.Die();
    // NOTE: Should do this on a child class
    EventBus<None>.Publish(GameEvent.BossDefeated);
  }
}
