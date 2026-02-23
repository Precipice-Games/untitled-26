using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private bool playOnStart = true;

    private void Reset ()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (playOnStart && backgroundMusic != null)
        {
            Play(backgroundMusic);
        }
    }

    public void Play(AudioClip clip, bool restartIfSame = false)
    {
        if (clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying && !restartIfSame)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void Pause()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void Resume()
    {
        if (musicSource.clip != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void Stop()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

    public bool IsPlaying => musicSource.isPlaying;
    public AudioClip CurrentTrack => musicSource.clip;
}
