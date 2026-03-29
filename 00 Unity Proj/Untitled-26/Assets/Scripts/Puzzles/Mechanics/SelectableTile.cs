using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SelectableTile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        Ice
    }

    // Default tile type is Normal
    public TileType tileType = TileType.Normal;

    public int startingGridX;
    public int startingGridZ;
    public int gridX;
    public int gridZ;
    
    // Reference to the GridManager for this specific puzzle.
    public GridManager gridManager;
    
    // NOTE: Remember how I said that we couldn't have more than one puzzle active
    // at a time in order for things to work? I think it has to do with the fact
    // that each puzzle has its own GridManager script, which was attempting to
    // create a singleton that all could reference, but I think it's confusing
    // the other scripts looking to reference it. In the meantime, I went ahead
    // and attached the GridManager to each of the tiles in Puzzle1.prefab.
    // -- Nikki

    private Renderer rend;
    
    // Making originalColor public for now to tell
    // which are the start and end tiles.
    public Color originalColor;
    public Color highlightColor = Color.yellow;
    
    [Title("Debug Mode")]
    [InfoBox("Check this variable if you want messages to be debugged from this script. If not, uncheck it.")]
    [PropertyTooltip("Enables or disables debug logs in a given script.")]
    public bool debugMode = true;
    
    // Event fired when a move is blocked by an occupied cell
    public static event Action<SelectableTile> cellOccupied;
    // Event fired when a move is out of bounds
    public static event Action<SelectableTile> moveOutOfBounds;

    // Subscribe to events
    private void OnEnable()
    {
        ResetPuzzle.resetPuzzle += ResetTiles;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        ResetPuzzle.resetPuzzle -= ResetTiles;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        rend.material.color = originalColor;
        // rend.material.SetColor("_BaseColor", originalColor);

        startingGridX = gridX;
        startingGridZ = gridZ;

        if (debugMode) Debug.Log(name + " starting at: " + gridX + "," + gridZ);

        gridManager.PlaceTile(this, gridX, gridZ);
        transform.localPosition = gridManager.GridToWorld(gridX, gridZ);
    }

    // Run when a tile is selected
    public void Select()
    {
        rend.material.color = highlightColor;
        Debug.Log($"SelectableTile.cs >> {name} SELECTED at: ({gridX},{gridZ})");
    }

    // Run when a tile is deselected
    public void Deselect()
    {
        rend.material.color = originalColor;
        Debug.Log($"SelectableTile.cs >> {name} DESELECTED");
    }

    public void TryMove(int xDir, int zDir)
    {
        int newX = gridX + xDir;
        int newZ = gridZ + zDir;

        Debug.Log($"SelectableTile.cs >> {name} trying move to: ({newX},{newZ})");
        
        // Before anything, check to see if the attempted move is valid.
        if (CheckForOutOfBounds(newX, newZ)) return; // Must be inside the grid
        if (!CheckForEmptyCell(newX, newZ)) return; // Must be an empty cell
        
        gridManager.ClearCell(gridX, gridZ);
        
        gridX = newX;
        gridZ = newZ;

        gridManager.PlaceTile(this, gridX, gridZ);

        transform.localPosition = gridManager.GridToWorld(gridX, gridZ);

        Debug.Log($"SelectableTile.cs >> {name} moved to: ({gridX},{gridZ})");
    }
    
    /// <summary>
    /// Checks for an empty cell.
    /// </summary>
    /// <param name="coordX"></param>
    /// <param name="coordZ"></param>
    private bool CheckForEmptyCell(int coordX, int coordZ)
    {
        if (gridManager.IsCellEmpty(coordX, coordZ))
        {
            Debug.Log($"SelectableTile.cs >> BLOCKED: Cell at ({coordX},{coordZ}) occupied – no mana spent");
            return true; // If it's empty, return true.
        }
        
        // Fire off an event to say that a cell is occupied
        cellOccupied?.Invoke(this);
        return false;
    }
    
    /// <summary>
    /// Checks that a tile is out of bounds of the grid. If it's
    /// out of bounds, return true.
    /// </summary>
    /// <param name="coordX"></param>
    /// <param name="coordZ"></param>
    private bool CheckForOutOfBounds(int coordX, int coordZ)
    {
        // If it's out of bounds, return true.
        if (!gridManager.IsInsideGrid(coordX, coordZ))
        {
            Debug.Log("SelectableTile.cs >> BLOCKED: Outside grid – no mana spent");
            // Fire off an event to say that the move is out of bounds
            return true;
        }
        return false;
    }

    /// <summary>
    /// Resets tiles to their original positions. This is called when the ResetPuzzle event is triggered.
    /// </summary>
    private void ResetTiles()
    {
        gridX = startingGridX;
        gridZ = startingGridZ;
        transform.localPosition = gridManager.GridToWorld(gridX, gridZ);
    }
}