using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// This script is used for Skye's airship. The SceneChanger.cs script, which
// defines the next island, is already attached to the airship prefab. Likewise,
// we simply need to connect its LoadScene() method to the travelToNextIsland
// event in the Inspector.

public class Airship : MonoBehaviour, IInteractable
{
    // Tracks if player is standing on the Airship
    private bool onAirship;

    // Static event that is invoked when the player boards the Airship
    public static event Action<bool> playerOnAirship;
    
    // Tracks if the island's objectives have been completed
    private bool islandCompleted;
    
    // Invokes the event to trigger the loading screen
    // and transition to the next island
    public UnityEvent travelToNextIsland;

    // Subscribe to events
    private void OnEnable()
    {
        PlayerGroundcast.airshipCheck += AirshipCheck;
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerGroundcast.airshipCheck -= AirshipCheck;
    }

    private void Awake()
    {
        islandCompleted = false;
    }

    /// <summary>
    /// Checks if the Player is on the ground. Consistently works
    /// to update the isGrounded and jumpsRemaining variables
    /// Called at the end of every FixedUpdate().
    /// </summary>
    private void AirshipCheck(bool isOnAirship)
    {
        // If the Player is on the airship
        if (isOnAirship)
        {
            onAirship = true; // Player is on the airship
            playerOnAirship?.Invoke(true);
        }
        else
        {
            // Player is not on the airship
            onAirship = false;
            playerOnAirship?.Invoke(false);
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
        Debug.Log($"Airship.cs >> onAirship = {onAirship} | islandCompleted = {islandCompleted}");
        
        // If the player is not standing on the airship, break out.
        if (!onAirship) return;
        
        // Check that the island has been completed
        if (!islandCompleted) return;
        
        Debug.Log("Airship.cs >> Player has interacted with the airship and the island is completed. Teleporting player to next location.");

        // Depart to the next destination
        Depart();
    }

    /// <summary>
    /// Departs the Player to the next destination via the Unity Inspector. Please ensure the
    /// island is set correctly on the SceneChanger.cs component of the airship prefab.
    /// </summary>
    private void Depart()
    {
        travelToNextIsland?.Invoke();
    }

    /// <summary>
    /// This method is subscribed to the islandCompleted event in IslandManager.cs
    /// and verifies that an island's objectives have been met.
    /// </summary>
    public void IslandCompleted()
    {
        islandCompleted = true;
        Debug.Log($"Airship.cs >> islandCompleted = {islandCompleted}. Airship is now ready to teleport player to next location.");
    }
}
