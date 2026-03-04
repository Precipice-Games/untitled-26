using UnityEngine;

// This script is used to grab information about the current puzzle.
// This includes the starting tile, ending tile, etc. It is unique to
// each puzzle and will be used by PlayerFixedMovement.cs to handle
// movement across each puzzle.

public class PuzzleInformation : MonoBehaviour
{
    public GameObject startTile;
    public GameObject endTile;
    
    // TODO: Perhaps put the Mana and movement information here as well?
    
    // Method just used to set the scene defaults.
    // All other state changes occur in TransitionToState().
    private void SetPuzzleDefaults(GameStateManager.GameState defaultState)
    {
        
    }
}