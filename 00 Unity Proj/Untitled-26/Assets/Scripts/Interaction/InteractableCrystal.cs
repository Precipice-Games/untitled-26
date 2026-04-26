using System.Dynamic;
using System.Numerics;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Yarn;
using Yarn.Unity;

public class InteractableCrystal : MonoBehaviour, IInteractable
{
    [Title("CrystalCollected", "This event is fired when the crystal has been collected.")]
    public UnityEvent crystalCollected;
    private bool finalPuzzleCompleted;

    DialogueRunner runner;

    [SerializeField]
    GameObject player;

    [SerializeField]
    UnityEngine.Vector3 destination;

    private void FixedUpdate()
    {
        // When the final puzzle is completed, ensure it rises up from the ground
        if (finalPuzzleCompleted && this.transform.position.y < 3.2)
        {
            this.transform.position = new UnityEngine.Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
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
        player.transform.position = new UnityEngine.Vector3(destination.x, destination.y, destination.z);
        runner = FindFirstObjectByType<Yarn.Unity.DialogueRunner>();
        runner.StartDialogue(gameObject.name);
    }
    
    /// <summary>
    /// This method is subscribed to the islandPuzzlesCompleted UnityEvent from
    /// IslandPuzzleManager. Once all puzzles are completed, this method will run
    /// and set finalPuzzleCompleted to true, which will cause the crystal to
    /// rise up from the ground in the FixedUpdate() method.
    /// </summary>
    public void FinalPuzzleCompleted()
    {
        finalPuzzleCompleted = true;
        Debug.Log($"InteractableCrystal.cs >> finalPuzzleCompleted = {finalPuzzleCompleted}. Now rising crystal from ground...");
    }
}
