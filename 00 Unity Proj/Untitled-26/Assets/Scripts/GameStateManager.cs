using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Set the initial game state when the game starts.
        // For now, we'll start in the Exploration state, but this could be changed in the future.
        ChangeToExploration();
    }

    // Public methods to change the game state.
    // These can be called by other scripts or events to trigger a state change.
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
        yield return new WaitForSecondsRealtime(0.1f); // Simulate a delay for transitioning states (e.g., for animations or loading screens)
        currentState = newState;
        HandleStateChange();
    }


    private void HandleStateChange()
    {
        switch (currentState) {
            case GameState.Exploration:
                // Handle logic for entering Exploration state
                break;
            case GameState.Puzzle:
                // Handle logic for entering Puzzle state
                break;
            case GameState.Paused:
                // Handle logic for entering Paused state
                break;
        }

    }
}
