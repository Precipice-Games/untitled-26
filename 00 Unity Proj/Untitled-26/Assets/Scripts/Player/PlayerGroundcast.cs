#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using System;
using Sirenix.OdinInspector;
using UnityEngine;

// This script creates a raycast that shoots downwards from the Player.
// It is used to check if the Player is currently on the ground.

public class PlayerGroundcast : MonoBehaviour
{
    [Title("Grounded Checker", "General variables used to handle ground checking.")]
    public bool groundChecking = true;
    private Ray groundInteractionRay;
    private RaycastHit groundRaycastHit;
    bool hittingGround;
    bool hittingAirship;
    private LayerMask groundLayerMask;
    private LayerMask airshipLayerMask;
    public GameObject currentPlatform;
    public float activeTimer = 5.0f;
    public float maxTime = 5.0f;
    
    [Space]
    [Title("Raycast Settings", "Settings for the ray. Hover over variables for more information.")]
    [PropertyTooltip("Baseline desired length of a ray.")]
    public float rawRayLength;
    [PropertyTooltip("Be sure to add a buffer for the ray length to ensure consistency in ground detection.")]
    public float rayLengthBuffer;
    private float groundRayLength; // Actual ray length
    
    [Space]
    [Title("Debugging Options", "Settings for quick debugging options.")]
    [PropertyTooltip("Print out what ground the Player is standing on. False by default.")]
    public bool printGroundedStatus = false;
    
    // Static events to notify subscribers of grounded raycast hits
    public static event Action<bool> groundCheck;
    public static event Action<bool> airshipCheck;

    private void Awake()
    {
        // Finalize the ray length by adding the raw length and the buffer together.
        groundRayLength = rawRayLength + rayLengthBuffer;
        
        // Even though the raw value of a ray is often half the size
        // of the object's height, sometimes there are issues detecting
        // the ground. Hence, why adding a tweakable buffer is necessary.
          
        groundLayerMask = LayerMask.GetMask("Ground");
        airshipLayerMask = LayerMask.GetMask("Airship");
    }
    
    /// <summary>
    /// In FixedUpdate(), we emit the ray from the origin of the Player and
    /// point it downwards. Collisions are handled within "if (hittingGround)"
    /// to see if the Player is standing on an object tagged with "Ground".
    /// </summary>

    void FixedUpdate()
    {
        // Set the origin and direction of the ray
        SetOriginAndDirection();
        Vector3 origin = groundInteractionRay.origin;
        Vector3 direction = groundInteractionRay.direction;

        // Perform the raycast using the ray's origin and downward direction
        hittingGround = Physics.Raycast(groundInteractionRay, out groundRaycastHit, groundRayLength, groundLayerMask);
        
        // Another raycast but for the airship
        // hittingAirship = Physics.Raycast(groundInteractionRay, out groundRaycastHit, groundRayLength, airshipLayerMask);

        // If the ray is hitting something
        if (hittingGround)
        {
            // Turn the ray green
            Debug.DrawRay(origin, direction * groundRayLength, Color.green);
            currentPlatform = groundRaycastHit.collider.gameObject;
            if (printGroundedStatus) Debug.Log("PlayerGroundcast.cs >> Grounded on: " + currentPlatform.name);
        }
        else
        {
            Debug.DrawRay(origin, direction * groundRayLength, Color.red);
            currentPlatform = null;
        }

        groundCheck?.Invoke(hittingGround);
        airshipCheck?.Invoke(hittingAirship);
    }

    /// <summary>
    /// Sets the origin and direction of the raycast.
    /// </summary>
    private void SetOriginAndDirection()
    {
        // Start the ray slightly above the player's position to avoid starting inside a collider
        groundInteractionRay.origin = transform.position + Vector3.up * 0.1f;
        // Cast downward so we hit the ground under the player (use world down for stability)
        groundInteractionRay.direction = Vector3.down;
    }
}
