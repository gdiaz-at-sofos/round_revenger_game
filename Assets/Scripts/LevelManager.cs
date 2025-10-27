using UnityEngine;

public class LevelManager : MonoBehaviour
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

    private void Start()
    {
        EventBus<None>.Publish(GameEvent.LevelStarted);
    }

    public void WinLevel()
    {
        UIManager.Instance.OpenScreen(GameScreen.Victory);
        GameManager.Instance.PauseGame();
    }

    public void LoseLevel()
    {
        UIManager.Instance.OpenScreen(GameScreen.Defeat);
        GameManager.Instance.PauseGame();
    }
}
