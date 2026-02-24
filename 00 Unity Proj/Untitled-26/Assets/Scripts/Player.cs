using System;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// This is the Player class that will allow us to get specific data about the Player.

public class Player : MonoSingleton<Player>
{
    [Header("Unity Events")]
    /// <summary>
    /// Triggered by pressing 'ESC' to pause/unpause the game.
    /// Listeners should check to make sure the game is in a pausable state.
    /// </summary>
    public UnityEvent Pause;
    /// <summary>
    /// Triggered by pressing 'E' to interact with an in-game object.
    /// Intended for the use of transititoning from exploration to puzzle mode via puzzle terminal.
    /// </summary>
    // 'Interact' event may be moved to a script attached to the player to better enable/disable input
    // when player is able to interact with an object in the environemnt
    public UnityEvent Interact;
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
        
        _playerControls.Menu.Enable();
        _playerControls.Menu.Pause.performed += OnPause;
        _playerControls.Menu.Interact.performed += OnInteract;
        _playerControls.Menu.Map.performed += OnMap;
    }

    void OnDisable()
    {
        InputManager.inputMapSwitched -= SwitchActionMap;
        InputManager.cursorChanged -= SwitchCursorFunctionality;
        
        _playerControls.Menu.Pause.performed -= OnPause;
        _playerControls.Menu.Interact.performed -= OnInteract;
        _playerControls.Menu.Map.performed -= OnMap;
        _playerControls.Menu.Disable();
    }
    
    private void OnDestroy()
    {
        _playerControls.Dispose();
    }
    
    private void OnPause(InputAction.CallbackContext context)
    {
        Pause.Invoke();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Interact.Invoke();
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