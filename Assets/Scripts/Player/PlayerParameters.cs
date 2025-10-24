using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "Parameters/PlayerParameters")]
public class PlayerParameters : ScriptableObject
{
    [Header("Physics")]
    public float mass = 5f;
    public float linearDrag = 0.75f;
    public float angularDrag = 0.05f;
    public float gravityScale = 1.6f;

    [Header("Movement")]
    public float movementSpeedForce = 80f;
    public float maxSpeed = 35f;
    public float dampingFactor = 0.2f;

    [Header("Jumping")]
    public float jumpForce = 800f;
    public float jumpTime = 0.5f;
    public float jumpRate = 0.2f;
    public float coyoteTime = 0.2f;
    public float groundCheckAngle = 0f;
    public float groundCheckDistance = 0.1f;

    [Header("Animations")]
    public float blinkOutDuration = 0.1f;

    [Header("Damage")]
    public int gunDamage = 1;
    public float gunSpeedForce = 20f;
    public float gunShootCooldown = 0.55f;

    [Header("Health")]
    public int entityHP = 5;
    public int maxHP = 5;
    public float invincibilityDuration = 1.0f;
}
