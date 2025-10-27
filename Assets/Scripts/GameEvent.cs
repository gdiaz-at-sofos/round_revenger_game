public enum GameEvent
{
    LevelStarted,
    BossStarted,
    PlayerHPChanged,
    PlayerHealed,
    PlayerDamaged,
    PlayerInvincibilityStarted,
    PlayerInvincibilityEnded,
    BossDefeated,
    BossHPChanged,
}

public interface IGameEvent { }

public class PlayerHPChangedEvent : IGameEvent
{
    public int CurrentHP { get; }
    public int MaxHP { get; }

    public PlayerHPChangedEvent(int currentHP, int maxHP)
    {
        CurrentHP = currentHP;
        MaxHP = maxHP;
    }
}

public class BossHPChangedEvent : IGameEvent
{
    public int CurrentHP { get; }
    public int MaxHP { get; }

    public BossHPChangedEvent(int currentHP, int maxHP)
    {
        CurrentHP = currentHP;
        MaxHP = maxHP;
    }
}