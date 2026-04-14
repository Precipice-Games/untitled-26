using Sirenix.OdinInspector;
using UnityEngine;
using Yarn.Unity;

// This script is used to track data about a given island. It is
// attached to the IslandManager GameObject in each island's scene.
// Right now, it contains the IslandPuzzleManager, which tracks the
// data of the puzzles on the island. The reason it is a different
// script is so that it can be turned into a functional prefab, and
// also handle any other island-related data in the scene.

public class IslandManager : MonoBehaviour
{
    public enum IslandName
    {
        MotherIsland,
        IceIsland,
        OasisIsland
    }

    [Title("Island Manager Data", "Data regarding the current island.")]
    [PropertyTooltip("Please assign the current island of this scene.")]
    public IslandName islandName = IslandName.IceIsland; // Default is Ice Island
    [PropertyTooltip("Attach the relevant data for this island's puzzles.")]
    public IslandPuzzleManager islandPuzzleManager;
    [PropertyTooltip("Attach the end crystal collectable for this island.")]
    public GameObject endCrystal;
    [PropertyTooltip("Attach the InMemoryVariableStorage component from the DialogueSystem object.")]
    public InMemoryVariableStorage variableStorage;
    
    /// <summary>
    /// Used to verify that all the current island's puzzles have been completed. Also
    /// updates the variable for the specified island in the YarnSpinner variable storage.
    /// </summary>
    public void IslandPuzzlesCompleted()
    {
        switch (islandName)
        {
            case IslandName.MotherIsland:
                Debug.Log("IslandManager.cs >> Mother Island puzzles completed!");
                // variableStorage.SetValue("motherFinished", true);
                break;
            case IslandName.IceIsland:
                Debug.Log("IslandManager.cs >> Ice Island puzzles completed!");
                variableStorage.SetValue("$iceFinished", true);
                break;
            case IslandName.OasisIsland:
                Debug.Log("IslandManager.cs >> Oasis Island puzzles completed!");
                // variableStorage.SetValue("$oasisFinished", true);
                break;
            
            // TODO: Add more cases for each island and check with Matthew about
            //       the specific variable names in the YarnSpinner variable storage.
        }
    }

    /// <summary>
    /// Used to verify that the island's end crystal has been collected. This is needed
    /// for the island itself to be completed in its entirety, and for the Player to be
    /// able to traverse to other islands using the Airship.
    /// </summary>
    public void CrystalCollected()
    {
        
    }
}