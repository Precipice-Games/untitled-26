using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    /// <summary>
    /// Reference to the currently selected tile.
    /// </summary>
    private SelectableTile selectedTile;
    
    /// <summary>
    /// Reference to the Player's coordinates on the grid.
    /// </summary>
    private int playerGridX;
    private int playerGridZ;
    
    // Subscribe to events
    private void OnEnable()
    {
        PlayerFixedMovement.playerMoved += UpdatePlayerCoordinates;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        PlayerFixedMovement.playerMoved -= UpdatePlayerCoordinates;
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
    /// Checks if the Player is currently on the selected tile. If so, it prevents
    /// the system from continuing before even attempting to move the tile.
    /// </summary>
    /// <returns></returns>
    private bool PlayerOnSelectedCube()
    {
        if (selectedTile.gridX == playerGridX && selectedTile.gridZ == playerGridZ)
        {
            Debug.Log("Cannot move tile: Player is on it.");
            
            return true;
        }
        return false;
    }

    public void MoveSelectedRight()
    {
        // Ensure that a tile is selected and that the Player is
        // not on the selected tile before trying to move it.
        if (selectedTile == null) return;
        if (PlayerOnSelectedCube()) return;

        if (!ResourceManager.Instance.UseMove("Right")) return;
        
        selectedTile.TryMove(1, 0); // moves right
    }

    public void MoveSelectedLeft()
    {
        // Ensure that a tile is selected and that the Player is
        // not on the selected tile before trying to move it.
        if (selectedTile == null) return;
        if (PlayerOnSelectedCube()) return;

        if (!ResourceManager.Instance.UseMove("Left")) return;
        
        selectedTile.TryMove(-1, 0); // moves left
    }

    public void MoveSelectedForward()
    {
        // Ensure that a tile is selected and that the Player is
        // not on the selected tile before trying to move it.
        if (selectedTile == null) return;
        if (PlayerOnSelectedCube()) return;

        if (!ResourceManager.Instance.UseMove("Forward")) return;
        
        selectedTile.TryMove(0, 1); // moves forward
    }

    public void MoveSelectedBack()
    {
        // Ensure that a tile is selected and that the Player is
        // not on the selected tile before trying to move it.
        if (selectedTile == null) return;
        if (PlayerOnSelectedCube()) return;

        if (!ResourceManager.Instance.UseMove("Back")) return;
        
        selectedTile.TryMove(0, -1); // moves back
    }
}