using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action playerInteraction;
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        //Debug.Log("PlayerInteraction.cs >> Interact() called");
        playerInteraction?.Invoke();
    }
}
