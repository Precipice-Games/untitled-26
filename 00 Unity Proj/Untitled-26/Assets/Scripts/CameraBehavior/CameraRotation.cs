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
    //Variables
    public Transform player;
    public float mouseSensitivity = 4f;
    private float cameraVerticalRotation = 0f;
    private bool isRotating = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotating)
        {
            //Grabs the mouse input
            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;


            //rotate the camera around local x axis
            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, 0f, 15.0f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            //rotate the players and camera around its Y axis
            player.Rotate(Vector3.up * inputX);
        }
    }

    public void toggleRotation()
    {
        isRotating = !isRotating;
    }
}
