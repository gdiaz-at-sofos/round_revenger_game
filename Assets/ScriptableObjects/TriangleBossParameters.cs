using UnityEngine;

[CreateAssetMenu(fileName = "TriangleBossParameters", menuName = "Parameters/TriangleBossParameters")]
public class TriangleBossParameters : ScriptableObject
{
    [Header("Opening")]
    public float initialChargeTime = 1f;

    [Header("AI")]
    public float idleTime = 1.5f;
    public float chargeTime = 2f;
    public int numberOfCharges = 3;
    public int numberOfBumps = 3;

    [Header("Animations")]
    public float timeToRotate = 1f;
    public float timeToMovePerUnit = 1f;
    public float chargeDistance = 100f;
    public float bounceBackDuration = 0.5f;

    [Header("Damage")]
    public int damage = 1;
}

