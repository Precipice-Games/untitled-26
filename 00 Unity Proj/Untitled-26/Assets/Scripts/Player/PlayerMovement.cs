#if UNITY_EDITOR
using UnityEditor.Build;
#endif
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerMovement : MonoBehaviour
{
    
#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    
    // Player Variables
    // [SerializeField] private Rigidbody rb; //contains the rigidbody of the player
    public CharacterController charController;
    public GameObject mainCamera;
    private PlayerControlsInputs _input;
    
    // ==== Movement ====
    [Title("Movement", "Variables used for the Player's movement mechanic.")]
    [SerializeField] private float moveSpeed = 5.0f; //speed coefficient
    [SerializeField] private float sprintSpeed = 7.0f;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;
    private Vector3 inputDirection;
    private float speedOffset = 0.1f;
    private float playerYaw;
    
    // Private calculation variables
    // (not set in the Inspector)
    private float xMovement; //left to right movement data
    private float yMovement; //forward to back movement data
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float targetRotation = 0.0f;
    public float mouseSensitivity = 1f;
    private float _terminalVelocity = 53.0f;
    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private const float _threshold = 0.01f;
    [SerializeField] private float turnSpeed = 120f;

    // ========== Jumping ==========
    [Space]
    [Title("Jump", "Variables used for the Player's jumping mechanic.")]
    public float jumpPower = 4.0f; // How strong the jump force is
    public int maxJumps = 1;
    public int jumpsRemaining;
    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;
    [Space]
    [Title("Ground Check", "Variables used to perform ground checks for jumping.")]
    [SerializeField] private bool isGrounded;
    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;
    
    // Subscribe to events
    private void OnEnable()
    {
        PlayerGroundcast.groundCheck += GroundCheck;
        // PlayerGroundcast.groundCheck += JumpAndGravity;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerGroundcast.groundCheck -= GroundCheck;
        // PlayerGroundcast.groundCheck -= JumpAndGravity;
    }
    
    // =============================
    
    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        _input = GetComponent<PlayerControlsInputs>();

#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#endif
    }

    private void Start()
    {
        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        MoveCharacter();
        RotateCharacter();
        JumpAndGravity();
        charController.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    

    
    private void Move()
    {
        // Set the target speed depending on movement type (walking or sprinting)
        float targetSpeed = _input.sprint ? sprintSpeed : moveSpeed;
        
        // If there's no input, set the target speed to 0
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;
        
        // Get the current horizontal speed (X, Z)
        float currentHorizontalSpeed = new Vector3(charController.velocity.x, 0.0f, charController.velocity.z).magnitude;
        
        // create a float input magnitude
        // float inputMagnitude = Mathf.Clamp01(_input.move.magnitude);
        float inputMagnitude = _input.move.magnitude;
        
        // Accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        
        // normalise input direction
        inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero)
        {
            // move
            inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
        }
        else if (context.canceled)
        {
            jump = false;
            Debug.Log("PlayerMovement.cs >> Jump canceled.");
        }
        
        // Normally, we would run the following:
        // if (context.performed && isGrounded && jumpsRemaining > 0)
        // {
        //     rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
        // }
        // However, because I'm trying to use the character controller, I'm going to see
        // if that can be handled in the JumpAndGravity() method instead.
    }

    private void RotateCharacter()
    {

    }
    
    /// <summary>
    /// Checks if the Player is on the ground. Consistently works
    /// to update the isGrounded and jumpsRemaining variables
    /// Called at the end of every FixedUpdate().
    /// </summary>
    private void GroundCheck(bool grounded)
    {
        // If the Player is grounded
        if (grounded)
        {
            isGrounded = true; // Player is on the ground
            jumpsRemaining = maxJumps; // Reset the jumps
        }
        else
        {
            // Player is not on the ground
            isGrounded = false;
        }
    }
    
    
    // ========================
    // ========================
    // ========================
    

    private void JumpAndGravity()
    {
        if (isGrounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;
            
            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (_input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            // if we are not grounded, do not jump
            _input.jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
}
