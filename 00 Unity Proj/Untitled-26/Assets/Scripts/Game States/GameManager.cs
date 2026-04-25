using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityCommunity.UnitySingleton;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// This is the GameManager. It is a singleton that will persist across scenes.

public class GameManager : PersistentMonoSingleton<GameManager>
{
    public enum SceneDestination
    {
        MainMenu,
        MotherIsland,
        IceIsland,
        OasisIsland
        
        // Add flower island later
    }
    
    private string scene = "scene name";

    public void IncomingScene(SceneDestination destination)
    {
        scene = DetermineScene(destination);
    }
    
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
    
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
