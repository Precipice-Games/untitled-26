using System;
using System.Collections;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Inherits from GameStateManager. 
/// ViewManager is responsible for managing the game's UI and camera views based on game state. 
/// This script includes the methods for changing the UI for each game state.
/// TO DO: add logic for changing camera views between exploration and puzzle states.
/// </summary>
public class ViewManager : MonoBehaviour
{

    [Header("GameUIs")]
    public GameObject mainMenuUI;
    public GameObject explorationUI;
    public GameObject puzzleUI;
    public GameObject pausedUI;
    private GameObject currentUI;

    [Header("Cameras")]
    public Camera playerCamera;
    public Camera puzzleCamera;
    
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
    public static GameState CurrentGameState { get; private set; }

    /// <summary>
    /// The most recent previous state the game was in before the current state.
    /// </summary>
    private GameState prevState;

    /// <summary>
    /// Tracks if the player is able to pause from the current state
    /// </summary>
    bool pausable = false;
    
    // Static event to notify subscribers of game state changes
    public static event Action<GameState> gameStateChanged;

    private void Start()
    {
        // Set the initial game state when the game starts.
        // ChangeToMainMenu();
        
        Debug.Log("ViewManager.cs >> CurrentGameState: " + CurrentGameState);
        
        // TODO: There should probably be some logic here regarding what the default state should be when we
        //       load up the game. Right now, it's set to MainMenu. But perhaps we can have a variable in each
        //       scene that corresponds to the GameState enum above to check what the default state SHOULD be
        //       for that given scene. If the default is EMPTY, then we can default to MainMenu.
    }

    // Public methods to change the game state.
    // These can be called by other scripts or events to trigger a state change.
    public void ChangeToMainMenu()
    {
        Debug.Log("ViewManager.cs >> Changing to " + CurrentGameState + " state...");
        StartCoroutine(TransitionToState(GameState.MainMenu));
    }
    public void ChangeToExploration()
    {
        Debug.Log("ViewManager.cs >> Changing to " + CurrentGameState + " state...");
        StartCoroutine(TransitionToState(GameState.Exploration));
    }

    public void ChangeToPuzzle()
    {
        Debug.Log("ViewManager.cs >> Changing to " + CurrentGameState + " state...");
        StartCoroutine(TransitionToState(GameState.Puzzle));
    }

    public void ChangeToPaused()
    {
        Debug.Log("ViewManager.cs >> Changing to " + CurrentGameState + " state...");
        StartCoroutine(TransitionToState(GameState.Paused));
    }

    /// <summary>
    /// A helper method to transition the game to a new state. 
    /// It includes a small delay to simulate the time it may take to transition between states.
    /// </summary>
    /// <param name="newState"> The <see cref="GameState"/> the game will transition to. </param>
    private IEnumerator TransitionToState(GameState newState)
    {
        if (newState != GameState.MainMenu)
        {
            yield return new WaitForSecondsRealtime(0.1f); // Simulate a delay for transitioning states (e.g., for animations or loading screens)
        }
        prevState = CurrentGameState;
        CurrentGameState = newState;
        Debug.Log("ViewManager.cs >> State transitioned to: " + CurrentGameState); // Confirm the state change
        HandleStateChange();
    }
    
    private void OnEnable()
    {
        // gameStateChanged += OnGameStateChanged;
        SceneManager.sceneLoaded += SceneDefaults;
    }
    
    private void OnDisable()
    {
        // gameStateChanged -= OnGameStateChanged;
        SceneManager.sceneLoaded -= SceneDefaults;
    }
    
    // By default the scene will load in the selected game state
    public void SceneDefaults(Scene scene, LoadSceneMode mode)
    {
        // Initialize the static CurrentGameState from the inspector value
        SetGameState(gameState);
    }
    
    /// <summary>
    /// Automatically changes the game state.
    /// </summary>
    /// <remarks> 
    /// Temporary method meant to automatically set the game state, even without a transition.
    /// It also notifies all other subscribers when the state has been changed.
    /// This method can be removed later on if needed.
    /// </remarks>
    public static void SetGameState(GameState newState)
    {
        Debug.unityLogger.Log("GameStateManager > SetGameState " + newState.ToString());
        
        // Update the static current state
        CurrentGameState = newState;

        // Notify all subscribers (including component instances)
        gameStateChanged?.Invoke(newState);
    }

    /// <summary>
    /// Handles logic for switching between game states.
    /// </summary>
    /// <remarks> 
    /// Switches between UI canvases and sets timescale to 0.0f when in a paused state.
    /// </remarks>
    private void HandleStateChange()
    {
        // Deactivate old UI
        if (currentUI)
        {
            currentUI.SetActive(false);
            Debug.Log("ViewManager.cs >> Disabled the following UI: " + currentUI); // Confirm disabling of UI
        }

        // TODO: handle change in camera views
        switch (CurrentGameState) {
            case GameState.MainMenu:
                currentUI = mainMenuUI;
                pausable = false;
                Time.timeScale = 0.0f;
                break;
            case GameState.Exploration:
                currentUI = explorationUI;
                pausable = true;
                Time.timeScale = 1.0f;
                break;
            case GameState.Puzzle:
                currentUI = puzzleUI;
                pausable = true;
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                currentUI = pausedUI;
                pausable = true;    // pausable also accounts for if the game can be unpaused, which is only true for the 'Paused' state
                Time.timeScale = 0.0f;
                break;
        }

        // activate UI of new state
        currentUI.SetActive(true);
        Debug.Log("ViewManager.cs >> Activated UI for: " + CurrentGameState);
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
                    ChangeToExploration();
                    break;
                case GameState.Puzzle:
                    ChangeToPuzzle();
                    break;
            }
        }
        else if (pausable)
        {
            ChangeToPaused();
        }
        else
        {
            Debug.Log("Cannot pause from current state");
            return;
        }
    }
}
