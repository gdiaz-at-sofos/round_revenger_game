using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float speedForce = 80f;
    [SerializeField] private float maxSpeed = 35f;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float jumpRate = 0.2f;
    [SerializeField] private float dampingFactor = 0.2f;
    [SerializeField] private float coyoteTime = 0.2f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D feet;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GunController gun;

    private Vector2 _moveHorizontal;
    private Vector2 _moveVertical;

    private bool _isOnGround = false;
    private bool _isJumpPressed = false;

    private float _coyoteTimeCounter = 0f;
    private float _jumpTimeCounter = 0f;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on a valid ground platform
        _isOnGround = Physics2D.BoxCast(
            new Vector2(feet.bounds.center.x, feet.bounds.center.y - feet.bounds.extents.y),
            feet.size,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );

        // Handle coyote time
        if (_isOnGround)
        {
            _coyoteTimeCounter = coyoteTime;
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
            _jumpTimeCounter = jumpTime;
        }
    }


    void FixedUpdate()
    {
        // Movement only on x-axis
        if (_moveHorizontal != Vector2.zero && Mathf.Abs(rb.velocity.x) <= maxSpeed)
        {
            rb.AddForce(_moveHorizontal, ForceMode2D.Impulse);
        }

        // Add opposing force to slow down the player
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            rb.AddForce(new Vector2(-rb.velocity.x * dampingFactor, 0f), ForceMode2D.Impulse);
        }

        // Jump only on y-axis
        if (_moveVertical.y > 0f && _isJumpPressed && _jumpTimeCounter > 0f)
        {
            rb.AddForce(_moveVertical * jumpRate, ForceMode2D.Impulse);
            _moveVertical -= _moveVertical * jumpRate;
            _coyoteTimeCounter = 0f;
        }
    }

    /**
     * Events tied to Update
     */

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _moveHorizontal = speedForce * Time.fixedDeltaTime * new Vector2(inputVector.x, 0f);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && _coyoteTimeCounter > 0f)
        {
            _moveVertical = jumpForce * Time.fixedDeltaTime * new Vector2(0f, 1f);
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
}
