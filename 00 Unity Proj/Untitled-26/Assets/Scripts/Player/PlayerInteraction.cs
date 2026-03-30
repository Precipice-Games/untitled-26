using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action playerInteraction;
    public void Interact()
    {
        Debug.Log("PlayerInteraction.cs >> Interact() called");
        playerInteraction?.Invoke();
    }
}
