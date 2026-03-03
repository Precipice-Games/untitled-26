using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractablePillar : MonoBehaviour, IInteractable
{

    public GameStateManager GameStateManager;

    public void Interaction()
    {
        Debug.Log("Interacting");

        GameStateManager.onPuzzleTrigger();
    }
}
