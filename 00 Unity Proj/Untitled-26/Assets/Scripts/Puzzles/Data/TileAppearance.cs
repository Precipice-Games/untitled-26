using UnityEngine;

// This script is used to set the appearance of the start and end tiles.
// It won't necessarily be used as a long-term solution, but it's good for
// testing purposes to identify where the start and end tiles are located.

public class TileAppearance : MonoBehaviour
{
    public GameObject startTile;
    public GameObject endTile;
    private Renderer startTileRend;
    private Renderer endTileRend;
    
    // Subscribe to events
    private void OnEnable()
    {
        SelectableTile.tilesDistributed += SetStartAndEndAppearance;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        SelectableTile.tilesDistributed -= SetStartAndEndAppearance;
    }

    private void SetStartAndEndAppearance()
    {
        startTileRend = startTile.GetComponent<Renderer>();
        endTileRend = endTile.GetComponent<Renderer>();
        
        startTileRend.material.color = Color.green;
        endTileRend.material.color = Color.red;
    }
}