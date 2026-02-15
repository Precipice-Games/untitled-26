using UnityEngine;

// Attach this script to a button to exit the application.

public class QuitApplication : MonoBehaviour
{
    public void LeaveApplication()
    {
        Application.Quit();
    }
}
