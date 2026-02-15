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

    private void Start()
    {

    }

    /*
     * 
     * 
     * 
     */

    private void FixedUpdate()
    {
        
        if (interactedWith)
        {
            Debug.Log("Dialogue Starting");
            
            runner = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
            runner.StartDialogue(gameObject.name);
            //change to dialogue state
                //disables e in this state
            interactedWith = false;

        }

    }

    public void Interaction()
    {

        interactedWith = true;

    }
}
