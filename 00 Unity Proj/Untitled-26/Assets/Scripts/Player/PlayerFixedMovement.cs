#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using UnityEngine;
using UnityEngine.InputSystem;

// This script is used to snap the Player in a fixed movement style during puzzle mode.
// It is similar to the PlayerMovement.cs script, but it is used to snap the player to
// a grid and only allow movement in four directions (up, down, left, right) instead of
// allowing for diagonal movement.

public class PlayerFixedMovement : MonoBehaviour
{

    // ==== Variables =====
    private Vector3 snapPosition; //the position the player will snap to
    private Vector3 playerCurrentPosition; //the current position of the player

   
    Rigidbody rb; //contains the rigidbody of the player

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*
     * 
     * Takes the movement variables and adjusts the corresponding axes
     * in a Vector3 variable, then adjusts the player's position based
     * on moveSpeed, the localMoveDirection variable, and deltaTime
     * (deltaTiem to normalize the movement)
     * 
     */

    private void FixedUpdate()
    {

    }

    /*
     * 
     * Takes the player's keyboard input in context as a Vector2
     * the x value of the Vector2 (left and right movement) gets 
     * assigned to xMovement, and the y value of the Vector2 
     * (forward and back movement) gets assigned to yMovement
     * 
     */

    public void PlayerMove(InputAction.CallbackContext context)
    {

        // xMovement = context.ReadValue<Vector2>().x;
        // yMovement = context.ReadValue<Vector2>().y;        

    }
}
