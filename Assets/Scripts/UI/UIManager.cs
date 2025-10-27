using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private static readonly Dictionary<GameScreen, UIScreenController> _screens = new();

    private UIScreenController _currentScreen;

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
        foreach (UIScreenController screen in _screens.Values)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public void Register(GameScreen screenName, UIScreenController screenController)
    {
        if (!_screens.ContainsKey(screenName))
        {
            _screens.Add(screenName, screenController);
        }
    }

    public void Unregister(GameScreen screenName)
    {
        if (_screens.ContainsKey(screenName))
        {
            _screens.Remove(screenName);
        }
    }

    public void OpenScreen(GameScreen screenName)
    {
        if (_screens.TryGetValue(screenName, out UIScreenController screen))
        {
            screen.gameObject.SetActive(true);
            _currentScreen = screen;
        }
    }

    public void CloseScreen()
    {
        if (_currentScreen != null)
        {
            _currentScreen.gameObject.SetActive(false);
            _currentScreen = null;
        }
    }
}
