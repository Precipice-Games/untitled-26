using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is used for buttons that change the scene when pressed.
// It contains an enum for a safer way to determine the next scene, as
// opposed to just using a string.

public class SceneChangerButton :  MonoBehaviour
{
    public enum SceneDestination
    {
        MainMenu,
        MotherIsland,
        IceIsland,
        OasisIsland
        
        // Add flower island later
    }
    
    [Title("SceneChanger Variables", "Variables related to scene switching.")]
    [PropertyTooltip("The scene to load upon pressing this button.")]
    public SceneDestination nextDestination = SceneDestination.MotherIsland; // Default is Mother Island
    
    // Placeholder string that will store name of next scene
    private string scene = "scene";

    /// <summary>
    /// Called by LoadScene(). Returns a string that is the name of the
    /// next scene to load, based on the value of nextDestination.
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    private string DetermineScene(SceneDestination destination)
    {
        switch (destination)
        {
            case SceneDestination.MainMenu:
                scene = "MainMenu";
                break;
            case SceneDestination.MotherIsland:
                scene = "Mother_Island";
                break;
            case SceneDestination.IceIsland:
                scene = "Ice_Island";
                break;
            case SceneDestination.OasisIsland:
                scene = "Oasis_Island";
                break;
        }
        
        return scene;
    }
    
    /// <summary>
    /// Calls DetermineScene() to get the name of the next scene to load.
    /// Triggered by the button's OnClick() event in the Inspector.
    /// </summary>
    public void LoadScene()
    {
        var nextScene = DetermineScene(nextDestination);
        SceneManager.LoadScene(nextScene);
    }
}