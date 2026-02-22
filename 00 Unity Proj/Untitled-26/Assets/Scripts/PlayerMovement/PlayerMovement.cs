#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // ==== Variables =====


    // ==== Movement ====
    float moveSpeed = 5.0f; //speed coefficient
    float xMovement; //left to right movement data
    float yMovement; //forward to back movement data
    public float jumpPower = 4.0f; //how strong the jump force is

   
    Rigidbody rb; //contains the rigidbody of the player

    /*
     * 
     * Assign the rigidbody component to rb
     * 
     */
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
        
        Vector3 localMoveDirection = transform.right * xMovement + transform.forward * yMovement;

        transform.position += localMoveDirection * moveSpeed * Time.deltaTime;

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

        xMovement = context.ReadValue<Vector2>().x;
        yMovement = context.ReadValue<Vector2>().y;        

    }

    /*
     * 
     * Takes the player's jump input in the context parameter
     * then checks if context wwas just performed and that the
     * player has a rigidbody variable and if both are true
     * the players vertical velocity gets boosted by jumpPower
     * 
     */

    public void PlayerJump(InputAction.CallbackContext context)
    {

        
        if (context.performed && rb != null)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
        }

    }
}
