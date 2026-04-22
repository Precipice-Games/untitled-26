using UnityEngine;

public class FlowerTexturing : DynamicTileTexturing
{
    // Please insert whatever the flower island's special
    // or specific tiles are supposed to be.
    //
    // e.g., if they have mana wells, then insert the following:
    // public Material manaWellTexture;
    
    protected override void AssignTileTexture(SelectableTile.TileType tileType)
    {
        // Assign the normal texture for this given island
        base.AssignTileTexture(tileType);
        
        // if (tileType == SelectableTile.TileType.ManaWell)
        // {
        //     tileRenderer.material = manaWellTexture;
        // }
    }
}