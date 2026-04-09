using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SelectableTile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        Ice,
        ManaWell
    }

    // Default tile type is Normal
    public TileType tileType = TileType.Normal;
    
    public int startingGridX;
    public int startingGridZ;
    public int gridX;
    public int gridZ;
    
    // Reference to the GridManager for this specific puzzle
    public GridManager gridManager;
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

        // Set color based on tile type
        // For now, ManaWell tiles are purple so we can test them visually.
        if (tileType == TileType.ManaWell)
        {
            rend.material.color = Color.magenta;
        }

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

    public bool TryMove(int xDir, int zDir)
    {
        int newX = gridX + xDir;
        int newZ = gridZ + zDir;

        Debug.Log($"SelectableTile.cs >> {name} trying move to: ({newX},{newZ})");
        
        // Before anything, check to see if the attempted move is valid.
        if (CheckForOutOfBounds(newX, newZ)) return false; // Must be inside the grid
        if (!CheckForEmptyCell(newX, newZ)) return false; // Must be an empty cell
        
        gridManager.ClearCell(gridX, gridZ);
        
        gridX = newX;
        gridZ = newZ;

        gridManager.PlaceTile(this, gridX, gridZ);

        transform.localPosition = gridManager.GridToWorld(gridX, gridZ);

        Debug.Log($"SelectableTile.cs >> {name} moved to: ({gridX},{gridZ})");

        return true;
    }
    
    /// <summary>
    /// Checks for an empty cell.
    /// </summary>
    /// <param name="coordX"></param>
    /// <param name="coordZ"></param>
    private bool CheckForEmptyCell(int coordX, int coordZ)
    {
        // If it's empty, return true.
        if (gridManager.IsCellEmpty(coordX, coordZ))
        {
            return true;
        }
        
        Debug.Log($"SelectableTile.cs >> BLOCKED: Cell at ({coordX},{coordZ}) is occupied.");
        
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
            moveOutOfBounds?.Invoke(this);
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

        // Reset tile color after reset
        if (tileType == TileType.ManaWell)
        {
            originalColor = Color.magenta;
        }

        rend.material.color = originalColor;
    }
}