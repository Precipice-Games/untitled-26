using System.Collections;
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
    public GameState currentState { get; private set; }

    /// <summary>
    /// The most recent previous state the game was in before the current state.
    /// </summary>
    private GameState prevState;

    /// <summary>
    /// Tracks if the player is able to pause from the current state
    /// </summary>
    bool pausable = false;

    private void Start()
    {
        // Set the initial game state when the game starts.
        ChangeToMainMenu();
    }

    // Public methods to change the game state.
    // These can be called by other scripts or events to trigger a state change.
    public void ChangeToMainMenu()
    {
        Debug.Log("ViewManager.cs >> Changing to " + currentState + " state...");
        StartCoroutine(TransitionToState(GameState.MainMenu));
    }
    public void ChangeToExploration()
    {
        Debug.Log("ViewManager.cs >> Changing to " + currentState + " state...");
        StartCoroutine(TransitionToState(GameState.Exploration));
    }

    public void ChangeToPuzzle()
    {
        Debug.Log("ViewManager.cs >> Changing to " + currentState + " state...");
        StartCoroutine(TransitionToState(GameState.Puzzle));
    }

    public void ChangeToPaused()
    {
        Debug.Log("ViewManager.cs >> Changing to " + currentState + " state...");
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
        prevState = currentState;
        currentState = newState;
        Debug.Log("ViewManager.cs >> State transitioned to: " + currentState); // Confirm the state change
        HandleStateChange();
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
        switch (currentState) {
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
        Debug.Log("ViewManager.cs >> Activated UI for: " + currentState);
    }

    /// <summary>
    /// This function listens for the Pause event (e.g., player pressing 'ESC') and checks current state of the game to pause/resume.
    /// When switching from paused to gameplay, change currentState to the previous state (before the game was paused).
    /// </summary>
    public void onPause()
    {
        if (currentState == GameState.Paused)
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
