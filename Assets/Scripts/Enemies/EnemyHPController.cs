public class EnemyHPController : HealthController
{
  public event System.Action<int, int> OnHPChanged;

  void Start()
  {
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
  }
}
