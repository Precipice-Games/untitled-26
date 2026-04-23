using UnityEngine;

public class IceTexturing : DynamicTileTexturing
{
    public Material iceTexture;
    
    protected override void AssignTileTexture(SelectableTile.TileType tileType)
    {
        // Assign the normal texture for this given island
        base.AssignTileTexture(tileType);
        
        if (tileType == SelectableTile.TileType.Ice)
        {
            tileRenderer.material = iceTexture;
        }
    }
}