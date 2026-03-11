#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
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

    // The Player's X and Z coordinates on the grid.
    [SerializeField] private int playerGridX;
    [SerializeField] private int playerGridZ;
    
    // The starting and ending tile's coordinates
    private int startTileX;
    private int startTileZ;
    private int endTileX;
    private int endTileZ;

    //Keeps track of the potential tile for the player to move to
    private int desintationX;
    private int desintationZ;

    // The new coordinates as a Vector3
    Vector3 newCoords;
    Vector3 newPosition;

   // Player's Rigidbody
    Rigidbody rb;
    
    // Static event to notify subscribers of the Player's movement
    public static event Action<int, int> playerMoved;
    
    // Event fired when Player reaches the end tile of the puzzle
    public UnityEvent puzzleCompleted;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Subscribe to events
    private void OnEnable()
    {
        InteractablePillar.puzzleTriggered += UpdatePuzzleInformation;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        InteractablePillar.puzzleTriggered -= UpdatePuzzleInformation;
    }
    
    /// <summary>
    /// This method is called on the puzzleTriggered event. It receives data
    /// regarding the current puzzle, such as the starting and ending tiles.
    /// This data is packaged and sent from the InteractablePillar.cs script.
    /// </summary>
    /// <param name="puzzleInfo"></param>
    private void UpdatePuzzleInformation(PuzzleInformation puzzleInfo)
    {
        gridManager = puzzleInfo.gridManager.GetComponent<GridManager>();
        startTile = puzzleInfo.startTile;
        endTile = puzzleInfo.endTile;
        
        // Get the grid coordinates of the starting tile
        startTileX = startTile.GetComponent<SelectableTile>().gridX;
        startTileZ = startTile.GetComponent<SelectableTile>().gridZ;
        
        // Get the grid coordinates of the end tile
        endTileX = endTile.GetComponent<SelectableTile>().gridX;
        endTileZ = endTile.GetComponent<SelectableTile>().gridZ;
        
        // After gathering data, move Player to the startTile
        TryToMovePlayer(startTileX, startTileZ);
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

    /// <summary>
    /// Attempts to move the Player in the specified direction on the puzzle grid.
    /// </summary>
    /// <param name="xDir"></param>
    /// <param name="zDir"></param>
    // TODO: Should refactor this to handle different tile types and mechanics. Right now,
    //       it's only checking for normal moves, but we will need additional code to handle
    //       special tiles, like the ice mechanic.
    public void TryToMovePlayer(int xDir, int zDir)
    {
        // Calculate the new position on the grid
        int newX = playerGridX + xDir;
        int newZ = playerGridZ + zDir;
        
        Debug.Log("newX,newY" + newX + ", " + newZ);

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

        if (gridManager.IsIceTileType(newX, newZ))
        {
            Debug.Log("Is Ice");
            Debug.Log("New+Dir" + (newX + xDir) + ", " + (newZ + zDir));
            TryToMovePlayer(newX + xDir, newZ + xDir);

        }
        else
        {

            desintationX = newX;
            Debug.Log("Destination X:" + desintationX);
            desintationZ = newZ;
            Debug.Log("Destination Z:" + desintationZ);

        }

        int gridX = newX;
        int gridZ = newZ;

        // TODO: Add tile type checking here to handle special mechanics.
        //       The same should be done with a normal tile.
        // HandleTileType();

        // For right now, we will just snap the player to the new tile.
        SnapPlayerToTile(desintationX, desintationZ);
    }

    public void SnapPlayerToTile(int coordX, int cordZ)
    {
        // Grab the X and Z coordinates in Vector3 from the GridManager
        newCoords = gridManager.GridToWorld(coordX, cordZ);
        newPosition = new Vector3(newCoords.x, transform.position.y, newCoords.z);
        transform.position = newPosition;

        playerGridX = coordX;
        playerGridZ = cordZ;

        //Debug.Log($"Player moved to: {playerGridX},{playerGridZ}");
        
        Debug.Log("endTileX: " + endTileX);
        Debug.Log("playerGridX: " + playerGridX);
        Debug.Log("endTileZ: " + endTileZ);
        Debug.Log("playerGridZ: " + playerGridZ);

        IsPlayerOnEndTile();
    }
    
    /// <summary>
    /// Used to check if the Player has reached the end tile. If so, the puzzle
    /// has been completed and the appropriate events can be triggered.
    /// </summary>
    /// <returns></returns>
    private void IsPlayerOnEndTile()
    {
        // Check if the Player's coordinates match the end tile's coordinates
        if (endTileX == playerGridX && endTileZ == playerGridZ)
        {
            Debug.Log($"PlayerFixedMovement.cs >> Player has reached the end tile at [{endTileX}, {endTileZ}].");
            puzzleCompleted.Invoke();
        }
        
        playerMoved?.Invoke(playerGridX, playerGridZ);
    }
}