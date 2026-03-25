    using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

// This script is used to track information about each puzzle on
// a given island. It's attached to the IslandManager of that
// island's scene.

public class IslandPuzzleManager : MonoBehaviour
{
    [Title("Puzzle Prefabs", "Attach each of the Puzzle Prefabs for this island.")]
    public List<GameObject> puzzlePrefabs;
    
    [Space]
    [Title("IslandCompleted", "This event is fired when an island is complete.")]
    public UnityEvent islandCompleted;

    // Subscribe to events
    private void OnEnable()
    {
        PlayerFixedMovement.updatePuzzleStatus += UpdateCompletionStatus;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerFixedMovement.updatePuzzleStatus -= UpdateCompletionStatus;
    }
    
    public void UpdateCompletionStatus(PuzzleInformation completedPuzzle)
    {
        // Update the puzzle's completion status
        completedPuzzle.puzzleSolved = true;
        CheckAllPuzzlesCompleted();
    }

    /// <summary>
    /// Used to check if all puzzles on the island have been completed.
    /// </summary>
    private void CheckAllPuzzlesCompleted()
    {
        foreach (GameObject puzzlePrefab in puzzlePrefabs)
        {
            PuzzleInformation puzzleInfo = puzzlePrefab.GetComponent<PuzzleInformation>();

            // Exit if there's any puzzles unsolved
            if (puzzleInfo.puzzleSolved == false)
            {
                return;
            }
            
            Debug.Log("IslandPuzzleManager.cs >> All puzzles completed!");
            
            // Notify the IslandManager that all puzzles have been completed.
            // This event is assigned in the Unity Editor.
            islandCompleted.Invoke();
        }
    }
}