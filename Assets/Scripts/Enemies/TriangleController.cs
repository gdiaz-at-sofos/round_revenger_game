using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TriangleController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float timeToRotate = 1f;
    [SerializeField] private float timeToMovePerUnit = 1f;
    [SerializeField] private float chargeTime = 10f;
    [SerializeField] private float chargeDistance = 100f;
    [SerializeField] private float bounceBackDuration = 0.5f;

    [Header("References")]
    [SerializeField] private LayerMask collisionLayer;

    private Tween _chargeTween;
    private bool _hasWallBeenHit = false;

    public float ChargeTime
    {
        get { return chargeTime; }
        set { chargeTime = value; }
    }

    public IEnumerator RotateTowards(Vector3 direction, Ease ease)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        transform.DORotateQuaternion(targetRotation, timeToRotate).SetEase(ease);

        yield return new WaitForSeconds(timeToRotate);
    }

    public IEnumerator Move(Vector3 direction, float distance, Ease ease)
    {
        Vector3 targetPosition = transform.position + direction.normalized * distance;
        float duration = distance * timeToMovePerUnit;

        transform.DOMove(targetPosition, duration).SetEase(ease);

        yield return new WaitForSeconds(duration);
    }

    public IEnumerator MoveTo(Vector3 targetPosition, Ease ease)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float duration = distance * timeToMovePerUnit;

        transform.DOMove(targetPosition, duration).SetEase(ease);

        yield return new WaitForSeconds(duration);
    }

    public IEnumerator Charge(Vector3 direction, Ease ease)
    {
        _hasWallBeenHit = false;

        // Move a large distance so we definitely hit something, but can cancel early
        Vector3 target = transform.position + direction.normalized * chargeDistance;

        _chargeTween = transform.DOMove(target, chargeTime)
            .SetEase(ease)
            .OnKill(() => _chargeTween = null);

        // Wait until a wall is hit or the tween is killed
        while (!_hasWallBeenHit && _chargeTween != null)
        {
            yield return null;
        }

        if (_hasWallBeenHit)
        {
            // Tween back a bit to represent bouncing off the wall
            Vector3 bounceBackPosition = transform.position - direction.normalized;
            transform.DOMove(bounceBackPosition, bounceBackDuration).SetEase(Ease.OutQuad);
        }

        // Kill the tween if it's still running
        if (_chargeTween != null)
        {
            _chargeTween.Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) & collisionLayer) != 0)
        {
            _hasWallBeenHit = true;
        }
    }

}
