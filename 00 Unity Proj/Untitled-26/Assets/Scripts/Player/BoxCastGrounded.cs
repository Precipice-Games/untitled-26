using System;
using Sirenix.OdinInspector;
using UnityEngine;

// This script creates a raycast that shoots downwards from the Player.
// It is used to check if the Player is currently on the ground.

public class BoxCastGrounded : MonoBehaviour
{
    [Space]
    [Title("Grounded Raycasting", "Variables used to handle ground checking.")]
    private Ray groundInteractionRay;
    private RaycastHit groundRaycastHit;
    bool hittingGround;
    public float rawRayLength; // Set a default ray length for ground detection
    public float rayLengthBuffer; // Be sure to add a buffer for the ray length to ensure consistency in ground detection.
    private float groundRayLength; // Actual ray length
    public bool groundRaycastEnabled = true;

    // ===== Variables =====
    public GameObject currentPlatform;
    public bool canInteract = true;
    public float activeTimer = 5.0f;
    public float maxTime = 5.0f;

    private void Awake()
    {
        // Finalize the ray length by adding the raw length and the buffer together.
        groundRayLength = rawRayLength + rayLengthBuffer;
        
        // Even though the raw value of a ray is often half the size
        // of the object's height, sometimes there are issues detecting
        // the ground. Hence, why adding a tweakable buffer is necessary.
    }
    
    /// <summary>
    /// In FixedUpdate(), we emit the ray from the origin of the Player and
    /// point it downwards. Collisions are handled within "if (hittingGround)"
    /// to see if the Player is standing on an object tagged with "Ground".
    /// </summary>

    void FixedUpdate()
    {

        if (!groundRaycastEnabled)
        {
            currentPlatform = null;
            return; // exit early if raycast is disabled (used for dialogue and puzzle states)
        }

        if (activeTimer < maxTime)
        {
            activeTimer += Time.deltaTime;
        }
        else
        {
            canInteract = true;
            activeTimer = 0.0f;
        }

        // Set the origin and direction of the ray
        // Start the ray slightly above the player's position to avoid starting inside a collider
        groundInteractionRay.origin = transform.position + Vector3.up * 0.1f;
        // Cast downward so we hit the ground under the player (use world down for stability)
        groundInteractionRay.direction = Vector3.down;

        Vector3 origin = groundInteractionRay.origin;
        Vector3 direction = groundInteractionRay.direction;

        // Perform the raycast using the ray's origin and downward direction
        hittingGround = Physics.Raycast(groundInteractionRay, out groundRaycastHit, groundRayLength);

        // If the ray is hitting something
        if (hittingGround)
        {

            Debug.DrawRay(origin, direction * groundRayLength, Color.green);
            Debug.Log(groundRaycastHit.collider.gameObject.name);

            // if (raycastHit.collider.GetComponent<IInteractable>() != null)
            // {
            //     currentPlatform = raycastHit.collider.gameObject;
            // }

            if (groundRaycastHit.collider.CompareTag("Ground"))
            {
                currentPlatform = groundRaycastHit.collider.gameObject;
            }

        }
        else
        {
            Debug.DrawRay(origin, direction * groundRayLength, Color.red);
            currentPlatform = null;
        }
    }
}
