using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{

    [Header("Unity Events")]
    public UnityEvent Pause;

    // Get a reference to the PlayerInput component
    public static PlayerInput playerInput { get; private set; }
    
    // Invoked whenever the input map is switched. This allows other
    // scripts to subscribe to this event and update their references
    // to the current input map as needed.
    public static event Action<string> inputMapSwitched;
    
    private void Awake()
    {
        // Initialize the PlayerInput component and
        // assign it to the static property
        if (Player.Instance == null)
        {
            Debug.Log("InputManager.cs >> Can't find the Player instance. Make sure it is in the scene with Player.cs attached.");
        }
        else
        {
            playerInput = Player.Instance.GetComponent<PlayerInput>();
        }
    }
    
    // Subscribe to events
    private void OnEnable()
    {
        if (playerInput != null)
        {
            GameStateManager.transitionedToNewState += FindCorrectActionMap;
        }
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        if (playerInput != null)
        {
            GameStateManager.transitionedToNewState -= FindCorrectActionMap;
        }
    }
    
    // Switches the current action map to the specified action map name
    private void FindCorrectActionMap(GameStateManager.GameState newState)
    {
        if (playerInput != null)
        {
            string actionMapName = "UI"; // Default to UI, will be overridden in switch statement
            
            switch (newState) {
                case GameStateManager.GameState.MainMenu:
                    actionMapName = "UI";
                    break;
                case GameStateManager.GameState.Exploration:
                    actionMapName = "Player";
                    break;
                case GameStateManager.GameState.Puzzle:
                    actionMapName = "Player";
                    // TODO: We should probably create a new action map
                    //       specifically for puzzle solving, even if it
                    //       shares similar inputs to the Player action map.
                    break;
                case GameStateManager.GameState.Paused:
                    actionMapName = "UI";
                    break;
            }
            
            inputMapSwitched?.Invoke(actionMapName);
        }
    }
    
    public void PauseTriggered(InputAction.CallbackContext context)
    {
        Pause.Invoke();
    }
}
