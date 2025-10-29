using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

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

  /**
  * Scene Management
  */

  public void RestartLevel()
  {
    ResumeGame();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void LoadMainMenu()
  {
    ResumeGame();
    SceneManager.LoadScene(GameScene.MainMenu.ToString());
  }

  public void LoadNextLevel()
  {
    ResumeGame();
    GameScene currentScene = (GameScene)Enum.Parse(typeof(GameScene), SceneManager.GetActiveScene().name);
    GameScene nextScene = currentScene + 1;
    SceneManager.LoadScene(nextScene.ToString());
  }

  public void ExitGame()
  {
    Application.Quit();
  }

  /**
  * Pause
  */

  void OnPause()
  {
    if (isPaused)
    {
      UIManager.Instance.OpenScreen(GameScreen.Pause);
      PauseGame();
    }
    else
    {
      UIManager.Instance.CloseScreen();
      ResumeGame();
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

  /**
  * Input Management
  */

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
}
