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
        IceIsland,
        OasisIsland
    }

    [Title("Island Manager Data", "Data regarding the current island.")]
    [PropertyTooltip("Please assign the current island of this scene.")]
    public IslandName islandName = IslandName.IceIsland;
    [PropertyTooltip("Attach the relevant data for this island's puzzles.")]
    public IslandPuzzleManager islandPuzzleManager;
    [PropertyTooltip("Attach the InMemoryVariableStorage component from the DialogueSystem object.")]
    public InMemoryVariableStorage variableStorage;
    
    /// <summary>
    /// Updates the variable for the current island in the
    /// Yarn Spinner variable storage.
    /// </summary>
    /// <param name="variableName"></param>
    /// <param name="value"></param>
    public void IslandCompleted()
    {
        switch (islandName)
        {
            case IslandName.IceIsland:
                Debug.Log("Ice Island completed!");
                variableStorage.SetValue("$iceFinished", true);
                break;
            case IslandName.OasisIsland:
                Debug.Log("Oasis Island completed!");
                break;
            
            // TODO: Add more cases for each island
        }
    }
}