using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InteractableCrystal : MonoBehaviour, IInteractable
{
    [Title("CrystalCollected", "This event is fired when the crystal has been collected.")]
    public UnityEvent crystalCollected;

    [SerializeField]
    PuzzleInformation finalPuzzle;

    private void FixedUpdate()
    {
        // When the final puzzle is completed, ensure it rises up from the ground
        if (finalPuzzle != null && finalPuzzle.puzzleSolved == true && this.transform.position.y < 3.2)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        }
    }

    /// <summary>
    /// Runs when interacting with a collectable crystal. This triggers
    /// crystalCollected and destroys the game object.
    /// </summary>
    public void Interaction()
    {
        crystalCollected.Invoke();
        Destroy(gameObject);
    }
}
