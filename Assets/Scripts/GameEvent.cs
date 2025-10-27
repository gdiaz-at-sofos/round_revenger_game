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