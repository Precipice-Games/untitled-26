using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerRaycastInteraction : MonoBehaviour
{

    //     ==== Player Raycast ====
    Ray interactionRay; //ray to cast
    RaycastHit raycastHit; //information about what is being hit
    bool isHitting;
    public float rayLength = 2.5f;

    //    ==== Interactable Object ====
    GameObject activeInteractable; //Stores overlapping interactable object

    /*
     * 
     * Emits interactionRay at the origin of the player rayLength
     * distance. Then it stores if the ray hits anything in isHitting
     * If it is then it draw the ray in green to the player and checks
     * if the overlapping object uses the IInteractable interface. If
     * so it assigns the object to activeInteractable. If the ray isn't
     * hitting anything it draws the ray as red and nulls the activeInteractable
     * 
     */

    void FixedUpdate()
    {
        
        interactionRay.origin = transform.position;
        interactionRay.direction = transform.forward;

        Vector3 origin = interactionRay.origin;
        Vector3 direction = interactionRay.direction;

        isHitting = Physics.Raycast(interactionRay, out raycastHit, rayLength);

        if (isHitting)
        {

            Debug.DrawRay(origin, direction * rayLength, Color.green);
            Debug.Log(raycastHit.collider.gameObject.name);

            if (raycastHit.collider.GetComponent<IInteractable>() != null)
            {

                activeInteractable = raycastHit.collider.gameObject;

            }

        }
        else
        {

            Debug.DrawRay(origin, direction * rayLength, Color.red);
            activeInteractable = null;

        }

    }

    /*
     * 
     * If the interact key is pressed the interact function triggers
     * the Interaction() function of the interactable object
     * 
     */

    public void Interact()
    {

        if (activeInteractable != null)
        {

            activeInteractable.GetComponent<IInteractable>().Interaction();

        }

    }
}
