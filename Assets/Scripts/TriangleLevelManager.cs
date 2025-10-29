using UnityEngine;

public class TriangleLevelManager : LevelManager, IWinLoseable
{
    private const string TRIANGLE_LEVEL_KEY = "TRIANGLE_LEVEL_PASSED";

    protected override void Start()
    {
        base.Start();
        StartCoroutine(UIManager.Instance.ShowTitle(TriangleBossManager.Instance.GetBossName()));
    }

    public void WinLevel()
    {
        UIManager.Instance.OpenScreen(GameScreen.Victory);
        GameManager.Instance.PauseGame();
        PlayerPrefs.SetInt(TRIANGLE_LEVEL_KEY, 1);
        PlayerPrefs.Save();
    }

    public void LoseLevel()
    {
        UIManager.Instance.OpenScreen(GameScreen.Defeat);
        GameManager.Instance.PauseGame();
    }
}
