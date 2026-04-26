using UnityEngine;

public class Deathbox : MonoBehaviour
{
    public Transform[] respawnLocations;
    public IslandManager islandManager;

    [SerializeField] private int puzzlesComplete = 0;

    public void OnPuzzleCompletion()
    {
        puzzlesComplete++;
    }

    public void OnCrystalCollect()
    {
        puzzlesComplete = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            Player.Instance.TeleportPlayer(respawnLocations[puzzlesComplete].position);

        }

    }

}
