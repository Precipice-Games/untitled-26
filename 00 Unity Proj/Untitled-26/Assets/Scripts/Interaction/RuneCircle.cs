using System;
using UnityEngine;

public class RuneCircle : MonoBehaviour
{
    [Header("Rune Circles")]
    /// <summary>
    /// The other rune circle linked to this puzzle.
    /// If the puzzle is completed, the player will be able to teleport to the other rune circle.
    /// </summary>
    public GameObject otherRuneCircle;

    [Header("UI")]
    public GameObject interactionUI;

    // An event that is invoked when the player stands on the rune circle
    public static event Action<bool> playerInCircle;

    private void FixedUpdate()
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCircle.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCircle.Invoke(false);
        }
    }
}
