using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{

    [Header("Unity Events")]
    public UnityEvent Pause;

    public static InputSystem_Actions Input { get; private set; }

    private void Awake()
    {
        Input = new InputSystem_Actions();
        Input.Menu.Enable();
    }

    // TODO: maybe use the input actions callback like we do with the Player movement
    //       & interaction system so that we're not constantly calling it in update().
    private void Update()
    {
        // Check for menu input actions
        if (Input.Menu.Pause.triggered)
        {
            Pause.Invoke();
        }
    }
}
