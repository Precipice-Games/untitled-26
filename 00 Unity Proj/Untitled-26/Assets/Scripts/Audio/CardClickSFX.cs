using UnityEngine;

public class CardClickSFX : MonoBehaviour
{
    public void PlaySound()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayCardClick();
        }  
    }
}
