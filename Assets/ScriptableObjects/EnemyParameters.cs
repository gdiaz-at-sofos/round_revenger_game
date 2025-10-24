using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParameters", menuName = "Parameters/EnemyParameters")]
public class EnemyParameters : ScriptableObject
{
    [Header("Health")]
    public int entityHP = 100;
    public int maxHP = 100;
}

