using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityCommunity.UnitySingleton;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    
    [Title("Game State")]
    [EnumToggleButtons, HideLabel]
    [InfoBox("Choose the default game state that this scene will load in.")]
    public GameState gameState;
    // Note: This variable is not static because each instance of the component
    // needs to maintain its own copy of the current game state for reference. Also,
    // the Enum will clearly change when the static event is invoked, so watch out
    // for the Inspector.

    /// <summary>
    /// The set of possible game states.
    /// </summary>
    /// <remarks>
    /// As of right now, the game can be in one of three states: Exploration, Puzzle, or Paused.
    /// </remarks>
    public enum GameState
    {
        MainMenu,
        Exploration,
        Puzzle,
        Paused
    }

    /// <summary>
    /// The state the game is currently in.
    /// </summary>
    public static GameState CurrentGameState { get; set; }

    /// <summary>
    /// The most recent previous state the game was in before the current state.
    /// </summary>
    private GameState prevState;

    /// <summary>
    /// Tracks if the player is able to pause from the current state
    /// </summary>
    bool pausable;
    
    // Static event to notify subscribers of game state changes
    public static event Action<GameState> transitionedToNewState;
    
    /// <summary>
    /// A singleton instance of the GameStateManager.
    /// This allows other scripts to easily access the game state manager without needing to find it in the scene or pass references around.
    /// </summary>
    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        // Commented this out because I think that the GameStateManager
        // might cause issues if it persists across scenes. Instead, I have
        // wrapped this class in a MonoSingleton<>, which is a singleton that
        // is destroyed between scenes. However, if you decide you want it
        // to NOT be destroyed, wrap the class in a PersistentMonoSingleton<>.
        //
        // // Check for an existing instance of the GameStateManager object in the scene.
        // // If one already exists, destroy this new instance to enforce the singleton pattern.
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject); // GameState Manager will persist across scenes
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }
    
    private void Start()
    {
        Debug.Log("GameStateManager.cs >> CurrentGameState: " + CurrentGameState);
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneDefaults;
        transitionedToNewState += HandlePauseValues;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneDefaults;
        transitionedToNewState -= HandlePauseValues;
    }
    
    // By default, the scene will load in the selected game state
    public void SceneDefaults(Scene scene, LoadSceneMode mode)
    {
        // Initialize the static CurrentGameState from the inspector value
        // SetGameState(gameState);
        
        // Set time scale immediately based on the starting game state to prevent frozen scenes
        // if (gameState == GameState.Exploration || gameState == GameState.Puzzle)
        // {
        //     Time.timeScale = 1.0f;
        //     Debug.Log($"GameStateManager.cs >> Immediately set time scale to 1.0 for {gameState} state.");
        // }
        // else if (gameState == GameState.MainMenu || gameState == GameState.Paused)
        // {
        //     Time.timeScale = 0.0f;
        //     Debug.Log($"GameStateManager.cs >> Immediately set time scale to 0.0 for {gameState} state.");
        // }
        
        Debug.Log($"GameStateManager.cs >> Starting Coroutine TransitionToState({gameState})...");
        TransitionToState(gameState);
    }
    
    /// <summary>
    /// A helper method to transition the game to a new state. 
    /// It includes a small delay to simulate the time it may take to transition between states.
    /// </summary>
    /// <param name="newState"> The <see cref="GameState"/> the game will transition to. </param>
    // private IEnumerator TransitionToState(GameState newState)
    // {
    //     //yield return new WaitForSecondsRealtime(0.1f); // Simulate a delay for transitioning states (e.g., for animations or loading screens)
    //     // if (newState != GameState.MainMenu)
    //     // {
    //     //     yield return new WaitForSecondsRealtime(0.1f); // Simulate a delay for transitioning states (e.g., for animations or loading screens)
    //     // }
    //     
    //     prevState = CurrentGameState;
    //     CurrentGameState = newState;
    //     Debug.Log("ViewManager.cs >> State transitioned to: " + CurrentGameState); // Confirm the state change
    //     transitionedToNewState?.Invoke(CurrentGameState);
    // }

    private void TransitionToState(GameState newState)
    {
        prevState = CurrentGameState;
        CurrentGameState = newState;
        Debug.Log("ViewManager.cs >> State transitioned to: " + CurrentGameState); // Confirm the state change
        transitionedToNewState?.Invoke(CurrentGameState);
    }

    /// <summary>
    /// Handles the logic for timescale & pausable bool.
    /// </summary>
    /// <remarks> 
    /// Once we've transitioned to the new game state, this method will update the time scale and the pausable variable accordingly.
    /// </remarks>
    private void HandlePauseValues(GameState newState)
    {
        // Update the timescale and pausable variable based on the new game state
        switch (newState)
        {
            case GameState.MainMenu:
                pausable = false;
                Time.timeScale = 0.0f;
                Debug.Log("GameStateManager.cs >> Main Menu loaded, time scale set to 0 and pausable set to false.");
                break;
            case GameState.Exploration:
                pausable = true;
                Time.timeScale = 1.0f;
                Debug.Log("GameStateManager.cs >> Exploration loaded, time scale set to 1 and pausable set to true.");
                break;
            case GameState.Puzzle:
                pausable = true;
                Time.timeScale = 1.0f;
                Debug.Log("GameStateManager.cs >> Exploration loaded, time scale set to 1 and pausable set to true.");
                break;
            case GameState.Paused:
                pausable = true;
                // pausable also accounts for if the game can be unpaused,
                // which is only true for the 'Paused' state
                Time.timeScale = 0.0f;
                Debug.Log("GameStateManager.cs >> Paused loaded, time scale set to 1 and pausable set to true.");
                break;
        }
    }

    /// <summary>
    /// This function listens for the Pause event (e.g., player pressing 'ESC') and checks current state of the game to pause/resume.
    /// When switching from paused to gameplay, change currentState to the previous state (before the game was paused).
    /// </summary>
    public void onPause()
    {
        if (CurrentGameState == GameState.Paused)
        {
            switch (prevState) {
                case GameState.Exploration:
                    TransitionToState(GameState.Exploration);
                    break;
                case GameState.Puzzle:
                    TransitionToState(GameState.Puzzle);
                    break;
            }
        }
        else if (pausable)
        {
            TransitionToState(GameState.Paused);
        }
        else
        {
            Debug.Log("Cannot pause from current state");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
