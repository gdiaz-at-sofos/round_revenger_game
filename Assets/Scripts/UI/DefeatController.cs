using UnityEngine;
using UnityEngine.UI;

public class DefeatController : UIScreenController
{
    [Header("References")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnEnable()
    {
        UIManager.Instance.RegisterScreen(GameScreen.Defeat, this);
    }

    private void OnDestroy()
    {
        UIManager.Instance.UnregisterScreen(GameScreen.Defeat);
    }

    private void OnRestartButtonClicked()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnMainMenuButtonClicked()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
