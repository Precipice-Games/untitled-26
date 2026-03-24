using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
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

    public Rigidbody rb;
    
    public PlayerControls _playerControls { get; private set; }
    
    [SerializeField] private PlayerInput _playerInput;
    
    [Title("Debug Mode")]
    [InfoBox("Check this variable if you want messages to be debugged from this script. If not, uncheck it.")]
    [PropertyTooltip("Enables or disables debug logs in a given script.")]
    public bool debugMode = true;

    void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        
        rb = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
        
    }

    // Subscribe to events
    void OnEnable()
    {
        InputManager.inputMapSwitched += SwitchActionMap;
        InputManager.cursorChanged += SwitchCursorFunctionality;
        PhysicsManager.kinematicsUpdated += SwitchKinematics;


        _playerControls.UI.Enable();
        _playerControls.Player.Enable();
        _playerControls.UI.Pause.performed += OnPause;
        _playerControls.UI.Map.performed += OnMap;
    }

    // Unsubscribe from events
    void OnDisable()
    {
        InputManager.inputMapSwitched -= SwitchActionMap;
        InputManager.cursorChanged -= SwitchCursorFunctionality;
        PhysicsManager.kinematicsUpdated -= SwitchKinematics;
        
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
            if (debugMode) Debug.LogError("Player.cs >> Cannot switch action map: PlayerInput is null!");
            return;
        }
        
        
        _playerInput.SwitchCurrentActionMap(actionMapName);
        if (debugMode) Debug.Log($"Player.cs >> Switched action map for {actionMapName} state.");
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
        
        if (debugMode) Debug.Log($"Player.cs >> Switched cursor functionality to {lockMode} and {visible}.");
    }
    
    /// <summary>
    /// Sets the player's Rigidbody to kinematic or non-kinematic based on the current game state.
    /// </summary>
    /// <param name="isKinematic"></param>
    private void SwitchKinematics(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
        Debug.Log("Player.cs >> Kinematics were set to " + isKinematic);
    }
}