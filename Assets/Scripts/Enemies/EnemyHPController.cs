using UnityEngine;

public class EnemyHPController : HealthController
{

  [Header("References")]
  [SerializeField] private EnemyParameters enemyParams;

  private void Awake()
  {
    entityHP = enemyParams.entityHP;
    maxHP = enemyParams.maxHP;
  }

  void Start()
  {
    EventBus<BossHPChangedEvent>.Publish(GameEvent.BossHPChanged, new BossHPChangedEvent(entityHP, maxHP));
  }

  public override void Damage(int hitPoints)
  {
    base.Damage(hitPoints);

    EventBus<BossHPChangedEvent>.Publish(GameEvent.BossHPChanged, new BossHPChangedEvent(entityHP, maxHP));
  }

  public override void Die()
  {
    base.Die();
    // NOTE: Should do this on a child class
    EventBus<None>.Publish(GameEvent.BossDefeated);
  }
}
