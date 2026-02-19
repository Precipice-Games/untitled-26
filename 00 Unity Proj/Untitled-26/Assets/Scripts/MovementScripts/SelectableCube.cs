using UnityEngine;

public class SelectableCube : MonoBehaviour
{
    public int gridX;
    public int gridZ;

    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

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

        if (!GridManager.Instance.IsInsideGrid(newX, newZ))
        {
            Debug.Log("BLOCKED: Outside grid");
            return;
        }

        if (!GridManager.Instance.IsCellEmpty(newX, newZ))
        {
            Debug.Log("BLOCKED: Cell occupied");
            return;
        }

        Debug.Log("Move allowed");

        GridManager.Instance.ClearCell(gridX, gridZ);

        gridX = newX;
        gridZ = newZ;

        GridManager.Instance.PlaceTile(this, gridX, gridZ);

        transform.position = GridManager.Instance.GridToWorld(gridX, gridZ);

        Debug.Log(name + " moved to: " + gridX + "," + gridZ);
    }
}