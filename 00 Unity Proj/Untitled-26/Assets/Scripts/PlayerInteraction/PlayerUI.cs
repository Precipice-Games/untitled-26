using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    private GameObject gameStateManager;


    private void Awake()
    {
        gameStateManager = GameObject.Find("GameStateManager");

        if (!gameStateManager)
        {
            Debug.LogError("GameStateManager object not found in the scene.");
        }
    }



}
