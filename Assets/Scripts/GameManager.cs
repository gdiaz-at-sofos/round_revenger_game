using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  /* [SerializeField] private PauseMenuController pauseMenu;
  [SerializeField] private VictoryMenuController victoryMenu;
  [SerializeField] private DefeatMenuController defeatMenu; */

  private bool isPaused = false;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
  }


  void OnPause()
  {
    if (isPaused)
    {
      // pauseMenu.OpenPauseMenu();
    }
    else
    {
      // pauseMenu.ClosePauseMenu();
    }
  }

  public void PauseGame()
  {
    isPaused = !isPaused;
    Time.timeScale = 0f;
    DisablePlayerInputs();
  }

  public void ResumeGame()
  {
    isPaused = !isPaused;
    Time.timeScale = 1f;
    EnablePlayerInputs();
  }

  public void RestartGame()
  {
    ResumeGame();
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
  }

  public void ExitGame()
  {
    Application.Quit();
  }

  void DisablePlayerInputs()
  {
    foreach (PlayerInput playerInput in FindObjectsOfType<PlayerInput>())
    {
      if (playerInput.gameObject == this.gameObject)
      {
        continue;
      }
      playerInput.DeactivateInput();
    }
  }

  void EnablePlayerInputs()
  {
    foreach (PlayerInput playerInput in FindObjectsOfType<PlayerInput>())
    {
      if (playerInput.gameObject == this.gameObject)
      {
        continue;
      }
      playerInput.ActivateInput();
    }
  }

  public void WinLevel()
  {
    // victoryMenu.OpenVictoryMenu();
  }

  public void LoseLevel()
  {
    // defeatMenu.OpenDefeatMenu();
  }
}
