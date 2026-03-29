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
        // TODO: Is there anyway we could improve the polling rate or the way the mouse
        //       input is detected? I've noticed that the mouse input is totally fine when
        //       I'm turning on my laptop, but when I try to play on my desktop, it's very
        //       laggy and hard to turn the character. It could be a problem with the
        //       polling or update rate. Let me know what you guys think. -- Nikki
        
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
