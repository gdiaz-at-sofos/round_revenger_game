using UnityEngine;
using UnityEngine.UI;

public class VictoryController : UIScreenController
{
    [Header("References")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnEnable()
    {
        UIManager.Instance.RegisterScreen(GameScreen.Victory, this);
    }

    private void OnDestroy()
    {
        UIManager.Instance.UnregisterScreen(GameScreen.Victory);
    }

    private void OnContinueButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
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
