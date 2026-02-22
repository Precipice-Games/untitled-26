using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.InputSystem;

// This is the Player class that will allow us to get specific data about the Player.

public class Player : MonoSingleton<Player>
{
    [SerializeField] private Rigidbody rb; // Rigidbody
    [SerializeField] private PlayerInput _playerInput;
    public static event Action inputMapSwitched;

    void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
    }
    
    // Returns the PlayerInput component attached to the player
    public PlayerInput GetPlayerInput()
    {
        return _playerInput;
    }

    // Subscribe to events
    private void OnEnable()
    {
        if (_playerInput != null)
        {
            inputMapSwitched += CurrentActionMap;
            ViewManager.gameStateChanged += SwitchActionMap;
            ViewManager.gameStateChanged += SwitchCursorFunctionality;
        }
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        if (_playerInput != null)
        {
            inputMapSwitched -= CurrentActionMap;
            ViewManager.gameStateChanged -= SwitchActionMap;
            ViewManager.gameStateChanged -= SwitchCursorFunctionality;
        }
    }
    
    // Switches the current action map to the specified action map name
    private void SwitchActionMap(ViewManager.GameState newState)
    {
        if (_playerInput != null)
        {
            string actionMapName = "UI"; // Default to UI, will be overridden in switch statement
            
            switch (newState) {
                case ViewManager.GameState.MainMenu:
                    actionMapName = "UI";
                    break;
                case ViewManager.GameState.Exploration:
                    actionMapName = "Player";
                    break;
                case ViewManager.GameState.Puzzle:
                    actionMapName = "Player"; // TODO: fix this accordingly
                    break;
                case ViewManager.GameState.Paused:
                    actionMapName = "UI";
                    // Set action map
                    break;
            }
            
            _playerInput.SwitchCurrentActionMap(actionMapName);
            inputMapSwitched?.Invoke();
        }
    }
    
    // Switches cursor functionality based on game state
    private void SwitchCursorFunctionality(ViewManager.GameState newState)
    {
        // If we're not in Exploration mode, the cursor should be visible and unlocked
        if (newState != ViewManager.GameState.Exploration)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        Debug.Log($"Player.cs >> Switched cursor functionality for {newState} state.");
    }
    
    // Prints out the name of the current action map
    private void CurrentActionMap()
    {
        Debug.Log("Player > CurrentActionMap > Current Action Map: " + _playerInput.currentActionMap.name);
    }
}