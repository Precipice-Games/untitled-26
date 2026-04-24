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
        StartCoroutine(TransitionRoutine(currentScene, nextScene));
    }

    /// <summary>
    /// Ensures the loading screen is present until the next scene is fully loaded.
    /// Once all scenes are loaded, the loading screen is deactivated.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitionRoutine(string currentScene, string nextScene)
    {
        // Activate the loading screen transition
        loadingScreen.SetActive(true);
        
        // yield return new WaitForSeconds(3f);
        
        // Toggle the loading screen for a few seconds
        yield return StartCoroutine(DelayTimer());
        
        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive));

        yield return StartCoroutine(ToggleLoadingScreen(currentScene, nextScene));
    }
    
    private IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(3f);
    }

    private IEnumerator ToggleLoadingScreen(string currentScene, string nextScene)
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                // totalSceneProgress = 0;
                yield return null;
            }
        }
        
        loadingScreen.SetActive(false);
    }
}
