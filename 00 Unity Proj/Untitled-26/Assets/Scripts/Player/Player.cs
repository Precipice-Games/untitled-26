using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// This is the Player class that will allow us to get specific data about the Player.

public class Player : MonoSingleton<Player>
{
    /// <summary>
    /// Triggered by pressing 'ESC' to pause/unpause the game.
    /// Listeners should check to make sure the game is in a pausable state.
    /// </summary>
    [Header("Unity Events")]
    public UnityEvent Pause;
    
    /// <summary>
    /// Triggered by pressing 'M' to open the map UI.
    /// Listeners should check the game is in exploration state.
    /// </summary>
    public UnityEvent Map;
    
    public PlayerControls _playerControls { get; private set; }
    
    [SerializeField] private PlayerInput _playerInput;

    void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        
        _playerControls = new PlayerControls();
        
    }

    void OnEnable()
    {
        InputManager.inputMapSwitched += SwitchActionMap;
        InputManager.cursorChanged += SwitchCursorFunctionality;
        
        _playerControls.UI.Enable();
        _playerControls.Player.Enable();
        _playerControls.UI.Pause.performed += OnPause;
        _playerControls.UI.Map.performed += OnMap;
    }

    void OnDisable()
    {
        InputManager.inputMapSwitched -= SwitchActionMap;
        InputManager.cursorChanged -= SwitchCursorFunctionality;
        
        _playerControls.UI.Disable();
        _playerControls.Player.Disable();
        _playerControls.UI.Pause.performed -= OnPause;
        _playerControls.UI.Map.performed -= OnMap;
    }
    
    private void OnDestroy()
    {
        _playerControls.Dispose();
    }
    
    private void OnPause(InputAction.CallbackContext context)
    {
        Pause.Invoke();
    }
    
    private void OnMap(InputAction.CallbackContext context)
    {
        Map.Invoke();
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