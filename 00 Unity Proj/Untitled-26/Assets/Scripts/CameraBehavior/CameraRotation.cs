using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script allows the camera to revolve around the Player. The Player
/// will always face the camera itself. Additionally, DO NOT add any controls
/// or cursor handling here; that is all handled in seperate files and events,
/// such as game state switches.
/// </summary>

public class CameraRotation : MonoBehaviour
{
    // Variables
    public GameObject player;
    public float mouseSensitivity = 4f;
    private bool isRotating = true;
    
    [SerializeField] private float lookX;
    [SerializeField] private float lookY;
    [SerializeField] private float sqrMag;
    
    private float _camYaw;
    private float _camPitch;
    
    private const float _threshold = 0.01f;
    
    private void Start()
    {
        _camYaw = player.transform.rotation.eulerAngles.y;
    }
    
    public void LookRotation(InputAction.CallbackContext context)
    {
        // Get the look input
        lookX = context.ReadValue<Vector2>().x;
        lookY = context.ReadValue<Vector2>().y;
        sqrMag = context.ReadValue<Vector2>().sqrMagnitude;
        
        // if there is an input and camera position is not fixed
        if (sqrMag >= _threshold)
        {
            // Get the yaw and pitch values
            // _camYaw += lookX * mouseSensitivity;
            // _camPitch += lookY * mouseSensitivity;
            
            _camYaw += lookX;
            _camPitch += lookY;
        }
        
        _camYaw -= lookY; // Invert the y-axis
        _camYaw = Mathf.Clamp(_camYaw, 0f, 15.0f);
        
        // Camera should follow the Player
        player.transform.rotation = Quaternion.Euler(_camPitch, _camYaw, 0.0f);
    }
    
    private void LateUpdate()
    {
        transform.localEulerAngles = Vector3.right * _camYaw;
    }

    public void toggleRotation()
    {
        isRotating = !isRotating;
    }
}
