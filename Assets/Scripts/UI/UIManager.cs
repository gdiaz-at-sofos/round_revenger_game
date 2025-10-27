using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private static readonly Dictionary<GameScreen, UIScreenController> _screens = new();

    [Header("References")]
    [SerializeField] private UIParameters UIParams;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TitleController titlePrefab;

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

    /**
     * Screen Management
     */

    public void RegisterScreen(GameScreen screenName, UIScreenController screenController)
    {
        if (!_screens.ContainsKey(screenName))
        {
            _screens.Add(screenName, screenController);
        }
    }

    public void UnregisterScreen(GameScreen screenName)
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

    /**
     * Title Management
     */

    public void ShowTitle(string titleText)
    {
        TitleController title = Instantiate(titlePrefab, mainCanvas.transform);
        title.SetTitle(titleText);
        StartCoroutine(FadeTitle(title));
    }

    private IEnumerator FadeTitle(TitleController title)
    {
        yield return title.FadeInAndOut(UIParams.titleFadeTime, UIParams.titleVisibleTime);
        Destroy(title.gameObject);
    }
}
