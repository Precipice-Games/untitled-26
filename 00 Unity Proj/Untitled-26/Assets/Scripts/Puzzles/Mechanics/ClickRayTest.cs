using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem; 

public class ClickRayTest : MonoBehaviour
{
    [Title("Debug Mode")]
    [InfoBox("Check this variable if you want messages to be debugged from this script. If not, uncheck it.")]
    [PropertyTooltip("Enables or disables debug logs in a given script.")]
    public bool debugMode = true;
    
    // TODO: optimize this script so it's not constantly running the logic in update()
    
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (debugMode) Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
}
