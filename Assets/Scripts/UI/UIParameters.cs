using UnityEngine;

[CreateAssetMenu(fileName = "UIParameters", menuName = "Parameters/UIParameters")]
public class UIParameters : ScriptableObject
{
    [Header("Animations")]
    public float titleFadeTime = 1f;
    public float titleVisibleTime = 2f;
}

