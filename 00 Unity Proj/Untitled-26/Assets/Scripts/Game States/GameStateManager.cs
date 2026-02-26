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
        Paused,
        Settings
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
    /// Tracks if the player is able to pause from the current state.
    /// </summary>
    /// <remarks> 
    /// This variable also accounts for if the game can be unpaused, which is only true for the 'Paused' game state.
    /// </remarks>
    //[SerializeField] private bool pausable;
    
    public static bool pausable { get; set; }
    
    /// <summary>
    /// Event Action used to notify subscribers of game state changes.
    /// </summary>
    /// <remarks> 
    /// This should be triggered after the new state has been assigned. The event will pass the new game state as a parameter to any subscribed methods.
    /// </remarks>
    
    // Static event to notify subscribers of game state changes
    public static event Action<GameState> transitionedToNewState;

    private new void Awake()
    {
        
    }
    
    private void Start()
    {
        Debug.Log("GameStateManager.cs >> CurrentGameState: " + CurrentGameState);
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }
    
    // Runs when a scene is loaded
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("GameStateManager.cs >> OnSceneLoaded() >> New scene has been loaded: " + scene.name);
        Debug.Log($"GameStateManager.cs >> Setting the Scene Defaults for defaultState {gameState}...");
        // Initialize the static CurrentGameState from the inspector value
        SetSceneDefaults(gameState);
    }
    
    // Runs when the active scene is changed
    public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        Debug.Log($"GameStateManager.cs >> OnActiveSceneChanged() >> Switched from {prevScene.name} to {nextScene.name}");
        Debug.Log($"GameStateManager.cs >> Setting the Scene Defaults for defaultState {gameState}...");
        // Initialize the static CurrentGameState from the inspector value
        SetSceneDefaults(gameState);
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

    // Method just used to set the scene defaults.
    // All other state changes occur in TransitionToState().
    private void SetSceneDefaults(GameState defaultState)
    {
        // There is no previous state, so just ensure
        // the CurrentGameState is assigned.
        CurrentGameState = defaultState;
        Debug.Log("GameStateManager.cs >> Set the CurrentGameState to the defaultState " + CurrentGameState);
        Debug.Log("GameStateManger.cs >> Calling on HandlePauseValues()...");
        HandlePauseValues(CurrentGameState);
        //transitionedToNewState?.Invoke(CurrentGameState);
    }

    /// <summary>
    /// Method used to assign game states and update the previous state.
    /// </summary>
    /// <param name="newState"></param>
    private void TransitionToState(GameState newState)
    {
        prevState = CurrentGameState;
        CurrentGameState = newState;
        Debug.Log("ViewManager.cs >> State transitioned to: " + CurrentGameState); // Confirm the state change
        HandlePauseValues(CurrentGameState);
    }

    /// <summary>
    /// Handles the logic for timescale and pausable bool.
    /// </summary>
    /// <remarks> 
    /// Once we've transitioned to the new game state, this method will update the timescale and the pausable variable accordingly.
    /// </remarks>
    private void HandlePauseValues(GameState newState)
    {
        // Update the timescale and pausable variable based on the new game state
        switch (newState)
        {
            case GameState.MainMenu:
                pausable = false;
                Time.timeScale = 0.0f;
                break;
            case GameState.Exploration:
                pausable = true;
                Time.timeScale = 1.0f;
                break;
            case GameState.Puzzle:
                pausable = true;
                Time.timeScale = 1.0f;
                break;
            case GameState.Dialogue:
                pausable = true;
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                pausable = true;
                Time.timeScale = 0.0f;
                break;
            case GameState.Settings:
                pausable = false;
                Time.timeScale = 0.0f;
                break;
        }
        
        Debug.Log($"GameStateManager.cs >> {newState} loaded, time scale set to {Time.timeScale.ToString()} and pausable set to {pausable}.");
        
        // Trigger this event after game state, timescale,
        // and pausable bool have been updated. 
        transitionedToNewState?.Invoke(CurrentGameState);
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
                case GameState.Dialogue:
                    TransitionToState(GameState.Dialogue);
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
