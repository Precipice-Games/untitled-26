using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChangerButton : MonoBehaviour
{
    /// <summary>
    /// The UI canvas that this button will activate when clicked. This should be set in the Unity editor.
    /// </summary>
    public GameObject targetUI;
    /// <summary>
    /// The UI canvas that is currently visible.
    /// </summary>
    private GameObject currentUI;


    public void swapUI()
    {
        currentUI = getCurrentUI();

        if (currentUI != null)
        {
            currentUI.SetActive(false);
        }

        targetUI.SetActive(true);
        Debug.Log($"ViewManager.cs >> Enabled UI Canvas: {targetUI}");
    }

    /// <summary>
    /// Get the current UI canvas that is active. This method assumes that the button is a child of the current UI canvas, so it will return the parent game object of the button as the current UI canvas.
    /// </summary>
    /// <returns></returns>
    private GameObject getCurrentUI()
    {
        return this.gameObject.transform.parent.gameObject;
    }


}
