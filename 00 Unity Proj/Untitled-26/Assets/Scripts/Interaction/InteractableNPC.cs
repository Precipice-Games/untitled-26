using System;
using UnityEngine;
using Yarn;
using Yarn.Unity;

#if UNITY_EDITOR
using Yarn.Unity.Editor;
#endif

public class InteractableNPC : MonoBehaviour, IInteractable
{

    //   ==== Interaction Variables ====
    bool interactedWith = false;
    DialogueRunner runner;

    // TODO: Does this need to be inside of a FixedUpdate()? Perhaps we can
    //       optimize this? FixedUpdate() is really for physics calculations,
    //       and this is just for starting a dialogue. Maybe we can move this
    //       to a more optimized method, like Callbacks, UnitEvents, etc.?
    private void FixedUpdate()
    {
        if (interactedWith)
        {
            Debug.Log("Dialogue Starting");
            
            // TODO: Is is possible to optimize this? I believe FindAnyObjectByType<>
            //       can quickly prove itself to be costly. If there's just one DialogueRunner
            //       in the scene, then we should ensure it is accessible at runtime.
            runner = FindAnyObjectByType<Yarn.Unity.DialogueRunner>();
            runner.StartDialogue(gameObject.name);
            interactedWith = false;

        }
    }

    public void Interaction()
    {
        interactedWith = true;
    }
}
