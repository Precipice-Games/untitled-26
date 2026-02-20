using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{

    /// <summary>
    /// A singleton instance of the GameStateManager. 
    /// This allows other scripts to easily access the game state manager without needing to find it in the scene or pass references around.
    /// </summary>
    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        // Check for an existing instance of the GameStateManager object in the scene.
        // If one already exists, destroy this new instance to enforce the singleton pattern.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // GameState Manager will persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
