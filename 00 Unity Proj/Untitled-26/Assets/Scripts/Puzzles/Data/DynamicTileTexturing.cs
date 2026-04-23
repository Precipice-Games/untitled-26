using Sirenix.OdinInspector;
using UnityEngine;

public class DynamicTileTexturing : MonoBehaviour
{
    protected SelectableTile selectableTile;
    protected SelectableTile.TileType tileType;
    protected Renderer tileRenderer;
    protected GameObject tile;
    
    [Title("Tile Textures", "Attach the textures for different tile types here.")]
    [SerializeField] protected Material normalTexture;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        selectableTile = GetComponentInParent<SelectableTile>();
        tile = selectableTile.gameObject;
        tileType = selectableTile.tileType;
        tileRenderer = tile.GetComponent<Renderer>();
        AssignTileTexture(tileType);
    }
    
    protected virtual void AssignTileTexture(SelectableTile.TileType tileType)
    {
        if (tileType == SelectableTile.TileType.Normal)
        {
            tileRenderer.material = normalTexture;
        }
        
        // Override this method in child classes to assign
        // the appropriate texture for each tile type.
    }
}