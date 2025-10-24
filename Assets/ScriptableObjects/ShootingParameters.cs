using UnityEngine;

[CreateAssetMenu(fileName = "ShootingParameters", menuName = "Parameters/ShootingParameters")]
public class ShootingParameters : ScriptableObject
{
    [Header("Bullet")]
    public float lifetime = 5f;

    [Header("Pool")]
    public int prewarmCount = 50;
    public int maxPoolSize = 200;
}

