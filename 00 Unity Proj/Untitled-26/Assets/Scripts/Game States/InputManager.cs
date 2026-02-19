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

    private void Update()
    {
        // Check for menu input actions
        if (Input.Menu.Pause.triggered)
        {
            Pause.Invoke();
        }
    }
}
