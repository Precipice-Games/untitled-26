using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlaySound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
