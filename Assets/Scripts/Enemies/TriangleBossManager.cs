using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TriangleBossManager : MonoBehaviour
{
    public static TriangleBossManager Instance { get; private set; }

    [SerializeField] private TriangleController triangle;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Transform centerPosition;

    private bool _isBossDefeated = false;
    private int _chargeCount = 3;
    private int _bumpCount = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        EventBus<None>.Subscribe(GameEvent.LevelStarted, OnLevelStarted);
        EventBus<None>.Subscribe(GameEvent.BossStarted, OnBossStarted);
    }

    private void OnLevelStarted()
    {
        StartCoroutine(InitialCombatSequence());
    }

    private IEnumerator InitialCombatSequence()
    {
        // Transport to starting position
        triangle.transform.position = startingPosition.position;

        // Rotate downwards
        yield return StartCoroutine(triangle.RotateTowards(Vector3.down, Ease.InSine));

        // Charge
        triangle.ChargeTime = 1f;
        yield return StartCoroutine(triangle.Charge(Vector3.down, Ease.InSine));

        // Move to center
        yield return StartCoroutine(GoToCenterPositionSequence());

        // Idle
        yield return StartCoroutine(IdleSequence());

        EventBus<None>.Publish(GameEvent.BossStarted);
    }

    private void OnBossStarted()
    {
        StartCoroutine(BossAI());
    }

    private IEnumerator BossAI()
    {
        while (!_isBossDefeated)
        {
            yield return StartCoroutine(ChargeSequence());

            yield return StartCoroutine(GoToCenterPositionSequence());

            yield return StartCoroutine(IdleSequence());


            /* yield return StartCoroutine(BumpSequence());

            yield return StartCoroutine(IdleSequence()); */
        }
    }

    private IEnumerator IdleSequence()
    {
        // Return to original rotation
        yield return StartCoroutine(triangle.RotateTowards(Vector3.up, Ease.InOutSine));

        // Wait
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator GoToCenterPositionSequence()
    {
        // Move to center of the screen
        yield return StartCoroutine(triangle.MoveTo(centerPosition.position, Ease.InSine));
    }

    private IEnumerator ChargeSequence()
    {
        triangle.ChargeTime = 4f;

        for (int i = 0; i < _chargeCount; i++)
        {
            // Rotate towards player
            Vector3 direction = GetHorizontalDirection(PlayerManager.Instance.GetPlayerPosition() - triangle.transform.position);
            yield return StartCoroutine(triangle.RotateTowards(direction, Ease.InOutSine));

            // Charge
            yield return StartCoroutine(triangle.Charge(direction, Ease.InOutSine));

            // Idle for a bit
            yield return new WaitForSeconds(1f);

            // Move between lanes
            if (i % 2 == 0)
            {
                yield return StartCoroutine(triangle.Move(Vector3.down, 2f, Ease.InSine));
            }
            else
            {
                yield return StartCoroutine(triangle.Move(Vector3.up, 2f, Ease.InSine));
            }

        }
    }

    private Vector3 GetHorizontalDirection(Vector3 direction)
    {
        if (direction.x < 0)
        {
            return Vector3.left;
        }
        else
        {
            return Vector3.right;
        }
    }
}
