using System;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using Yarn.Unity.Variables;

#if UNITY_EDITOR
using Yarn.Unity.Editor;
#endif

public class CompletionButton : MonoBehaviour, IInteractable
{

    bool interactedWith = false;
    public InMemoryVariableStorage variableStorage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (variableStorage == null)
        {
            variableStorage = FindObjectOfType<InMemoryVariableStorage>();
        }

        if (variableStorage == null)
        {
            Debug.LogError("InMemoryVariableStorage not found in the scene!");
        }
    }


    private void FixedUpdate()
    {
         if (interactedWith)
        {
            Debug.Log("Completed");
            
            variableStorage.SetValue("$iceFinished", true);
            interactedWith = false;

        }
    }

    public void Interaction()
    {

        interactedWith = true;

    }
}
