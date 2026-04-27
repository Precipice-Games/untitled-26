using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{

    //   ==== Interaction Variables ====
    bool interactedWith = false;

    //   ==== Object Variables ====
    float wallSize;


    /*
     * 
     * Assigns the vertical size of the object to wall size
     * 
     */

    private void Start()
    {

        wallSize = GetComponent<Collider>().bounds.size.y;

    }

    /*
     * 
     * if the object has been interacted with and the wall hasn't
     * moved down enough to hide the wall. The position of the wall
     * lowers by 10 units times deltaTime (to normalize movement across
     * framerates)
     * 
     */

    private void FixedUpdate()
    {
        
        if (interactedWith && transform.position.y > -wallSize)
        {

            transform.position = new Vector3(transform.position.x,transform.position.y - (10 * Time.deltaTime),transform.position.z);

        }

    }

    public void Interaction()
    {

        interactedWith = true;

    }
}
