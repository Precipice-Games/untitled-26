#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using UnityEngine;
using UnityEngine.InputSystem;

// This script is used to snap the Player in a fixed movement style during puzzle mode.
// It is similar to the PlayerMovement.cs script, but it is used to snap the player to
// a grid and only allow movement in four directions (up, down, left, right) instead of
// allowing for diagonal movement.

public class PlayerFixedMovement : MonoBehaviour
{

    // ==== Variables =====
    private Vector3 playerCurrentPosition; // Current Vector3 position
    private Vector3 startPosition;
    private PuzzleInformation puzzleInformation;
    
    // Tile that the Player will start at
    // (Not necessarily within the grid)
    private GameObject startTile;
    private GameObject endTile;
    
    public int gridX;
    public int gridZ;

    // The Player's X and Z coordinates on the grid.
    private int playerGridX;
    private int playerGridZ;

   // Player's Rigidbody
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Set the player's starting position to the position of the starting tile.
        if (startTile != null)
        {
            startPosition = startTile.transform.position;
            playerCurrentPosition = startPosition;
            transform.position = playerCurrentPosition;
        }
        else
        {
            Debug.LogError("PlayerFixedMovement.cs >> Starting tile is not assigned.");
        }
    }
    
    // The following methods listen to callback events from the Puzzle map from the
    // PlayerControls.inputactions asset. They are subscribed to these events via
    // the PlayerInput component in the Inspector menu of the Unity Editor. This is
    // the same methodology used for the freeform movement in PlayerMovement.cs.

    public void MoveUp(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (context.performed)
        {
            Debug.Log("PlayerFixedMovement.cs >> MoveUp performed.");
            TryToMovePlayer(0, 1);
        }
    }
    
    public void MoveDown(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (context.performed)
        {
            Debug.Log("PlayerFixedMovement.cs >> MoveDown called.");
            TryToMovePlayer(0, -1);
        }
    }
    
    public void MoveLeft(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (context.performed)
        {
            Debug.Log("PlayerFixedMovement.cs >> MoveLeft called.");
            TryToMovePlayer(-1, 0);
        }
    }
    
    public void MoveRight(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (context.performed)
        {
            Debug.Log("PlayerFixedMovement.cs >> MoveRight called.");
            TryToMovePlayer(1, 0);
        }
    }

    public void TryToMovePlayer(int xDir, int zDir)
    {
        // Calculate the new position on the grid
        int newX = playerGridX + xDir;
        int newZ = playerGridX + zDir;
        
        Debug.Log("PlayerFixedMovement.cs >> Attempting to move the Player to: " + xDir + "," + zDir);

        // Check if there's a tile to move to
        if (!GridManager.Instance.IsCellEmpty(newX, newZ))
        {
            Debug.Log($"PlayerFixedMovement.cs >> There is a tile at ({newX},{newZ})");
            return;
        }
    }
}