#if UNITY_EDITOR
using UnityEditor.Build;
#endif

using System;
using Sirenix.OdinInspector;
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
    private PuzzleInformation puzzleInfo;
    private Vector3 playerCurrentPosition; // Current Vector3 position
    private Vector3 startPosition;
    private GridManager gridManager;
    private GameObject puzzleCam; // The camera object holds relevant scripts
    
    // Tile that the Player will start at
    // (Not necessarily within the grid)
    private GameObject startTile;
    private GameObject endTile;

    [Title("Player's Grid Coordinates")]
    [SerializeField] private int playerGridX;
    [SerializeField] private int playerGridZ;
    
    // The starting and ending tile's coordinates
    private int startTileX;
    private int startTileZ;
    private int endTileX;
    private int endTileZ;

    //Keeps track of the potential tile for the player to move to
    private int destinationX = 0;
    private int destinationZ = 0;

    private int deltaX;
    private int deltaZ;

    private int landingX = 0;
    private int landingZ = 0;

    // The new coordinates as a Vector3
    Vector3 newCoords;
    Vector3 newPosition;

   // Player's Rigidbody
    private Rigidbody rb;
    
    // Static event to notify subscribers of the Player's movement
    public static event Action<int, int> playerMoved;
    
    [Space]
    [Title("Puzzle Completion Event", "Event fired when Player reaches the end tile of the puzzle.")]
    public UnityEvent puzzleCompleted;
    public static event Action<PuzzleInformation> updatePuzzleStatus;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Subscribe to events
    private void OnEnable()
    {
        InteractablePillar.puzzleTriggered += UpdatePuzzleInformation;
        ResetPuzzle.resetPuzzle += ResetPlayerPosition;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        InteractablePillar.puzzleTriggered -= UpdatePuzzleInformation;
        ResetPuzzle.resetPuzzle -= ResetPlayerPosition;
    }
    
    /// <summary>
    /// This method is called on the puzzleTriggered event. It receives data
    /// regarding the current puzzle, such as the starting and ending tiles.
    /// This data is packaged and sent from the InteractablePillar.cs script.
    /// </summary>
    /// <param name="info"></param>
    private void UpdatePuzzleInformation(PuzzleInformation info)
    {
        puzzleInfo = info;
        gridManager = puzzleInfo.gridManager.GetComponent<GridManager>();
        startTile = puzzleInfo.startTile;
        endTile = puzzleInfo.endTile;

        transform.parent = puzzleInfo.gameObject.transform.GetChild(0);

        // Get the grid coordinates of the starting tile
        startTileX = startTile.GetComponent<SelectableTile>().gridX;
        startTileZ = startTile.GetComponent<SelectableTile>().gridZ;

        Debug.Log($"PlayerFixedMovement.cs >> Starting X,Z: ({startTileX},{startTileZ})");
        
        // Get the grid coordinates of the end tile
        endTileX = endTile.GetComponent<SelectableTile>().gridX;
        endTileZ = endTile.GetComponent<SelectableTile>().gridZ;
        
        // After gathering data, move Player to the startTile
        //TryToMovePlayer(startTileX, startTileZ);
        MovePlayerToStartTile(startTileX, startTileZ);
    }

    /// <summary>
    /// This method simply moves the Player to the starting tile. It was created as a
    /// way to break down the complexity of the movement system itself. We may want to
    /// optimize it later on by reducing code redundancy, but for now, it is functional
    /// and should probably be kept for bug testing.
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="startz"></param>
    private void MovePlayerToStartTile(int startX, int startZ)
    {
        Debug.Log($"PlayerFixedMovement.cs >> Moving Player to the starting tile ({startX},{startZ})...");
        SnapPlayerToTile(startX, startZ);
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
        MoveDirection(0,1);
    }
    
    public void MoveDown(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveDown called.");
        MoveDirection(0, -1);
    }
    
    public void MoveLeft(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveLeft called.");
        MoveDirection(-1, 0);
    }
    
    public void MoveRight(InputAction.CallbackContext context)
    {
        // Ensures that the action is only performed once per key press.
        if (!context.performed) return;
        
        Debug.Log("PlayerFixedMovement.cs >> MoveRight called.");
        MoveDirection(1,0);
    }
    
    /// <summary>
    /// This method is used to reduce redundancy in the directional movement methods.
    /// </summary>
    /// <param name="xDir"></param>
    /// <param name="zDir"></param>
    private void MoveDirection(int xDir, int zDir)
    {
        destinationX = 0;
        destinationZ = 0;
        TryToMovePlayer(xDir, zDir);
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
        deltaX = xDir;
        deltaZ = zDir;
        
        Debug.Log($"PlayerFixedMovement.cs >> destinationX,destinationZ: ({destinationX},{destinationZ})");
        Debug.Log($"PlayerFixedMovement.cs >> deltaX,deltaZ: ({deltaX},{deltaZ})");
        Debug.Log("PlayerFixedMovement.cs >> Now adding the delta values onto the destination values...");

        destinationX += deltaX;
        destinationZ += deltaZ;
        
        Debug.Log($"PlayerFixedMovement.cs >> destinationX,destinationZ: ({destinationX},{destinationZ})");
        Debug.Log($"PlayerFixedMovement.cs >> destinationX + playerGridX = {destinationX + playerGridX}");
        Debug.Log($"PlayerFixedMovement.cs >> destinationZ + playerGridZ = {destinationZ + playerGridZ}");

        Debug.Log($"PlayerFixedMovement.cs >> Attempting to move the Player to: {destinationX + playerGridX},{destinationZ + playerGridZ}");
        
        SelectableTile.TileType tileType = gridManager.GetTileType(destinationX + playerGridX, destinationZ + playerGridZ);

        switch (tileType)
        {
            case SelectableTile.TileType.Normal:
                Debug.Log("PlayerFixedMovement.cs >> Normal tile detected.");
                break;
            case SelectableTile.TileType.Ice:
                Debug.Log("PlayerFixedMovement.cs >> Ice tile detected.");
                break;
        }
        
        // Instead call CheckForEmptyCell() method to reduce redundancy and clean up code. -- Nikki
        // CheckForEmptyCell(destinationX + playerGridX, destinationZ + playerGridZ);
        
        // if (gridManager.IsCellEmpty(destinationX + playerGridX, destinationZ + playerGridZ))
        // {
        //     Debug.Log($"PlayerFixedMovement.cs >> There is no tile to jump to at: {destinationX + playerGridX},{destinationX + playerGridX}");
        //     return;
        // }

        // Instead call BoundsCheck() method. -- Nikki
        // if (!gridManager.IsInsideGrid(destinationX + playerGridX, destinationZ + playerGridZ))
        // {
        //     Debug.Log("PlayerFixedMovement.cs >> Move blocked: Outside grid");
        //     return;
        // }

        // Debug.Log($"PlayerFixedMovement.cs >> Destination: ({destinationX},{destinationZ})");
        // Debug.Log($"PlayerFixedMovement.cs >> Destination + CurrentPosition: ({destinationX + playerGridX},{destinationZ + playerGridZ})");
        //
        // // We should have something here to handle normal tiles as well as ice tiles.
        //
        // if (gridManager.IsIceTileType(destinationX + playerGridX, destinationZ + playerGridZ))
        // {
        //     Debug.Log("Is Ice");
        //     
        //      if (!gridManager.IsCellEmpty(destinationX + playerGridX, destinationZ + playerGridZ))
        //      {
        //          SnapPlayerToTile(destinationX + playerGridX, destinationZ + playerGridZ);
        //      }
        //     
        //     TryToMovePlayer(deltaX, deltaZ);
        //
        // }
        // else
        // {
        //
        //     landingX = destinationX + playerGridX;
        //     landingZ = destinationZ + playerGridZ;
        //
        //     if (gridManager.IsCellEmpty(landingX,landingZ))
        //     {
        //
        //         landingX = playerGridX;
        //         landingZ = playerGridZ;
        //
        //     }
        //
        // }
        //
        // // TODO: Remove gridX and gridZ? My IDE said they are
        // //       assigned but never used. Lmk. -- Nikki
        // int gridX = newX;
        // int gridZ = newZ;
        //
        // // TODO: Add tile type checking here to handle special mechanics.
        // //       The same should be done with a normal tile.
        // // HandleTileType();
        //
        // Debug.Log($"PlayerFixedMovement.cs >> Destination + PlayerGrid: ({destinationX + playerGridX},{destinationZ + playerGridZ})");
        // Debug.Log($"PlayerFixedMovement.cs >> Landings: ({landingX},{landingZ})");
        //
        // // For right now, we will just snap the player to the new tile.
        // SnapPlayerToTile(landingX, landingZ);
    }

    /// <summary>
    /// Checks for an empty cell.
    /// </summary>
    /// <param name="coordX"></param>
    /// <param name="cordZ"></param>
    public void CheckForEmptyCell(int coordX, int coordZ)
    {
        if (gridManager.IsCellEmpty(coordX, coordZ))
        {
            Debug.Log($"PlayerFixedMovement.cs >> There is no tile to jump to at: ({coordX},{coordZ})");
            return;
        }
    }
    
    /// <summary>
    /// Checks that a tile is within bounds of the grid.
    /// </summary>
    /// <param name="coordX"></param>
    /// <param name="coordZ"></param>
    public void BoundsCheck(int coordX, int coordZ)
    {
        if (!gridManager.IsInsideGrid(coordX, coordZ))
        {
            Debug.Log($"PlayerFixedMovement.cs >> Move blocked: ({coordX},{coordZ}) is outside the grid.");
            return;
        }
    }
    
    public void SnapPlayerToTile(int coordX, int cordZ)
    {
        // Grab the X and Z coordinates in Vector3 from the GridManager
        newCoords = gridManager.GridToWorld(coordX, cordZ);
        newPosition = new Vector3(newCoords.x, transform.localPosition.y, newCoords.z);
        Debug.Log($"PlayerFixedMovement.cs >> New Position: {newPosition}");
        transform.localPosition = newPosition;

        playerGridX = coordX;
        playerGridZ = cordZ;

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
            Debug.Log($"PlayerFixedMovement.cs >> Player has reached the end tile at ({endTileX}, {endTileZ}).");

            transform.parent = null;
            transform.position = new Vector3(3.59f, 0.83f, 21.43f);

            puzzleCompleted.Invoke();
        }
        
        playerMoved?.Invoke(playerGridX, playerGridZ);
    }

    private void ResetPlayerPosition()
    {
        // Get the grid coordinates of the starting tile
        startTileX = startTile.GetComponent<SelectableTile>().gridX;
        startTileZ = startTile.GetComponent<SelectableTile>().gridZ;

        Debug.Log("Starting X,Z: " + startTileX + "," + startTileZ);

        // After gathering data, move Player to the startTile
        SnapPlayerToTile(startTileX, startTileZ);
    }
}