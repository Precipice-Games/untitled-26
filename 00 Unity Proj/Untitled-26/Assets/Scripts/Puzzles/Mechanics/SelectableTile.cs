using Sirenix.OdinInspector;
using UnityEngine;

public class SelectableTile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        Ice
    }

    public TileType tileType = TileType.Normal;

    public int gridX;
    public int gridZ;

    private Renderer rend;
    
    // Making originalColor public for now to tell
    // which are the start and end tiles.
    public Color originalColor;
    public Color highlightColor = Color.yellow;
    
    [Title("Debug Mode")]
    [InfoBox("Check this variable if you want messages to be debugged from this script. If not, uncheck it.")]
    [PropertyTooltip("Enables or disables debug logs in a given script.")]
    public bool debugMode = true;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        rend.material.color = originalColor;
        // rend.material.SetColor("_BaseColor", originalColor);

        if (debugMode) Debug.Log(name + " starting at: " + gridX + "," + gridZ);

        GridManager.Instance.PlaceTile(this, gridX, gridZ);
        transform.position = GridManager.Instance.GridToWorld(gridX, gridZ);
    }

    public void Select()
    {
        rend.material.color = highlightColor;
        if (debugMode) Debug.Log(name + " SELECTED");
    }

    public void Deselect()
    {
        rend.material.color = originalColor;
        if (debugMode) Debug.Log(name + " DESELECTED");
    }

    public void TryMove(int xDir, int zDir)
    {
        int newX = gridX + xDir;
        int newZ = gridZ + zDir;

        if (debugMode) Debug.Log(name + " trying move to: " + newX + "," + newZ);

        // Check that it's inside the grid
        if (!GridManager.Instance.IsInsideGrid(newX, newZ))
        {
            if (debugMode) Debug.Log("BLOCKED: Outside grid – no mana spent");
            return;
        }

        // Check for cell vacancy
        if (!GridManager.Instance.IsCellEmpty(newX, newZ))
        {
            if (debugMode) Debug.Log("BLOCKED: Cell occupied – no mana spent");
            return;
        }

        GridManager.Instance.ClearCell(gridX, gridZ);

        gridX = newX;
        gridZ = newZ;

        GridManager.Instance.PlaceTile(this, gridX, gridZ);

        transform.position = GridManager.Instance.GridToWorld(gridX, gridZ);

        Debug.Log(name + " moved to: " + gridX + "," + gridZ);

    }
}