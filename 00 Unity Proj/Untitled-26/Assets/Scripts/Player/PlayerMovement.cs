#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // ==== Movement ====
    float moveSpeed = 5.0f; //speed coefficient
    float xMovement; //left to right movement data
    float yMovement; //forward to back movement data
    Rigidbody rb; //contains the rigidbody of the player
    
    // ========== Jumping ==========
    [Space]
    [Title("Jump", "Variables used for the Player's jumping mechanic.")]
    public float jumpPower = 4.0f; // How strong the jump force is
    public int maxJumps = 1;
    public int jumpsRemaining;

    [Space]
    [Title("Ground Check", "Variables used to perform ground checks for jumping.")]
    [SerializeField] private bool isGrounded;
    
    // Subscribe to events
    private void OnEnable()
    {
        BoxCastGrounded.groundCheck += GroundCheck;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        BoxCastGrounded.groundCheck -= GroundCheck;
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
