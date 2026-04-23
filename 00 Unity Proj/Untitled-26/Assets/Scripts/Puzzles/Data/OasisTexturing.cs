using UnityEngine;

public class OasisTexturing : DynamicTileTexturing
{
    public Material manaWellTexture;
    
    protected override void AssignTileTexture(SelectableTile.TileType tileType)
    {
        // Assign the normal texture for this given island
        base.AssignTileTexture(tileType);
        
        if (tileType == SelectableTile.TileType.ManaWell)
        {
            tileRenderer.material = manaWellTexture;
        }
    }
}