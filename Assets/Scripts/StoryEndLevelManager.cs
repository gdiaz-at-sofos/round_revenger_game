using UnityEngine;
using System.Collections;

public class StoryEndLevelManager : LevelManager
{
    [Header("Parameters")]
    [SerializeField] private StoryParameters storyParams;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(StorySequence());
    }

    private IEnumerator StorySequence()
    {
        yield return StartCoroutine(UIManager.Instance.ShowTitle(storyParams.storyEndTextA));
        yield return StartCoroutine(UIManager.Instance.ShowTitle(storyParams.storyEndTextB));
        GameManager.Instance.LoadMainMenu();
    }
}
