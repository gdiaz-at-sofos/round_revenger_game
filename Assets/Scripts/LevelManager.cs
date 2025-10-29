using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    protected virtual void Start()
    {
        EventBus<None>.Publish(GameEvent.LevelStarted);
    }
}
