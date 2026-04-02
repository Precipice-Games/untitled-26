using UnityEngine;

public class Texture_Animation : MonoBehaviour
{

    [SerializeField]
    private Material _idleAnimationMaterial;

    [SerializeField]
    private float _frameCounter = 0.0f;

    [SerializeField]
    private Texture2D[] _idleTextures = new Texture2D[31];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
        _idleAnimationMaterial = GetComponent<MeshRenderer>().materials[0];
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (_frameCounter < _idleTextures.Length)
        {

            //Debug.Log(_idleTextures[(int)_frameCounter].name);
            //Debug.Log(_idleTextures[(int)_frameCounter]);

            _idleAnimationMaterial.mainTexture = _idleTextures[(int)_frameCounter];

            _frameCounter += 15f * Time.deltaTime;


        }
        else
        {
            _frameCounter = 0;
        }

    }
}
