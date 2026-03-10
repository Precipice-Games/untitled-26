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

    void Start()
    {
        rend = GetComponent<Renderer>();
        // originalColor = rend.material.color;
        rend.material.color = originalColor;

        Debug.Log(name + " starting at: " + gridX + "," + gridZ);

        GridManager.Instance.PlaceTile(this, gridX, gridZ);
        transform.position = GridManager.Instance.GridToWorld(gridX, gridZ);
    }

    public void Select()
    {
        rend.material.color = highlightColor;
        Debug.Log(name + " SELECTED");
    }

    public void Deselect()
    {
        rend.material.color = originalColor;
        Debug.Log(name + " DESELECTED");
    }

    public void TryMove(int xDir, int zDir)
    {
        int newX = gridX + xDir;
        int newZ = gridZ + zDir;

        Debug.Log(name + " trying move to: " + newX + "," + newZ);

        // Check that it's inside the grid
        if (!GridManager.Instance.IsInsideGrid(newX, newZ))
        {
            Debug.Log("BLOCKED: Outside grid – no mana spent");
            return;
        }

        // Check for cell vacancy
        if (!GridManager.Instance.IsCellEmpty(newX, newZ))
        {
            Debug.Log("BLOCKED: Cell occupied – no mana spent");
            return;
        }

    }
}