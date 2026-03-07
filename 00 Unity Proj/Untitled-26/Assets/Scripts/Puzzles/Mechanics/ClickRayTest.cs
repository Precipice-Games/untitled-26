using UnityEngine;
using UnityEngine.InputSystem; 

public class ClickRayTest : MonoBehaviour
{
    // TODO: optimize this script so it's not constantly running the logic in update()
    
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
}
