using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityCommunity.UnitySingleton;
using UnityEngine.SceneManagement;

// This is the GameManager. It is a singleton that will persist across scenes.

public class GameManager : PersistentMonoSingleton<GameManager>
{
    public GameObject loadingScreen;
    
    private void Awake()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Mother_Island", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Ice_Island", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Oasis_Island", LoadSceneMode.Additive);
        
        // In the future, add Flower Island here
        // SceneManager.LoadSceneAsync("Flower_Island", LoadSceneMode.Additive);
    }

    // This is starting the game from the Main Menu
    public void StartGame(string currentScene, string nextScene)
    {
        loadingScreen.SetActive(true);
        // Unload the current scene and asynchronously
        // load the next scene
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.UnloadSceneAsync(nextScene);
        
        
        // SceneManager.UnloadSceneAsync("MainMenu");
        // SceneManager.UnloadSceneAsync("Mother_Island");
        // SceneManager.UnloadSceneAsync("Ice_Island");
        // SceneManager.UnloadSceneAsync("Oasis_Island");
    }
}
