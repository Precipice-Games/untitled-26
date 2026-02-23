using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Inherits from GameStateManager. 
/// ViewManager is responsible for managing the game's UI and camera views based on game state. 
/// This script includes the methods for changing the UI for each game state.
/// It also includes listeners for unity events that trigger state changes.
/// TO DO: add logic for changing camera views between exploration and puzzle states.
/// </summary>
public class ViewManager : MonoBehaviour
{
    [Header("GameUIs")]
    private List<GameObject> uiCanvases;
    public GameObject mainMenuUI;
    public GameObject explorationUI;
    public GameObject puzzleUI;
    public GameObject pausedUI;
    private GameObject currentUI;

    [Header("Cameras")]
    private List<Camera> cameras;
    public Camera playerCamera;
    public Camera puzzleCamera;
    public Camera menuCamera;
    private Camera _targetCamera;

    private void Awake()
    {
        // Initialize cameras list if null
        if (cameras == null)
        {
            cameras = new List<Camera>();
        }
        
        // Clear and rebuild the list to ensure it's up to date
        cameras.Clear();
        
        // Add present cameras to cameras list
        if (playerCamera != null) cameras.Add(playerCamera);
        if (puzzleCamera != null) cameras.Add(puzzleCamera);
        if (menuCamera != null) cameras.Add(menuCamera);
        
        Debug.Log($"ViewManager.cs >> Initialized with {cameras.Count} cameras.");
        
        // Initialize UI canvases list if null
        if (uiCanvases == null)
        {
            uiCanvases = new List<GameObject>();
        }
        
        // Clear and rebuild the list to ensure it's up to date
        uiCanvases.Clear();
        
        // Add present UI Canvases to uiCanvases list
        if (mainMenuUI != null) uiCanvases.Add(mainMenuUI);
        if (explorationUI != null) uiCanvases.Add(explorationUI);
        if (puzzleUI != null) uiCanvases.Add(puzzleUI);
        if (pausedUI != null) uiCanvases.Add(pausedUI);
        
        Debug.Log($"ViewManager.cs >> Initialized with {uiCanvases.Count} UI Canvases.");
    }

    private void OnEnable()
    {
        GameStateManager.transitionedToNewState += HandleUIChange;
    }
    
    private void OnDisable()
    {
        GameStateManager.transitionedToNewState -= HandleUIChange;
    }

    /// <summary>
    /// Handles logic UI toggling.
    /// </summary>
    /// <remarks> 
    /// Once we've transitioned to the new game state, this method will disable the current UI canvas and enable the new one.
    /// </remarks>
    private void HandleUIChange(GameStateManager.GameState newState)
    {
        // Deactivate old UI
        if (currentUI)
        {
            currentUI.SetActive(false);
            Debug.Log("ViewManager.cs >> Disabled the following UI: " + currentUI); // Confirm disabling of UI
        }

        // Set the current UI based on the new game state
        switch (GameStateManager.CurrentGameState)
        {
            case GameStateManager.GameState.MainMenu:
                currentUI = mainMenuUI;
                _targetCamera = menuCamera;
                break;
            case GameStateManager.GameState.Exploration:
                currentUI = explorationUI;
                _targetCamera = playerCamera;
                break;
            case GameStateManager.GameState.Puzzle:
                currentUI = puzzleUI;
                _targetCamera = puzzleCamera;
                break;
            case GameStateManager.GameState.Paused:
                currentUI = pausedUI;
                _targetCamera = menuCamera;
                break;
        }
        
        // activate UI of new state
        currentUI.SetActive(true);
        Debug.Log("ViewManager.cs >> Activated UI for: " + GameStateManager.CurrentGameState);
        
        HandleCameraChange(_targetCamera);
    }
    
    /// <summary>
    /// Handles camera switching.
    /// </summary>
    /// <remarks> 
    /// Once we've transitioned to the new game state, this method will enable the correct camera and disable all others.
    /// </remarks>
    private void HandleCameraChange(Camera targetCamera)
    {
        // TODO: Refactor this if-else tree later on for better readability and maintainability.

        foreach (Camera cam in cameras)
        {
            if (cam != null)
            {
                if (cam == targetCamera)
                {
                    cam.enabled = true;
                    Debug.Log($"ViewManager.cs >> Enabled camera: {cam.name}");
                }
                else
                {
                    cam.enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Listens for the Interact event (e.g., player pressing 'E') and checks current state of the game to switch to puzzle scene if in exploration state.
    /// </summary>
    public void onInteract()
    {
        if(currentState == GameState.Exploration)
        {
            ChangeToPuzzle();
            SceneManager.LoadScene(puzzleScene); // Temporary code to switch scenes until full integration
        }
        else
        {
            Debug.Log("Cannot switch to puzzle scene from current state.");
        }
    }


}
