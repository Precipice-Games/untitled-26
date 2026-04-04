using Sirenix.OdinInspector;
using UnityEngine;

// This script is used to grab information about the current puzzle.
// This includes the starting tile, ending tile, etc. It is unique to
// each puzzle and will be used by PlayerFixedMovement.cs to handle
// movement across each puzzle.

public class PuzzleInformation : MonoBehaviour
{
    [Title("Puzzle Information", "Attach the relevant data of this puzzle.")]
    public Camera camera;
    public GameObject canvas;
    public GameObject startTile;
    public GameObject endTile;
    public GameObject gridManager;
    public bool puzzleSolved; // Has the puzzle been solved?

    // TODO: Perhaps put the Mana and movement information here as well?
    
    // NOTE: We cannot, with the current puzzle system, have two puzzles
    // active at once. I know Cass wrote about this a bit in Issue #249,
    // but as I'm working on Issue #254, I wanted to make a note that I
    // was having this issue here as well. -- Nikki
}