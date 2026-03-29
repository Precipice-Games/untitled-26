using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// TODO: Might need to revamp this script, or the way we're generally assigning
// textures because I noticed, when you enter Puzzle Mode, the normal tiles only
// appear white. I don't even think it's like a reflection thing, because I
// was able to go into freecam mode and I noticed that the Normal Tiles had their
// textures seemingly wiped? -- Nikki

public class DynamicTileTexturing : MonoBehaviour
{
    [Title("Tile Textures")]
    [EnumToggleButtons, HideLabel]
    [InfoBox("Attach the textures for different tile types here.")]
    
    private SelectableTile selectableTile;
    private SelectableTile.TileType tileType;
    public Renderer tileRenderer;
    public GameObject tile;
    public Material normalTexture;
    public Material iceTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        selectableTile = GetComponentInParent<SelectableTile>();
        tile = selectableTile.gameObject;
        tileType = selectableTile.tileType;
        tileRenderer = tile.GetComponent<Renderer>();
        AssignTileTexture(tileType);
    }
    
    public void AssignTileTexture(SelectableTile.TileType tileType)
    {
        if (tileType == SelectableTile.TileType.Normal)
        {
            tileRenderer.material = normalTexture;
        }
        
        if (tileType == SelectableTile.TileType.Ice)
        {
            Vector2 currentScale = GetComponent<MeshRenderer>().material.mainTextureScale;

            currentScale.x *= -1f;

            GetComponent<MeshRenderer>().material.mainTextureScale = currentScale;
            
            tileRenderer.material = iceTexture;
        }
    }
}