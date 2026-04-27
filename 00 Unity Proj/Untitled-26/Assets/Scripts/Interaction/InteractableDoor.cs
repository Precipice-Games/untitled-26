using UnityEngine;

public class InteractableDoor : MonoBehaviour
{

    public Vector3 teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.Instance.TeleportPlayer(teleportLocation);
        }
    }

}
