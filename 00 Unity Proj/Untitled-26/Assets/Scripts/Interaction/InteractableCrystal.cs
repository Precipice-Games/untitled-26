using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InteractableCrystal : MonoBehaviour, IInteractable
{

    // TODO: Update with proper scene options. Also need to link
    //       this together with the current island completion
    //       system. However, I tested it on Ice Island and can
    //       confirm it sent me to Mother Island. -- Nikki
    
    [Space]
    [Title("CrystalCollected", "This event is fired when the crystal has been collected.")]
    public UnityEvent crystalCollected;

    [SerializeField]
    PuzzleInformation finalPuzzle;

    private void FixedUpdate()
    {
        // Ensure the puzzle rises up from the ground
        if (finalPuzzle != null && finalPuzzle.puzzleSolved == true && this.transform.position.y < 3.2)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        }
    }

    public void Interaction()
    {

        // // Ensure the puzzle rises up from the ground
        // if (finalPuzzle != null && finalPuzzle.puzzleSolved == true && this.transform.position.y < 3.2)
        // {
        //     this.transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        // }
        
        crystalCollected.Invoke();
        Destroy(gameObject);
    }

    // Commenting this out for now. This was the original Interaction() method I was
    // working with for the progression system, but I will try to integrate my work
    // with what Dan was working on in the version above. -- Nikki
    // public void Interaction()
    // {
    //     crystalCollected.Invoke();
    // }
}
