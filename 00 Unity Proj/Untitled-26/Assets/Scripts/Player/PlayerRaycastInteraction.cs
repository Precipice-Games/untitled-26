using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

// This script is responsible for handling the player's interaction-based
// raycast in the environment. There is another raycast created for the Player
// in BoxCastGrounded.cs, but that is for ground checks, NOT for interactions
// in the environment.

public class PlayerRaycastInteraction : MonoBehaviour
{
    //     ==== Player Raycast ====
    Ray interactionRay; //ray to cast
    RaycastHit raycastHit; //information about what is being hit
    bool isHitting;
    public float rayLength = 2.5f;
    public bool raycastEnabled = true;

    //    ==== Interactable Object ====
    public GameObject activeInteractable; //Stores overlapping interactable object
    public bool canInteract = true;

    //    === UI ===
    //public GameObject interactionUI;

    //    ==== Timer ====
    public float activeTimer = 5.0f;
    public float maxTime = 5.0f;
    
    [Space]
    [Title("Debugging Options", "Settings for quick debugging options.")]
    [PropertyTooltip("Print out what object the Player's raycast is hitting. False by default.")]
    public bool printRaycastHit = false;
    
    // Static event to notify subscribers of raycast toggling
    public static event Action<bool> raycastToggled;
    // Static event to notofy subscribers that the raycast is hitting an interactable object
    public static event Action<bool> raycastHitInteractable;

    /*
     * 
     * Emits interactionRay at the origin of the player rayLength
     * distance. Then it stores if the ray hits anything in isHitting
     * If it is then it draw the ray in green to the player and checks
     * if the overlapping object uses the IInteractable interface. If
     * so it assigns the object to activeInteractable. If the ray isn't
     * hitting anything it draws the ray as red and nulls the activeInteractable
     * 
     */

    void FixedUpdate()
    {

        if (!raycastEnabled)
        {
            activeInteractable = null;
            return; // exit early if raycast is disabled (used for dialogue and puzzle states)
        }

        if (activeTimer < maxTime)
        {
            activeTimer +=  Time.deltaTime;
        }
        else
        {
            canInteract = true;
            activeTimer = 0.0f;
        }
        
        interactionRay.origin = transform.position;
        interactionRay.direction = transform.forward;

        Vector3 origin = interactionRay.origin;
        Vector3 direction = interactionRay.direction;

        // Bool to store if the ray is hitting anything
        isHitting = Physics.Raycast(interactionRay, out raycastHit, rayLength);

        if (isHitting)
        {
            Debug.DrawRay(origin, direction * rayLength, Color.green);
            if (printRaycastHit) Debug.Log($"PlayerRaycastInteraction.cs >> Raycast Hit: {raycastHit.collider.gameObject.name}");

            if (raycastHit.collider.GetComponent<IInteractable>() != null)
            {
                activeInteractable = raycastHit.collider.gameObject;
                raycastHitInteractable?.Invoke(true);
            }
            else
            {
                activeInteractable = null;
                raycastHitInteractable?.Invoke(false);
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * rayLength, Color.red);
            activeInteractable = null;
            raycastHitInteractable?.Invoke(false);
        }

    }
    
    /// <summary>
    /// If the interact key is pressed the interact function triggers
    /// the Interaction() function of the interactable object
    /// </summary>
    public void Interact()
    {
        if (activeInteractable != null && canInteract)
        {
            activeInteractable.GetComponent<IInteractable>().Interaction();
            canInteract = false;
            activeInteractable = null;
            raycastHitInteractable?.Invoke(true);
        }
    }
    
    /// <summary>
    /// Toggles the raycast on and off. Used for dialogue and puzzle states
    /// to prevent the player from interacting with objects while in those states
    /// </summary>
    private void OnEnable()
    {
        GameStateManager.transitionedToNewState += ToggleRaycast;
        PlayerInteraction.playerInteraction += Interact;
    }

    private void OnDisable()
    {
        GameStateManager.transitionedToNewState -= ToggleRaycast;
        PlayerInteraction.playerInteraction -= Interact;
    }

    /// <summary>
    /// This method ensures that the raycast is toggled properly,
    /// depending on the current state of the game.
    /// </summary>
    /// <param name="state"></param>
    public void ToggleRaycast(GameStateManager.GameState state)
    {
        if (state == GameStateManager.GameState.Exploration)
        {
            raycastEnabled = true;
            raycastToggled?.Invoke(true);
        }
        else
        {
            raycastEnabled = false;
            raycastToggled?.Invoke(false);
        }
    }
}
