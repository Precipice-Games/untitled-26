using System;
using Sirenix.OdinInspector;
using UnityEngine;

// This script is used for Skye's airship.

public class Airship : MonoBehaviour
{
    [Title("Airship Variables", "Variables related to Skye's Airship.")]
    public IslandManager islandManager;
    
    /// <summary>
    /// Tracks if player is standing on the Airship.
    /// </summary>
    private bool onAirship;
    private GameObject player;

    // An event that is invoked when the player stands on the rune circle
    public static event Action<bool> playerOnAirship;
    
    private bool islandCompleted;

    // Subscribe to events
    private void OnEnable()
    {
        PlayerInteraction.playerInteraction += Interaction;
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerInteraction.playerInteraction -= Interaction;
    }

    /// <summary>
    /// Called when the Player's raycast enters the body of the vessel.
    /// This sets onAirship to true and invokes the playerOnAirship event
    /// (true), which is picked up by InteractionPrompt.cs.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnAirship?.Invoke(true);
            onAirship = true;
            player = other.gameObject;
        }
    }

    /// <summary>
    /// Called when the Player's collider exits the rune circle collider.
    /// This sets inCircle to false and invokes the playerInCircle event
    /// (false), which is picked up by InteractionPrompt.cs.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnAirship?.Invoke(false);
            onAirship = false;
        }
    }

    /// <summary>
    /// Subscribed to the playerInteraction event in PlayerInteraction.cs. This is
    /// triggered when the Player presses 'E' while standing in the rune circle. It
    /// first checks if the puzzle has already been completed. If so, the Player is
    /// teleported to the next rune circle. Otherwise, puzzleTriggered is fired to
    /// set up the puzzle state.
    /// </summary>
    public void Interaction()
    {
        // If the player is not standing on the rune circle, break out.
        if (!onAirship) return;

        // If there's no island manager, break out.
        if (!IslandManagerFound()) return;
        
        // Check that the 
        if (!islandCompleted) return;
        
        Debug.Log("Airship.cs >> Player has interacted with the airship and the island is completed. Teleporting player to next location.");
    }

    /// <summary>
    /// Verifies that this rune circle has the puzzleInfo variable for its corresponding puzzle.
    /// </summary>
    /// <returns></returns>
    private bool IslandManagerFound()
    {
        if (islandManager != null)
        {
            return true;
        }

        Debug.LogError("Airship.cs >> No island manager is attached to this airship.");
        return false;
    }

    public void IslandCompleted()
    {
        islandCompleted = true;
    }
}
