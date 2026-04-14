#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // ==== Movement ====
    [Title("Movement", "Variables used for the Player's movement mechanic.")]
    [SerializeField] private float moveSpeed = 5.0f; //speed coefficient
    [SerializeField] private float xMovement; //left to right movement data
    [SerializeField] private float yMovement; //forward to back movement data
    [SerializeField] private Rigidbody rb; //contains the rigidbody of the player
    public float mouseSensitivity = 1f;
    private float targetRotation = 0.0f;
    public GameObject mainCamera;
    private float _rotationVelocity;
    
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    
    // [PropertyTooltip("Please attach the Player's camera. This is necessary for making it revolve around the Player as they turn.")]
    // public GameObject playerCamera;
    // private PlayerCameraRotation cameraRotation;
    // [PropertyTooltip("Mouse sensitivity. Default is 0.5f.")]
    // public float mouseSensitivity = 1f;
    
    // ========== Jumping ==========
    [Space]
    [Title("Jump", "Variables used for the Player's jumping mechanic.")]
    public float jumpPower = 4.0f; // How strong the jump force is
    public int maxJumps = 1;
    public int jumpsRemaining;

    [Space]
    [Title("Ground Check", "Variables used to perform ground checks for jumping.")]
    [SerializeField] private bool isGrounded;

    
    private float turnInput;
    private Vector2 look;
    
    // Subscribe to events
    private void OnEnable()
    {
        PlayerGroundcast.groundCheck += GroundCheck;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerGroundcast.groundCheck -= GroundCheck;
    }
    
    // =============================
    
    private void Start()
    {
        // Assign the rigidbody component to rb
        rb = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Takes the movement variables and adjusts the corresponding axes
    /// in a Vector3 variable, then adjusts the player's position based
    /// on moveSpeed, the localMoveDirection variable, and deltaTime.
    /// The deltaTime is used to normalize the movement.
    /// </summary>

    private void FixedUpdate()
    {
        Vector3 localMoveDirection = transform.right * xMovement + transform.forward * yMovement;
        transform.position += localMoveDirection * moveSpeed * Time.deltaTime;
    }
    
    /// <summary>
    /// Takes the player's keyboard input in context as a Vector2
    /// the x value of the Vector2 (left and right movement) gets
    /// assigned to xMovement, and the y value of the Vector2
    /// (forward and back movement) gets assigned to yMovement.
    /// </summary>
    /// <param name="context"></param>

    public void PlayerMove(InputAction.CallbackContext context)
    {
        xMovement = context.ReadValue<Vector2>().x;
        yMovement = context.ReadValue<Vector2>().y;
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
        
        // Normalize look input direction
        Vector3 inputDirection = new Vector3(look.x, 0.0f, look.y).normalized;
        
        targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSmoothTime);

        // rotate to face input direction relative to camera position
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        
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
        if (context.performed && isGrounded && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
        }
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
}
