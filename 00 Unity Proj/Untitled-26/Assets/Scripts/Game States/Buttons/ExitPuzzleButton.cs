using System;
using UnityEngine;

public class ExitPuzzleButton : MonoBehaviour
{
    public GameObject player;

    public static event Action<GameStateManager.GameState> exitPuzzle;
    public RuneCircle currentRuneCircle;

    public Vector3 respawnLocation;

    public void OnExitPuzzleButtonPressed()
    {
        player.transform.parent = null;
        player.transform.position = respawnLocation;
        player.GetComponent<PlayerRaycastInteraction>().canInteract = true;

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.EndPuzzleModeMusic();
        }
        
        exitPuzzle.Invoke(GameStateManager.GameState.Exploration);
    }
}
