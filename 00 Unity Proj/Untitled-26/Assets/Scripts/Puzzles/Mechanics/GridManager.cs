using Sirenix.OdinInspector;
using UnityEngine;

//[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    [Space]
    [Title("Grid Settings", "Settings for the puzzle grid.")]
    [PropertyTooltip("Grid Width.")]
    public int width = 4;
    [PropertyTooltip("Grid Height.")]
    public int height = 3;
    [PropertyTooltip("Size of tiles.")]
    public float tileSize = .25f;

    // Grid of selectable tiles
    private SelectableTile[,] grid;

    void Awake()
    {
        // An additional 2 rows and columns are added to the
        // grid to allow for the start and end rune tiles.
        width += 2;
        height += 2;
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
        return x > 0 && x < width - 1 && z > 0 && z < height - 1;
    }

    /// <summary>
    /// Check if movement action is within grid for player. 
    /// This is used to allow the player to move onto the start and end tiles, outside the area allowed for other tiles.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public bool IsInsideGridPlayer(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 && z < height;
    }

    /// <summary>
    /// Check if a cell is vacant. If it is, return true.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public bool IsCellEmpty(int x, int z)
    {
        return grid[x, z] == null;
    }
    
    /// <summary>
    /// Returns the TileType of a given tile.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public SelectableTile.TileType GetTileType(int x, int z)
    {
        SelectableTile.TileType type = grid[x, z].tileType;
        return type;
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