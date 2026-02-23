using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.InputSystem;

// This is the Player class that will allow us to get specific data about the Player.

public class Player : MonoSingleton<Player>
{
    [SerializeField] private PlayerInput _playerInput;

    void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
    }

    void OnEnable()
    {
        InputManager.inputMapSwitched += SwitchActionMap;
        GameStateManager.transitionedToNewState += SwitchCursorFunctionality;
    }

    void OnDisable()
    {
        InputManager.inputMapSwitched -= SwitchActionMap;
        GameStateManager.transitionedToNewState -= SwitchCursorFunctionality;
    }
    
    // Switches the current action map to the specified action map name
    private void SwitchActionMap(string actionMapName)
    {
        _playerInput.SwitchCurrentActionMap(actionMapName);
        Debug.Log($"Player.cs >> Switched action map for {actionMapName} state.");
    }
    
    // TODO: The cursor commands are static, so it's not as easy to assign
    //       them as the action map, but this should do for now. Perhaps
    //       we can set it up later on to respond to the current action
    //       map rather than the current game state.
    
    // Switches cursor functionality based on game state
    private void SwitchCursorFunctionality(GameStateManager.GameState newState)
    {
        // Cursor should only be locked and invisible during Exploration mode.
        if (newState != GameStateManager.GameState.Exploration)
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
}