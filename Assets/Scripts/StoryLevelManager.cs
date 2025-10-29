using UnityEngine;
using System.Collections;

public class StoryLevelManager : LevelManager
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
        yield return StartCoroutine(UIManager.Instance.ShowTitle(storyParams.storyStartTextA));
        yield return StartCoroutine(UIManager.Instance.ShowTitle(storyParams.storyStartTextB));
        GameManager.Instance.LoadNextLevel();
    }
}
