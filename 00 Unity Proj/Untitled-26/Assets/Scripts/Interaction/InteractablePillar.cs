using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InteractablePillar : MonoBehaviour, IInteractable
{
    public UnityEvent PuzzleTriggered;

    public void Interaction()
    {
        Debug.Log("Interacting");

        // Invoke this event. The onPuzzleTrigger() event from
        // GameStateManager.cs has been subscribed to this event
        // in the Unity Editor.
        PuzzleTriggered.Invoke();
    }
}
