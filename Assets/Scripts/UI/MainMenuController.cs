using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : UIScreenController
{
    [Header("References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
    }

    private void OnExitButtonClicked()
    {
        GameManager.Instance.ExitGame();
    }
}
