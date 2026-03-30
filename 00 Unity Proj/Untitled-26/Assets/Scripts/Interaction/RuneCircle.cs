using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class RuneCircle : MonoBehaviour, IInteractable
{
    [Title("Puzzle Information")]
    [InfoBox("Attach the data of the puzzle that this terminal corresponds to.")]
    public PuzzleInformation puzzleInfo;
    public ExitPuzzleButton exitPuzzleButton;
    /// <summary>
    /// The transform of the other rune circle that the player will be teleported to when they interact with this rune circle after completing the puzzle. 
    /// This is used in the TeleportPlayer() method.
    /// </summary>
    public GameObject otherRuneCircle;

    /// <summary>
    /// Tracks if player is standing on an active rune circle.
    /// </summary>
    private bool inCircle;
    private GameObject player;


    // An event that is invoked when the player stands on the rune circle
    public static event Action<bool> playerInCircle;

    // Static event to notify subscribers of game state changes
    public static event Action<PuzzleInformation> puzzleTriggered;

    private void OnEnable()
    {
        PlayerInteraction.playerInteraction += Interaction;
    }

    private void OnDisable()
    {
        PlayerInteraction.playerInteraction -= Interaction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCircle?.Invoke(true);
            inCircle = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCircle?.Invoke(false);
            inCircle = false;
        }
    }

    public void Interaction()
    {
        // If the player is not standing on the rune circle, break out.
        if (!inCircle) return;

        // If there's no puzzle info, break out.
        if (!PuzzleInfoFound()) return;

        // If the puzzle has already been completed, teleport to the other rune circle.
        if (puzzleInfo.puzzleSolved == true)
        {
            TeleportPlayer();
            return;
        }

        // Assign the transform position of the player's respawn location to be on the starting rune circle.
        exitPuzzleButton.currentRuneCircle = this;
        exitPuzzleButton.respawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // If the puzzle has not been completed, trigger the
        // event to notify subscribers.
        puzzleTriggered.Invoke(puzzleInfo);
    }

    private bool PuzzleInfoFound()
    {
        if (puzzleInfo != null)
        {
            return true;
        }

        Debug.LogError("No puzzle information attached to this pillar.");
        return false;
    }

    /// <summary>
    /// Teleport the player to the other rune circle.
    /// This is triggered when the player interacts with a rune circle of a completed puzzle.
    /// </summary>
    private void TeleportPlayer()
    {
        if (player)
        {
            player.transform.position = otherRuneCircle.transform.position;
        }
        else
        {
            Debug.Log("Could not find player");
        }
    }
}
