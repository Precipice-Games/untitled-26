using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

// This script is attached to a puzzle pillar. It fires the event
// that triggers the puzzle when the player interacts with it. With
// it, the PuzzleInformation data is sent to subscribers that need
// to use the data to set up the puzzle state.

public class InteractablePillar : MonoBehaviour, IInteractable
{
    // Static event to notify subscribers of game state changes
    public static event Action<PuzzleInformation> puzzleTriggered;
    
    [Title("Puzzle Information")]
    [InfoBox("Attach the data of the puzzle that this terminal corresponds to.")]
    public PuzzleInformation puzzleInfo;

    public ExitPuzzleButton exitPuzzleButton;

    public void Interaction()
    {
        // If there's no puzzle info, break out
        if (!PuzzleInfoFound()) return;

        // Ensure the puzzle has not already been completed.
        if (puzzleInfo.puzzleSolved == true) return;

        //
        exitPuzzleButton.currentPillar = this;
        exitPuzzleButton.respawnLocation = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 0.25f);

        // If the puzzle has not been completed, trigger the
        // event to notify subscribers.
        puzzleTriggered.Invoke(puzzleInfo);
    }

    /// <summary>
    /// Used to check if the puzzleInfo variable has been assigned in the
    /// Inspector in the Unity Editor.
    /// </summary>
    /// <returns></returns>
    private bool PuzzleInfoFound()
    {
        if (puzzleInfo != null)
        {
            return true;
        }
        
        Debug.LogError("No puzzle information attached to this pillar.");
        return false;
    }
}