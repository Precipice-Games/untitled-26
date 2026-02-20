using UnityEngine;
using UnityEngine.Events;

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

    public static InputSystem_Actions Input { get; private set; }

    private void Awake()
    {
        Input = new InputSystem_Actions();
        Input.Menu.Enable();    // TO DO: Figure out how/when to disable input.
    }

    private void Update()
    {
        // Check for menu input actions
        // TO DO: Is there a more efficient way to check for input?
        if (Input.Menu.enabled)
        {
            if (Input.Menu.Pause.triggered)
            {
                Pause.Invoke();
            }
            else if (Input.Menu.Interact.triggered)
            {
                Interact.Invoke();
            }
            else if (Input.Menu.Map.triggered)
            {
                Map.Invoke();
            }
        }
    }
}
