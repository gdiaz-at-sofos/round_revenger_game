using UnityEngine;

[CreateAssetMenu(fileName = "StoryParameters", menuName = "Parameters/StoryParameters")]
public class StoryParameters : ScriptableObject
{
    [Header("Text")]
    public string storyStartTextA = "YOU ARE ROUND THEY ARE EDGED";
    public string storyStartTextB = "SHAPE THEM UP OR SHUT THEM DOWN";
    public string storyEndText = "The End";
}

