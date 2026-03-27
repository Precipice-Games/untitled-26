using System;
using UnityEngine;

public class ExitPuzzleButton : MonoBehaviour
{
    public GameObject player;

    public static event Action<GameStateManager.GameState> exitPuzzle;
    public InteractablePillar currentPillar;

    public Vector3 respawnLocation;

    public void OnExitPuzzleButtonPressed()
    {
        player.transform.parent = null;
        player.transform.position = respawnLocation;
        player.GetComponent<PlayerRaycastInteraction>().canInteract = true;
        exitPuzzle.Invoke(GameStateManager.GameState.Exploration);
    }
}
