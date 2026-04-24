using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityCommunity.UnitySingleton;
using UnityEngine.SceneManagement;

// This is the GameManager. It is a singleton that will persist across scenes.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject loadingScreen;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    
    private void Awake()
    {
        Instance = this;
        
        // SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }
    
    /// <summary>
    /// This method takes in the names of the current and next scenes. It unloads the
    /// current one and asynchronously loads the next one. It activates the loading
    /// screen transition while this is occuring. 
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="nextScene"></param>
    public void TransitionToNextScene(string currentScene, string nextScene)
    {
        // Activate the loading screen transition
        loadingScreen.SetActive(true);
        
        // // Asynchronously unload and load the current
        // // scene and next scene, respectively.
        // scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        // scenesLoading.Add(SceneManager.LoadSceneAsync(nextScene));
        //
        // StartCoroutine(GetSceneLoadProgress());
    }

    /// <summary>
    /// Ensures the loading screen is present until the next scene is fully loaded.
    /// Once all scenes are loaded, the loading screen is deactivated.
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetSceneLoadProgress()
    {
        // Scene is still loading
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            // Scene is still loading
            while (!scenesLoading[i].isDone)
            {
                float progress = Mathf.Clamp01(scenesLoading[i].progress / 0.9f);
                Debug.Log("Loading progress: " + progress * 100 + "%");
                yield return null;
            }
            
            loadingScreen.SetActive(false);
        }
        
        
        
        // for (int i = 0; i < scenesLoading.Count; i++)
        // {
        //     // Scene is still loading
        //     while (!scenesLoading[i].isDone)
        //     {
        //         float progress = Mathf.Clamp01(scenesLoading[i].progress / 0.9f);
        //         Debug.Log("Loading progress: " + progress * 100 + "%");
        //         yield return null;
        //     }
        //     
        //     loadingScreen.SetActive(false);
        // }
    }
}
