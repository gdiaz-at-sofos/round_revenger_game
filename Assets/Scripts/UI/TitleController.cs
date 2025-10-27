using TMPro;
using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour
{
  [Header("References")]
  [SerializeField] private TextMeshProUGUI text;

  private float _timeElapsed = 0f;

  public void SetTitle(string title)
  {
    text.text = title;
  }

  public IEnumerator FadeInAndOut(float fadeTime, float visibleTime)
  {
    // Fade In
    _timeElapsed = 0f;
    while (_timeElapsed < fadeTime)
    {
      _timeElapsed += Time.deltaTime;
      text.alpha = Mathf.Clamp01(_timeElapsed / fadeTime);
      yield return null;
    }

    // Display
    yield return new WaitForSeconds(visibleTime);

    // Fade Out
    _timeElapsed = 0f;
    while (_timeElapsed < fadeTime)
    {
      _timeElapsed += Time.deltaTime;
      text.alpha = 1f - Mathf.Clamp01(_timeElapsed / fadeTime);
      yield return null;
    }
  }
}
