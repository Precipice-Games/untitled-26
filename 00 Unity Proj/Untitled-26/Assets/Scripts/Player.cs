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
        InputManager.cursorChanged += SwitchCursorFunctionality;
    }

    void OnDisable()
    {
        InputManager.inputMapSwitched -= SwitchActionMap;
        InputManager.cursorChanged -= SwitchCursorFunctionality;
    }
    
    // Switches the current action map to the specified action map name
    private void SwitchActionMap(string actionMapName)
    {
        if (_playerInput == null)
        {
            Debug.LogError("Player.cs >> Cannot switch action map: PlayerInput is null!");
            return;
        }
        
        _playerInput.SwitchCurrentActionMap(actionMapName);
        Debug.Log($"Player.cs >> Switched action map for {actionMapName} state.");
    }
    
    // TODO: The cursor commands are static, so it's not as easy to assign
    //       them as the action map, but this should do for now. Perhaps
    //       we can set it up later on to respond to the current action
    //       map rather than the current game state.
    
    // Switches cursor functionality based on game state
    private void SwitchCursorFunctionality(CursorLockMode lockMode, bool visible)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = visible;
        
        Debug.Log($"Player.cs >> Switched cursor functionality to {lockMode} and {visible}.");
    }
}