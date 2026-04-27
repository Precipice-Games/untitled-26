using UnityEngine;

public class MenuSFX : MonoBehaviour
{
    public void PlayMenuClick()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayMenu();
        }
    }
}
