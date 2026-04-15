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

    public void Interaction()
    {
        crystalCollected.Invoke();
    }
}
