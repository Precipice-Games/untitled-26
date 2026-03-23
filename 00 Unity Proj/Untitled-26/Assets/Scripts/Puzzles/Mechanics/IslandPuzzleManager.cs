using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// This script is used to track information about each puzzle on
// a given island. It's attached to the IslandManager of that
// island's scene.

public class IslandPuzzleManager : MonoBehaviour
{
    [Title("Puzzle Information")]
    [InfoBox("Attach each of the Puzzle Prefabs for this island.")]
    public List<GameObject> puzzlePrefabs;
}