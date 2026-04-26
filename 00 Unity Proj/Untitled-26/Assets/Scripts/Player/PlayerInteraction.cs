using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action playerInteraction;

    //Call Interaction() on active rune circle that the player is standing on, if there is one. If not, invoke the playerInteraction event as a fallback for other interactions.
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if(RuneCircle.activeRuneCircle != null)
        {
            RuneCircle.activeRuneCircle.Interaction();
            return;
        }

        //Fallback
        playerInteraction?.Invoke();
    }
}
