using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractablePillar : MonoBehaviour, IInteractable
{
    public void Interaction()
    {
        Debug.Log("Interacting");
        SceneManager.LoadScene("TileMoveExperiment");

    }
}
