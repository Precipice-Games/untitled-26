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

    // private void Awake()
    // {
    //     if (puzzleInfo != null)
    //     {
    //         puzzleInfo = GetComponent<PuzzleInformation>();
    //     }
    // }

    public void Interaction()
    {
        Debug.Log("Interacting");

        if (puzzleTriggered != null)
        {
            // Invoke this event. The onPuzzleTrigger() event from
            // GameStateManager.cs has been subscribed to this event
            // in the Unity Editor.
            puzzleTriggered.Invoke(puzzleInfo);
        }
    }
}