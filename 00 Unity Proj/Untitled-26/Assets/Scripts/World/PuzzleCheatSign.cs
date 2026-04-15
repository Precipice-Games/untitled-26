using System;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to a cheat sign (optional) on an island and used to mark all
// puzzles as complete. It is helpful for both playtesting and if there are any puzzles
// that are broken and the Player cannot traverse to the next islands. It should be
// removed in the final build.

public class PuzzleCheatSign : MonoBehaviour, IInteractable
{
    private List<GameObject> puzzles;
    public IslandPuzzleManager islandPuzzleManager;
    
    public static event Action allPuzzlesComplete;

    private void Awake()
    {
        // Get the puzzle prefabs
        puzzles = islandPuzzleManager.puzzlePrefabs;
    }
    
    public void Interaction()
    {
        MarkAllPuzzlesComplete();
    }
    
    /// <summary>
    /// Used to check if all puzzles on the island have been completed.
    /// </summary>
    private void MarkAllPuzzlesComplete()
    {
        Debug.Log($"PuzzleCheatSign.cs >> Message received. Marking {puzzles.Count} puzzles as complete.");
        
        foreach (GameObject puzzle in puzzles)
        {
            PuzzleInformation puzzleInfo = puzzle.GetComponent<PuzzleInformation>();
            puzzleInfo.puzzleSolved = true;
        }
        
        allPuzzlesComplete?.Invoke();
    }
}
