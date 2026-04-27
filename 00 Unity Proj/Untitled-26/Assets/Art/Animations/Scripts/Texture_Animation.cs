using System;
using UnityEngine;

public class Texture_Animation : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer animationRendererSprite;

    [SerializeField]
    private float _frameCounter = 0.0f;

    [SerializeField]
    private Sprite[] _idleSprites = new Sprite[23];
    [SerializeField]
    private Sprite[] _runningSprites = new Sprite[27];
    [SerializeField]
    public bool _isRunning = false;
    [SerializeField]
    private Sprite[] _jumpingSprites = new Sprite[19];
    [SerializeField]
    private bool _isJumping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        animationRendererSprite = GetComponent<SpriteRenderer>();

        // Commenting this out; added through a merge conflict.
        // Vector2 currentScale = GetComponent<MeshRenderer>().material.mainTextureScale;

        //currentScale.y *= -1f;

        // GetComponent<MeshRenderer>().material.mainTextureScale = currentScale;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log(transform.parent.GetComponent<CharacterController>().velocity);

        if (transform.parent.GetComponent<CharacterController>().velocity.x != 0 || transform.parent.GetComponent<CharacterController>().velocity.z != 0)
        {

            _isRunning = true;

        }
        else
        {
            _isRunning = false;
        }

        if (Math.Abs(transform.parent.GetComponent<CharacterController>().velocity.y) >= 0.2)
        {
            _isJumping = true;
        }
        else
        {
            _isJumping = false;
        }

        if (_isRunning == false && _isJumping == false)
        {

            if (_frameCounter < _idleSprites.Length)
            {

                //Debug.Log(_idleTextures[(int)_frameCounter].name);
                //Debug.Log(_idleTextures[(int)_frameCounter]);

                animationRendererSprite.sprite = _idleSprites[(int)_frameCounter];

                _frameCounter += 15f * Time.deltaTime;


            }
            else
            {
                _frameCounter = 0;
            }

        }
        else if (_isJumping == true)
        {

            if (_frameCounter < _jumpingSprites.Length)
            {

                //Debug.Log(_idleTextures[(int)_frameCounter].name);
                //Debug.Log(_idleTextures[(int)_frameCounter]);

                animationRendererSprite.sprite = _jumpingSprites[(int)_frameCounter];

                _frameCounter += 15f * Time.deltaTime;


            }
            else
            {
                _frameCounter = 0;
            }
        }
        else
        {


            if (_frameCounter < _runningSprites.Length)
            {

                //Debug.Log(_idleTextures[(int)_frameCounter].name);
                //Debug.Log(_idleTextures[(int)_frameCounter]);

                animationRendererSprite.sprite = _runningSprites[(int)_frameCounter];

                _frameCounter += 15f * Time.deltaTime;


            }
            else
            {
                _frameCounter = 0;
            }
        }

    }
}
