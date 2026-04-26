using UnityEngine;
using UnityEngine.Audio;

///<summary>
///This script manages the sound effects for our game.
///It will handle playing the appropriate SFX for player
///interactions like card clicking and tile moving,
///SFX for impossible tile movement and other feedback sounds,
///as well as eventually handling looping ambiance
///</summary>
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance {get; private set;}    

    //Unity inspector field for the audio source that will play the SFX
    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    //Unity inspector field for the audio mixer group to route the SFX through
    [Header("Mixer Routing")]
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    //Unity inspector fields for the common SFX used in the game (will be expanded as needed)
    [Header("SFX")]
    [SerializeField] private AudioClip cardClickSFX;
    [SerializeField] private AudioClip invalidMoveSFX;
    [SerializeField] private AudioClip puzzleResetSFX;
    [SerializeField] private AudioClip runeCircleSFX;

    private bool suppressNextCardClick = false;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxSource != null && sfxMixerGroup != null)
        {
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        }
    }
    
    private void OnEnable()
    {
        ResetPuzzle.resetPuzzle += OnPuzzleReset;
    }

    private void OnDisable()
    {
        ResetPuzzle.resetPuzzle -= OnPuzzleReset;
    }
    public void PlayCardClick()
    {
        if (suppressNextCardClick)
        {
            suppressNextCardClick = false;
            return;
        }

        PlayClip(cardClickSFX);
    }

    public void PlayInvalidMove()
    {
        suppressNextCardClick = true;
        PlayClip(invalidMoveSFX);
    }

    public void PlayRuneCircle()
    {
        PlayClip(runeCircleSFX);
    }

    private void OnPuzzleReset()
    {
        PlayClip(puzzleResetSFX);
    }

    public void PlayClip(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }
}
