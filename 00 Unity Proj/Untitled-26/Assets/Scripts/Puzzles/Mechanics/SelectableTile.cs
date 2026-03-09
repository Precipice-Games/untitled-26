using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectableTile : MonoBehaviour
{
    public int gridX;
    public int gridZ;

    private Renderer rend;
    // Making originalColor public for now to tell
    // which are the start and end tiles.
    public Color originalColor;
    public Color highlightColor = Color.yellow;
    public static event Action tilesDistributed;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = originalColor;

        Debug.Log(name + " starting at: " + gridX + "," + gridZ);

        GridManager.Instance.PlaceTile(this, gridX, gridZ);
        transform.position = GridManager.Instance.GridToWorld(gridX, gridZ);
        tilesDistributed?.Invoke();
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

        // Valid move: deduct mana
        if (!ResourceManager.Instance.UseMana(1))
        {
            Debug.Log("Not enough mana to move");
            return;
        }

        Debug.Log("Move allowed – mana deducted");

        GridManager.Instance.ClearCell(gridX, gridZ);

        gridX = newX;
        gridZ = newZ;

        GridManager.Instance.PlaceTile(this, gridX, gridZ);

        transform.position = GridManager.Instance.GridToWorld(gridX, gridZ);

        Debug.Log(name + " moved to: " + gridX + "," + gridZ);
    }
}