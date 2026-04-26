using System;
using Sirenix.OdinInspector;
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
    
    private CharacterController charController;

    private GameObject model;
    
    [Space]
    [Title("Debugging Options", "Settings for quick debugging options.")]
    [PropertyTooltip("Print out messages regarding the Player's kinematics. False by default.")]
    public bool printKinematics = false;
    [PropertyTooltip("Print out updates regarding the action map. False by default.")]
    public bool printActionMapUpdates = false;
    [PropertyTooltip("Print out updates regarding the cursor. False by default.")]
    public bool printCursorUpdates = false;

    void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        
        if (charController == null)
        {
            charController = GetComponent<CharacterController>();
        }

        if (model == null)
        {
            model = transform.GetChild(0).gameObject;
        }
        
        _playerControls = new PlayerControls();
    }

    // Subscribe to events
    void OnEnable()
    {
        InputManager.inputMapSwitched += SwitchActionMap;
        InputManager.cursorChanged += SwitchCursorFunctionality;
        GameStateManager.transitionedToNewState += ConfigureOrientation;


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
        GameStateManager.transitionedToNewState -= ConfigureOrientation;
        
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
            if (printActionMapUpdates) Debug.LogError("Player.cs >> Cannot switch action map: PlayerInput is null!");
            return;
        }
        
        _playerInput.SwitchCurrentActionMap(actionMapName);
        if (printActionMapUpdates) Debug.Log($"Player.cs >> Switched action map for {actionMapName} state.");
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
        
        if (printCursorUpdates) Debug.Log($"Player.cs >> Switched cursor functionality to {lockMode} and {visible}.");
    }
    
    /// <summary>
    /// Used to configure the Player's orientation based on the game state.
    /// For instance, the orientation should be different in Puzzle mode to
    /// ensure we can see Skye's sprite properly.
    /// </summary>
    /// <param name="newState"></param>
    private void ConfigureOrientation(GameStateManager.GameState newState)
    {
        // If we're in Puzzle Mode, we want to orient the Player so
        // her 2D sprite is more visible to the camera.
        if (newState != GameStateManager.GameState.Puzzle)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(-Vector3.up);
        }
    }
    
    /// <summary>
    /// Toggles the Player's character controller on and off. This method is public
    /// to allow other scripts access to this functionality.
    /// </summary>
    /// <param name="enabled"></param>
    public void ToggleCharacterController(bool enabled)
    {
        charController.enabled = enabled;
    }
    
    /// <summary>
    /// Diables character controller, teleports the player to the new position, and
    /// then re-enables the character controller.
    /// </summary>
    /// <param name="newPosition"></param>
    public void TeleportPlayer(Vector3 newPosition)
    {
        charController.enabled = false;
        transform.position = newPosition;
        charController.enabled = true;
    }
}