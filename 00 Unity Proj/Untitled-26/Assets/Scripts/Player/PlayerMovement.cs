#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using System;
using Cdm.Figma;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerMovement : MonoBehaviour
{
    // Player Variables
    // [SerializeField] private Rigidbody rb; //contains the rigidbody of the player
    public CharacterController charController;
    public GameObject mainCamera;
    
    // ==== Movement ====
    [Title("Movement", "Variables used for the Player's movement mechanic.")]
    [SerializeField] private float moveSpeed = 5.0f; //speed coefficient
    [SerializeField] private float xMovement; //left to right movement data
    [SerializeField] private float yMovement; //forward to back movement data
    // [SerializeField] private Rigidbody rb; //contains the rigidbody of the player
    public float mouseSensitivity = 1f;
    private float targetRotation = 0.0f;
    public GameObject mainCamera;
    private float _rotationVelocity;
    
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;
    
    // Private calculation variables
    // (not set in the Inspector)
    private float _speed;
    private Vector2 move;
    private bool jump;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float targetRotation = 0.0f;
    public float mouseSensitivity = 1f;
    private float _terminalVelocity = 53.0f;
    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private float turnInput;
    private Vector2 look;
    private const float _threshold = 0.01f;

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

    private void Start()
    {
        // Assign the rigidbody component to rb
        // rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveCharacter();
        RotateCharacter();
        JumpAndGravity();
    }
    
    /// <summary>
    /// Takes the player's keyboard input in context as a Vector2 and
    /// assigns it to the move variable, which is used in the Move() method.
    /// </summary>
    /// <param name="context"></param>
    public void PlayerMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    
    private void MoveCharacter()
    {
        // Set the target speed (for us it's just moveSpeed,
        // but this could change if we decide to add sprinting)
        float targetSpeed = move == Vector2.zero ? 0.0f : moveSpeed;
        
        // Get the current horizontal speed (X, Z)
        float currentHorizontalSpeed = new Vector3(charController.velocity.x, 0.0f, charController.velocity.z).magnitude;
        
        // Create a float named speed offset
        float speedOffset = 0.1f;
        
        // create a float input magnitude
        float inputMagnitude = Mathf.Clamp01(move.magnitude);
        
        // Accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // Use Lerp() to smoothly interpolate between the current speed and the
            // target speed, based on the input magnitude and the speed change rate.
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            
            // Round speed to reduce jitter
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        
        // Normalize input direction
        // Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;
        
        // // If move input detected, rotate the Player
        // if (look != Vector2.zero)
        // {
        //     targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        //     float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSmoothTime);
        //     
        //     // rotate to face input direction relative to camera position
        //     transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        // }
        
        // Rotate only when there is movement input
        if (look.sqrMagnitude >= _threshold)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
        // Update the Vector3 target direction
        // Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        Vector3 targetDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        
        // Finally, move the player with CharacterController.Move()
        charController.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        // Horizontal + vertical motion
        // charController.Move(targetDirection.normalized * (_speed * Time.deltaTime) + Vector3.up * (_verticalVelocity * Time.deltaTime));
    }

    private void RotateCharacter()
    {
        
    }
    
    /// <summary>
    /// Takes in the Player's mouse pointer (delta) input in as a
    /// Vector2 and assigns it to lookX, which defines the turning
    /// direction. Then we multiply that value by the mouseSensitivity
    /// and apply it to turnInput. This comes to fruition in the
    /// FixedUpdate() method to physically rotate the Player.
    /// </summary>
    /// <param name="context"></param>
    public void PlayerLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Takes the player's jump input in the context parameter
    /// then checks if context was just performed and that the
    /// player has a rigidbody variable and if both are true
    /// the players vertical velocity gets boosted by jumpPower
    /// </summary>
    /// <param name="context"></param>
    public void PlayerJump(InputAction.CallbackContext context)
    {
        // if (context.performed && isGrounded)
        // {
        //     Debug.Log("PlayerMovement.cs >> Jump performed.");
        // }

        if (context.performed)
        {
            // rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
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
            if (jump && _jumpTimeoutDelta <= 0.0f)
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
            jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
}
