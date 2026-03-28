using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// This script controls the interaction prompt that appears when the player
/// is in range of an interactable object. This functionality is designated
/// in its own script because it's possible that the Player paused while in
/// range of an interactable object, which does not make the prompt disappear.
/// </summary>
public class InteractionPrompt : MonoBehaviour
{
    // Subscribe to events
    private void OnEnable()
    {
        PlayerRaycastInteraction.raycastToggled += TogglePrompt;
        RuneCircle.playerInCircle += TogglePrompt;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerRaycastInteraction.raycastToggled -= TogglePrompt;
        RuneCircle.playerInCircle -= TogglePrompt;
    }

    
    /// <summary>
    /// This method properly toggles the interaction prompt on and
    /// off, depending on the state of the raycast.
    /// </summary>
    /// <param name="isEnabled"></param>
    private void TogglePrompt(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }
}