using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// This script is used to track data about a given island. It is
// attached to the IslandManager GameObject in each island's scene.
// Right now, it contains the IslandPuzzleManager, which tracks the
// data of the puzzles on the island. The reason it is a different
// script is so that it can be turned into a functional prefab, and
// also handle any other island-related data in the scene.

public class IslandManager : MonoBehaviour
{
    [Title("Island Puzzle Information")]
    [InfoBox("Attach the relevant data for this island's puzzles.")]
    public IslandPuzzleManager islandPuzzleManager;
    
    // // Subscribe to events
    // private void OnEnable()
    // {
    //     InteractablePillar.puzzleTriggered += PuzzleCompleted;
    // }
    //
    // // Unsubscribe from events
    // private void OnDisable()
    // {
    //     InteractablePillar.puzzleTriggered -= PuzzleCompleted;
    // }
    
    private void PuzzleCompleted()
    {
        // This method will be used to track the completion of puzzles on the island.
        // It will be subscribed to the InteractablePillar's puzzleTriggered event, which is fired when a puzzle is completed.
        // For now, it will just print a message to the console, but it can be expanded to include more functionality as needed.
        Debug.Log("Puzzle completed on island!");
    }
}