using UnityEngine;
using System.Collections;

public class PlayerHPController : HealthController
{
    private float _invincibilityDuration = 1.0f;
    private float _invincibilityTimer = 0.0f;

    void Start()
    {
        EventBus<PlayerHPChangedEvent>.Publish(GameEvent.PlayerHPChanged, new PlayerHPChangedEvent(entityHP, maxHP));
    }

    public override void Heal(int hitPoints)
    {
        base.Heal(hitPoints);
        EventBus<PlayerHPChangedEvent>.Publish(GameEvent.PlayerHPChanged, new PlayerHPChangedEvent(entityHP, maxHP));
        EventBus<None>.Publish(GameEvent.PlayerHealed);
    }

    public override void Damage(int hitPoints)
    {
        if (_invincibilityTimer == 0)
        {
            // Reduce player HP
            base.Damage(hitPoints);

            // Start invincibility frames
            StartCoroutine(TimeInvincibility());

            // Notify subscribers
            EventBus<PlayerHPChangedEvent>.Publish(GameEvent.PlayerHPChanged, new PlayerHPChangedEvent(entityHP, maxHP));
        }
    }

    public override void Die()
    {
        GameManager.Instance.LoseLevel();
    }

    // NOTE: Maybe take the time in Update instead of a coroutine
    private IEnumerator TimeInvincibility()
    {
        EventBus<None>.Publish(GameEvent.PlayerInvincibilityStarted);
        _invincibilityTimer = _invincibilityDuration;
        while (_invincibilityTimer > 0)
        {
            _invincibilityTimer -= Time.deltaTime;
            if (_invincibilityTimer < 0)
            {
                _invincibilityTimer = 0;
                EventBus<None>.Publish(GameEvent.PlayerInvincibilityEnded);
            }
            yield return null;
        }
    }
}
