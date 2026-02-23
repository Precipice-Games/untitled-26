using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
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

    public InputSystem_Actions Input { get; private set; }

    private void Awake()
    {
        Input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        Input.Menu.Enable();
        Input.Menu.Pause.performed += OnPause;
        Input.Menu.Interact.performed += OnInteract;
        Input.Menu.Map.performed += OnMap;
    }

    private void OnDisable()
    {
        Input.Menu.Pause.performed -= OnPause;
        Input.Menu.Interact.performed -= OnInteract;
        Input.Menu.Map.performed -= OnMap;
        Input.Menu.Disable();
    }

    private void OnDestroy()
    {
        Input.Dispose();
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
}
