using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    // ===== Variables =====
    private GridManager gridManager;
    [PropertyTooltip("Please assign the ResourceManager for this specific puzzle prefab.")]
    public ResourceManager resourceManager;

    /// <summary>
    /// Reference to the currently selected tile.
    /// </summary>
    private SelectableTile selectedTile;
    
    /// <summary>
    /// Reference to the Player's coordinates on the grid.
    /// </summary>
    private int playerGridX;
    private int playerGridZ;

    // The starting and ending tile's coordinates
    private int startTileX;
    private int startTileZ;
    private int endTileX;
    private int endTileZ;
    
    [Title("Debug Mode")]
    [InfoBox("Check this variable if you want messages to be debugged from this script. If not, uncheck it.")]
    [PropertyTooltip("Enables or disables debug logs in a given script.")]
    public bool debugMode = true;

    // Subscribe to events
    private void OnEnable()
    {
        PlayerFixedMovement.playerMoved += UpdatePlayerCoordinates;
        RuneCircle.puzzleTriggered += AssignStartAndEndTiles;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerFixedMovement.playerMoved -= UpdatePlayerCoordinates;
        RuneCircle.puzzleTriggered += AssignStartAndEndTiles;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SelectableTile tile = hit.collider.GetComponent<SelectableTile>();
                if (tile != null)
                {
                    if (selectedTile != null)
                        selectedTile.Deselect();

                    selectedTile = tile;
                    selectedTile.Select();
                }
            }
        }
    }

    /// <summary>
    /// Used to update the reference to the Player's coordinates on the grid. It
    /// ensures that we're not moving the tile that the Player is currently on.
    /// </summary>
    /// <param name="playerX"></param>
    /// <param name="playerZ"></param>
    private void UpdatePlayerCoordinates(int playerX, int playerZ)
    {
        playerGridX = playerX;
        playerGridZ = playerZ;
    }

    /// <summary>
    /// Assigns the coordinates of the start and end tiles for the current puzzle.
    /// This is used to prevent the Player from moving these tiles.
    /// </summary>
    /// <param name="puzzleInformation"></param>
    private void AssignStartAndEndTiles(PuzzleInformation puzzleInfo)
    {
        // Assign the GridManager and the Start & End tiles
        gridManager = puzzleInfo.gridManager.GetComponent<GridManager>();
        GameObject startTile = puzzleInfo.startTile;
        GameObject endTile = puzzleInfo.endTile;
        
        // Get the grid coordinates of the starting tile
        startTileX = startTile.GetComponent<SelectableTile>().gridX;
        startTileZ = startTile.GetComponent<SelectableTile>().gridZ;
        
        // Get the grid coordinates of the end tile
        endTileX = endTile.GetComponent<SelectableTile>().gridX;
        endTileZ = endTile.GetComponent<SelectableTile>().gridZ;
    }
    
    /// <summary>
    /// Checks if the Player is currently on the selected tile. If so, it prevents
    /// the system from continuing before even attempting to move the tile.
    /// </summary>
    /// <returns></returns>
    private bool PlayerOnSelectedTile()
    {
        if (selectedTile.gridX == playerGridX && selectedTile.gridZ == playerGridZ)
        {
            if (debugMode) Debug.Log("TileSelector.cs >> Cannot the tile the Player is currently on.");
            
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the current tile is either the start or end tile.
    /// </summary>
    /// <returns></returns>
    private bool SelectedTileIsStartOrEnd()
    {
        // Check for Start tile
        if (selectedTile.gridX == startTileX && selectedTile.gridZ == startTileZ)
        {
            if (debugMode) Debug.Log("TileSelector.cs >> Cannot move the Start tile.");

            return true;
        }

        // Check for End tile
        if (selectedTile.gridX == endTileX && selectedTile.gridZ == endTileZ)
        {
            if (debugMode) Debug.Log("TileSelector.cs >> Cannot move the End tile.");
            return true;
        }

        return false;
    }

    public void MoveSelectedRight()
    {
        // Ensure that a tile is selected, and that we're not moving
        // the start tile, the end tile, or the Player-occupied tile.
        if (selectedTile == null) return;
        if (PlayerOnSelectedTile()) return;
        if (SelectedTileIsStartOrEnd()) return;

        if (!resourceManager.UseMove("Right")) return;
        
        selectedTile.TryMove(1, 0); // moves right
    }

    public void MoveSelectedLeft()
    {
        // Ensure that a tile is selected, and that we're not moving
        // the start tile, the end tile, or the Player-occupied tile.
        if (selectedTile == null) return;
        if (PlayerOnSelectedTile()) return;
        if (SelectedTileIsStartOrEnd()) return;

        if (!resourceManager.UseMove("Left")) return;
        
        selectedTile.TryMove(-1, 0); // moves left
    }

    public void MoveSelectedForward()
    {
        // Ensure that a tile is selected, and that we're not moving
        // the start tile, the end tile, or the Player-occupied tile.
        if (selectedTile == null) return;
        if (PlayerOnSelectedTile()) return;
        if (SelectedTileIsStartOrEnd()) return;

        if (!resourceManager.UseMove("Forward")) return;
        
        selectedTile.TryMove(0, 1); // moves forward
    }

    public void MoveSelectedBack()
    {
        // Ensure that a tile is selected, and that we're not moving
        // the start tile, the end tile, or the Player-occupied tile.
        if (selectedTile == null) return;
        if (PlayerOnSelectedTile()) return;
        if (SelectedTileIsStartOrEnd()) return;

        if (!resourceManager.UseMove("Back")) return;
        
        selectedTile.TryMove(0, -1); // moves back
    }
}