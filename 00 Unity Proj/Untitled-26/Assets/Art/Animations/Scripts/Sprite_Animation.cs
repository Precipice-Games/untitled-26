using UnityEngine;

public class Sprite_Animation : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _frameCounter = 0.0f;

    [SerializeField]
    private Sprite[] _idleSprites = new Sprite[38];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (_frameCounter < _idleSprites.Length)
        {

            //Debug.Log(_idleSprites[(int)_frameCounter].name);
            //Debug.Log(_idleSprites[(int)_frameCounter]);

            _spriteRenderer.sprite = _idleSprites[(int)_frameCounter];

            _frameCounter += 15f * Time.deltaTime;


        }
        else
        {
            _frameCounter = 0;
        }

    }
}
