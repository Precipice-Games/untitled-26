using Sirenix.OdinInspector;
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
    [Title("Camera Rotation Variables", "Variables used in the rotation of the camera. Some influence Player rotation.")]
    [PropertyTooltip("Please attach the Player GameObject.")]
    public GameObject player;
    [PropertyTooltip("Mouse sensitivity. Default is 0.5f.")]
    public float mouseSensitivity = 1f;
    private bool isRotating = true;
    
    // Variables used to store the input values from
    // the Look action on the PlayerControls map
    private float lookX;
    private float lookY;
    private float sqrMag;
    private const float _threshold = 0.01f;
    
    // Variables used to store the current yaw and pitch of
    // the camera. Yaw is the horizontal rotation and pitch
    // is the vertical rotation.
    private float camYaw;
    private float camPitch;

    
    private void Start()
    {
        camYaw = player.transform.rotation.eulerAngles.y;
    }
    
    public void LookRotation(InputAction.CallbackContext context)
    {
        // Get the look input
        lookX = context.ReadValue<Vector2>().x; // Yaw
        lookY = context.ReadValue<Vector2>().y; // Pitch
        sqrMag = context.ReadValue<Vector2>().sqrMagnitude;
        
        // if there is an input and camera position is not fixed
        if (sqrMag >= _threshold)
        {
            // Get the yaw and pitch values
            camYaw += lookX;
            camPitch += lookY;
        }
        
        camPitch = Mathf.Clamp(camPitch, 0f, 15.0f);
        transform.localEulerAngles = Vector3.right * camPitch;
    }

    // TODO: Should we get rid of this method? It
    //       Doesn't appear to be in use. -- Nikki
    public void toggleRotation()
    {
        isRotating = !isRotating;
    }
}
