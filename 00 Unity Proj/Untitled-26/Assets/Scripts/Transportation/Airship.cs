using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is used for Skye's airship.

public class Airship : MonoBehaviour, IInteractable
{
    public enum Destination
    {
        MotherIsland,
        IceIsland,
        OasisIsland
    }
    
    [Title("Airship Variables", "Variables related to Skye's Airship.")]
    [PropertyTooltip("The next island the Player should travel to after completing required tasks.")]
    public Destination nextDestination = Destination.IceIsland; // Default is Ice Island
    
    // Tracks if player is standing on the Airship
    private bool onAirship;

    // Static event that is invoked when the player boards the Airship
    public static event Action<bool> playerOnAirship;
    
    private bool islandCompleted;

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
        Depart(nextDestination);
    }

    /// <summary>
    /// Departs the Player to the next destination based on the value of nextDestination.
    /// The value of nextDestination is set in the Inspector of the Airship.
    /// </summary>
    private void Depart(Destination destination)
    {
        switch (destination)
        {
            case Destination.MotherIsland:
                Debug.Log("Airship.cs >> Now departing to Mother Island...");
                SceneManager.LoadScene("Mother_Island");
                break;
            case Destination.IceIsland:
                Debug.Log("Airship.cs >> Now departing to Ice Island...");
                SceneManager.LoadScene("Ice_Island");
                break;
            case Destination.OasisIsland:
                Debug.Log("Airship.cs >> Now departing to Oasis Island...");
                SceneManager.LoadScene("Oasis_Island");
                break;
        }
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
