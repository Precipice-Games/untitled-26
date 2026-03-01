using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    // Get a reference to the PlayerInput component
    public static PlayerInput playerInput { get; private set; }
    // [SerializeField] private PlayerInput playerInput;
    
    // Invoked whenever the input map is switched. This allows other
    // scripts to subscribe to this event and update their references
    // to the current input map as needed.
    public static event Action<string> inputMapSwitched;
    public static event Action<CursorLockMode, bool> cursorChanged;
    
    private void Awake()
    {
        // Initialize the PlayerInput component and
        // assign it to the static property
        AcquirePlayerInputReference();
    }
    
    // Subscribe to events
    private void OnEnable()
    {
        // Re-acquire the PlayerInput reference in case scene changed
        AcquirePlayerInputReference();
        
        // Always subscribe to the event, we'll check for null in FindCorrectActionMap
        GameStateManager.transitionedToNewState += FindCorrectActionMap;
        GameStateManager.transitionedToNewState += ChangeCursorFunctionality;
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        GameStateManager.transitionedToNewState -= FindCorrectActionMap;
        GameStateManager.transitionedToNewState -= ChangeCursorFunctionality;
    }
    
    /// <summary>
    /// Gets the PlayerInput reference from the Player singleton instance.
    /// </summary>
    private void AcquirePlayerInputReference()
    {
        if (Player.Instance == null)
        {
            Debug.LogWarning("InputManager.cs >> Can't find the Player instance. Make sure it is in the scene with Player.cs attached.");
            playerInput = null;
        }
        else
        {
            playerInput = Player.Instance.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                Debug.Log("InputManager.cs >> Successfully acquired PlayerInput reference.");
            }
            else
            {
                Debug.LogWarning("InputManager.cs >> Player instance found but PlayerInput component is missing!");
            }
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
                case GameStateManager.GameState.Dialogue:
                    actionMapName = "UI";
                    break;
                case GameStateManager.GameState.Paused:
                    actionMapName = "UI";
                    break;
                case GameStateManager.GameState.Settings:
                    actionMapName = "UI";
                    break;
            }
            
            inputMapSwitched?.Invoke(actionMapName);
        }
    }
    
    // Switches the current action map to the specified action map name
    private void ChangeCursorFunctionality(GameStateManager.GameState newState)
    {
        if (playerInput != null)
        {
            CursorLockMode lockMode = CursorLockMode.None; // Default to unlocked
            bool visible = true; // Default to visible

            // Cursor should only be locked and invisible during Exploration mode.
            if (newState != GameStateManager.GameState.Exploration)
            {
                lockMode = CursorLockMode.None;
                visible = true;
            }
            else
            {
                lockMode = CursorLockMode.Locked;
                visible = false;
            }
            
            cursorChanged?.Invoke(lockMode, visible);
        }
    }
}
