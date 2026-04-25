using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is used for triggering scene changes. It can be used
// for button presses, airship boarding, and more.

public class SceneChanger :  MonoBehaviour
{
    [Title("SceneChanger Variables", "Variables related to scene switching.")]
    [PropertyTooltip("The scene to load upon pressing this button.")]
    public GameManager.SceneDestination nextDestination = GameManager.SceneDestination.MotherIsland; // Default is Mother Island
    
    // Static event to notify subscribers of game state changes
    public static event Action queueLoadingScreen;
    
    /// <summary>
    /// Calls DetermineScene() to get the name of the next scene to load.
    /// Triggered by the button's OnClick() event in the Inspector.
    /// </summary>
    public void LoadScene()
    {
        // Inform the game manager of the next scene to load, so that it can set the scene variable accordingly.
        GameManager.Instance.IncomingScene(nextDestination);
        queueLoadingScreen?.Invoke();
    }
}