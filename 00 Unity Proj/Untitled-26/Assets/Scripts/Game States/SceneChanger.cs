using System;
using Sirenix.OdinInspector;
using UnityEngine;

// This script is used for toggling the loading screen and triggering
// scene changes. It can be used for button presses, airship boarding,
// and more. It must be attached to whatever object is using it.

public class SceneChanger :  MonoBehaviour
{
    [Title("SceneChanger Variables", "Variables related to scene switching.")]
    [PropertyTooltip("The next scene to load.")]
    public GameManager.SceneDestination nextDestination = GameManager.SceneDestination.MotherIsland; // Default is Mother Island
    
    // Static event to queue the loading screen
    public static event Action queueLoadingScreen;
    
    /// <summary>
    /// Informs the GameManager of the next scene to load, and fires queueLoadingScreen
    /// to trigger the loading screen. Once the animation is completed in the ViewManager,
    /// it will trigger an event to actually switch us to the next scene.
    /// </summary>
    public void LoadScene()
    {
        // Inform the game manager of the next scene to load,
        // so that it can set the scene variable accordingly.
        GameManager.Instance.IncomingScene(nextDestination);
        queueLoadingScreen?.Invoke();
    }
}