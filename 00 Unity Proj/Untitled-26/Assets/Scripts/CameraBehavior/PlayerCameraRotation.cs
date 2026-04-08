using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script allows the camera to revolve around the Player. The Player
/// will always face the camera itself. Additionally, DO NOT add any controls
/// or cursor handling here; that is all handled in seperate files and events,
/// such as game state switches.
/// </summary>

public class PlayerCameraRotation : MonoBehaviour
{
    // ==== Movement ====
    [Title("Camera Variables", "Variables used for the Player's camera.")]
    [PropertyTooltip("Please attach the Player GameObject.")]
    public GameObject player;
    [PropertyTooltip("Mouse sensitivity. Default is 0.5f.")]
    public float mouseSensitivity = 1f;
    private bool isRotating = true;
    
    [Title("Cinemachine Variables", "Variables used specifically for Cinemachine.")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject cameraTarget;
    
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    
    // Variables used to store the input values from
    // the Look action on the PlayerControls map
    private Vector2 look;
    private float sqrMag;
    private const float _threshold = 0.01f;
    
    // Variables used to store the current yaw and pitch of
    // the camera. Yaw is the horizontal rotation and pitch
    // is the vertical rotation.
    private float camYaw;
    private float camPitch;
    private PlayerInput playerInput;

    
    private void Start()
    {
        camYaw = cameraTarget.transform.rotation.eulerAngles.y;
        playerInput = player.GetComponent<PlayerInput>();
    }

    // LateUpdate() is good for camera updates.
    private void LateUpdate()
    {
        CameraRotation();
    }
    

    // This just gets the look values
    public void LookRotation(InputAction.CallbackContext context)
    {
        // Get the look input
        look = context.ReadValue<Vector2>();
        sqrMag = context.ReadValue<Vector2>().sqrMagnitude;
        
        // if there is an input and camera position is not fixed
        if (sqrMag >= _threshold)
        {
            // Get the yaw and pitch values
            camYaw += look.x;
            camPitch += look.y;
        }
    }

    // Called in LateUpdate()
    private void CameraRotation()
    {

        // clamp our rotations so our values are limited 360 degrees
        // _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        camYaw = ClampAngle(camYaw, float.MinValue, float.MaxValue);
        
        // _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
        camPitch = ClampAngle(camPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        cameraTarget.transform.rotation = Quaternion.Euler(camPitch, camYaw, 0.0f);
    }
    
    // Clamps the angle of the camera
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
