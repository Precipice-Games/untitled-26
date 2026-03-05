#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

// This script is used to snap the Player in a fixed movement style during puzzle mode.
// It is similar to the PlayerMovement.cs script, but it is used to snap the player to
// a grid and only allow movement in four directions (up, down, left, right) instead of
// allowing for diagonal movement.

public class PlayerFixedMovement : MonoBehaviour
{

    // ==== Variables =====
    private Vector3 playerCurrentPosition; // Current Vector3 position
    private Vector3 startPosition;
    private GridManager gridManager;
    private GameObject puzzleCam; // The camera object holds relevant scripts
    
    // Tile that the Player will start at
    // (Not necessarily within the grid)
    private GameObject startTile;
    private GameObject endTile;
    
    public int gridX;
    public int gridZ;

    // The Player's X and Z coordinates on the grid.
    [SerializeField] private int playerGridX;
    [SerializeField] private int playerGridZ;
    
    // The new coordinates as a Vector3
    Vector3 newCoords;
    Vector3 newPosition;

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
    
    private void OnEnable()
    {
        InteractablePillar.puzzleTriggered += UpdatePuzzleInformation;
    }
    
    private void OnDisable()
    {
        InteractablePillar.puzzleTriggered -= UpdatePuzzleInformation;
    }
    
    private void UpdatePuzzleInformation(PuzzleInformation puzzleInfo)
    {
        gridManager = puzzleInfo.gridManager.GetComponent<GridManager>();
        startTile = puzzleInfo.startTile;
        endTile = puzzleInfo.endTile;
        MoveToStartTile();
    }
    
    // The following methods listen to callback events from the Puzzle map from the
    // PlayerControls.inputactions asset. They are subscribed to these events via
    // the PlayerInput component in the Inspector menu of the Unity Editor. This is
    // the same methodology used for the freeform movement in PlayerMovement.cs.

    public void MoveUp(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveUp performed.");
        TryToMovePlayer(0, 1);
    }
    
    public void MoveDown(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveDown called.");
        TryToMovePlayer(0, -1);
    }
    
    public void MoveLeft(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveLeft called.");
        TryToMovePlayer(-1, 0);
    }
    
    public void MoveRight(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveRight called.");
        TryToMovePlayer(1, 0);
    }
    
    // TODO: Clean this method up and make it more efficient. Would like to
    //       set the Player's position using grid coordinates if possible.
    
    public void MoveToStartTile()
    {
        if (startTile != null)
        {
            Vector3 startCoords = startTile.transform.position;
            Vector3 newPosition = new Vector3(startCoords.x, transform.position.y, startCoords.z);
            transform.position = newPosition;
            Debug.Log("PlayerFixedMovement.cs >> Moved player to starting tile.");
        }
        else
        {
            Debug.LogError("PlayerFixedMovement.cs >> Starting tile is not assigned.");
        }
    }

    public void TryToMovePlayer(int xDir, int zDir)
    {
        // Calculate the new position on the grid
        int newX = playerGridX + xDir;
        int newZ = playerGridZ + zDir;
        
        Debug.Log($"PlayerFixedMovement.cs >> Attempting to move the Player to: {xDir},{zDir}");

        // Check if there's a tile to move to
        if (gridManager.IsCellEmpty(newX, newZ))
        {
            Debug.Log($"PlayerFixedMovement.cs >> There is no tile to jump to at: {newX},{newZ}");
            return;
        }
        
        if (!gridManager.IsInsideGrid(newX, newZ))
        {
            Debug.Log("PlayerFixedMovement.cs >> Move blocked: Outside grid");
            return;
        }
        
        gridX = newX;
        gridZ = newZ;

        // TODO: Add tile type checking here to handle special mechanics.
        //       The same should be done with a normal tile.
        // HandleTileType();

        // For right now, we will just snap the player to the new tile.
        SnapPlayerToTile(gridX, gridZ);
    }

    public void SnapPlayerToTile(int coordX, int cordZ)
    {
        // Grab the X and Z coordinates in Vector3 from the GridManager
        newCoords = gridManager.GridToWorld(gridX, gridZ);
        newPosition = new Vector3(newCoords.x, transform.position.y, newCoords.z);
        transform.position = newPosition;

        playerGridX = gridX;
        playerGridZ = gridZ;

        Debug.Log($"Player moved to: {gridX},{gridZ}");
    }
}