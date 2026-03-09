using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// This script is used to track data about a given island. It is
// attached to the IslandManager GameObject in each island's scene.
// Right now, it contains the IslandPuzzleManager, which tracks the
// data of the puzzles in the island. The reason it is a different
// script is so that it can be turned into a functional prefab, and
// also handle any other island-related data in the scene.

public class IslandManager : MonoBehaviour
{
    [Title("Island Puzzle Information")]
    [InfoBox("Attach the relevant data for this island's puzzles.")]
    public IslandPuzzleManager islandPuzzleManager;
}