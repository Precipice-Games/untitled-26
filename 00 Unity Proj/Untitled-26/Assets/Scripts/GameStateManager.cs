using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
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
    /// A singleton instance of the GameStateManager. 
    /// This allows other scripts to easily access the game state manager without needing to find it in the scene or pass references around.
    /// </summary>
    public static GameStateManager Instance;

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

    private void Start()
    {
        // Set the initial game state when the game starts.
        ChangeToMainMenu();
    }

    // Public methods to change the game state.
    // These can be called by other scripts or events to trigger a state change.
    public void ChangeToMainMenu()
    {
        StartCoroutine(TransitionToState(GameState.MainMenu));
    }
    public void ChangeToExploration()
    {
        StartCoroutine(TransitionToState(GameState.Exploration));
    }

    public void ChangeToPuzzle()
    {
        StartCoroutine(TransitionToState(GameState.Puzzle));
    }

    public void ChangeToPaused()
    {
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
            currentState = newState;
        HandleStateChange();
    }

    /// <summary>
    /// Handles logic for switching between game states.
    /// </summary>
    /// <remarks> 
    /// Switches between UI canvases and sets time scale to 0.0f when in a paused state.
    /// </remarks>
    private void HandleStateChange()
    {
        // deactivate old UI
        if (currentUI)
        {
            currentUI.SetActive(false);
        }

        // TO DO: handle change in camera views
        switch (currentState) {
            case GameState.MainMenu:
                currentUI = mainMenuUI;
                Time.timeScale = 0.0f;
                break;
            case GameState.Exploration:
                currentUI = explorationUI;
                Time.timeScale = 1.0f;
                break;
            case GameState.Puzzle:
                currentUI = puzzleUI;
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                currentUI = pausedUI;
                Time.timeScale = 0.0f;
                break;
        }

        // activate UI of new state
        currentUI.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
