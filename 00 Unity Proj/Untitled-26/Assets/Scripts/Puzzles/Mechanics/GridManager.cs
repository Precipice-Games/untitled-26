using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 4;
    public int height = 3;
    public float tileSize = .25f;

    // Grid of selectable tiles
    private SelectableTile[,] grid;

    void Awake()
    {
        Instance = this;
        grid = new SelectableTile[width, height];
    }

    /// <summary>
    /// Check if the movement action is within the grid.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public bool IsInsideGrid(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 && z < height;
    }

    /// <summary>
    /// Check if a cell is vacant.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public bool IsCellEmpty(int x, int z)
    {
        return grid[x, z] == null;
    }

    /// <summary>
    /// Place a tile in the grid at the given coordinates.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void PlaceTile(SelectableTile tile, int x, int z)
    {
        grid[x, z] = tile;
    }

    /// <summary>
    /// Clear the cell at the given coordinates. This is used when a tile
    /// moves out of its current cell and into a new one.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void ClearCell(int x, int z)
    {
        grid[x, z] = null;
    }

    /// <summary>
    /// Converts grid coordinates to world coordinates. This is used to
    /// position the tiles in the world based on their grid location.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Vector3 GridToWorld(int x, int z)
    {
        return new Vector3(x * tileSize, 0, z * tileSize);
    }
}