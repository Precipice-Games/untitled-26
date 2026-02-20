using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 4;
    public int height = 3;
    public float tileSize = .25f;

    private SelectableCube[,] grid;

    void Awake()
    {
        Instance = this;
        grid = new SelectableCube[width, height];
    }

    public bool IsInsideGrid(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 && z < height;
    }

    public bool IsCellEmpty(int x, int z)
    {
        return grid[x, z] == null;
    }

    public void PlaceTile(SelectableCube tile, int x, int z)
    {
        grid[x, z] = tile;
    }

    public void ClearCell(int x, int z)
    {
        grid[x, z] = null;
    }

    public Vector3 GridToWorld(int x, int z)
    {
        return new Vector3(x * tileSize, 0, z * tileSize);
    }
}