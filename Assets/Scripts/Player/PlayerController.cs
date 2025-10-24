using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private PlayerParameters playerParams;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D feet;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GunController gun;
    [SerializeField] private SpriteRenderer sprite;

    private Vector2 _moveHorizontal;
    private Vector2 _moveVertical;

    private bool _isOnGround = false;
    private bool _isJumpPressed = false;

    private float _coyoteTimeCounter = 0f;
    private float _jumpTimeCounter = 0f;

    private Coroutine _blinkCoroutine;

    void OnEnable()
    {
        EventBus<None>.Subscribe(GameEvent.PlayerInvincibilityStarted, OnPlayerInvincibilityStarted);
        EventBus<None>.Subscribe(GameEvent.PlayerInvincibilityEnded, OnPlayerInvincibilityEnded);
    }

    void OnDisable()
    {
        EventBus<None>.Unsubscribe(GameEvent.PlayerInvincibilityStarted, OnPlayerInvincibilityStarted);
        EventBus<None>.Unsubscribe(GameEvent.PlayerInvincibilityEnded, OnPlayerInvincibilityEnded);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on a valid ground platform
        _isOnGround = Physics2D.BoxCast(
            new Vector2(feet.bounds.center.x, feet.bounds.center.y - feet.bounds.extents.y),
            feet.size,
            playerParams.groundCheckAngle,
            Vector2.down,
            playerParams.groundCheckDistance,
            groundLayer
        );

        // Handle coyote time
        if (_isOnGround)
        {
            _coyoteTimeCounter = playerParams.coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        // Keep track of button pressed
        if (_isJumpPressed)
        {
            _jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            _jumpTimeCounter = playerParams.jumpTime;
        }
    }


    void FixedUpdate()
    {
        // Movement only on x-axis
        if (_moveHorizontal != Vector2.zero && Mathf.Abs(rb.velocity.x) <= playerParams.maxSpeed)
        {
            rb.AddForce(_moveHorizontal, ForceMode2D.Impulse);
        }

        // Add opposing force to slow down the player
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            rb.AddForce(new Vector2(-rb.velocity.x * playerParams.dampingFactor, 0f), ForceMode2D.Impulse);
        }

        // Jump only on y-axis
        if (_moveVertical.y > 0f && _isJumpPressed && _jumpTimeCounter > 0f)
        {
            rb.AddForce(_moveVertical * playerParams.jumpRate, ForceMode2D.Impulse);
            _moveVertical -= _moveVertical * playerParams.jumpRate;
            _coyoteTimeCounter = 0f;
        }
    }

    /**
     * Events tied to Update
     */

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _moveHorizontal = playerParams.speedForce * Time.fixedDeltaTime * new Vector2(inputVector.x, 0f);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && _coyoteTimeCounter > 0f)
        {
            _moveVertical = playerParams.jumpForce * Time.fixedDeltaTime * Vector2.up;
            _isJumpPressed = true;
        }
        else
        {
            _isJumpPressed = false;
        }
    }

    void OnShoot(InputValue value)
    {
        gun.Shoot();
    }

    /**
     * Event Handlers
     */

    private void OnPlayerInvincibilityStarted()
    {
        _blinkCoroutine = StartCoroutine(BlinkSprite(sprite));
    }

    private void OnPlayerInvincibilityEnded()
    {
        StopCoroutine(_blinkCoroutine);
        sprite.enabled = true;
    }

    private IEnumerator BlinkSprite(SpriteRenderer spriteR)
    {
        // NOTE: Again, maybe use Update instead of a coroutine
        while (true)
        {
            spriteR.enabled = !spriteR.enabled;
            yield return new WaitForSeconds(playerParams.blinkOutDuration);
        }
    }
}
