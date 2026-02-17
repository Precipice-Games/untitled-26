using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    //Variables
    public Transform player;
    public float mouseSensitivity = 4f;
    private float cameraVerticalRotation = 0f;

    // Update is called once per frame
    void FixedUpdate()
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
