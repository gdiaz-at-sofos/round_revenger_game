using UnityEngine;

public class TriangleLevelManager : LevelManager, IWinLoseable
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(UIManager.Instance.ShowTitle(TriangleBossManager.Instance.GetBossName()));
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
