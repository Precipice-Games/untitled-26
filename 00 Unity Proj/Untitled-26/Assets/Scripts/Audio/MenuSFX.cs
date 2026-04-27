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

    public void PlayExitButton()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayExitButton();
        }
    }
}
