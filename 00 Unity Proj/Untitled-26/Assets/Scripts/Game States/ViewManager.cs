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
    public GameObject settingsUI;
    private GameObject _targetUI;

    [Header("Cameras")]
    private List<Camera> cameras;
    public Camera playerCamera;
    public Camera puzzleCamera;
    public Camera menuCamera;
    public Camera dialogueCamera;
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
        if (dialogueCamera != null) cameras.Add(dialogueCamera);
        
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
        // if (dialogueUI != null) uiCanvases.Add(dialogueUI);
        
        Debug.Log($"ViewManager.cs >> Initialized with {uiCanvases.Count} UI Canvases.");
    }

    private void OnEnable()
    {
        GameStateManager.transitionedToNewState += HandleViewChange;
    }
    
    private void OnDisable()
    {
        GameStateManager.transitionedToNewState -= HandleViewChange;
    }

    /// <summary>
    /// Handles logic regarding what UI and camera to toggle.
    /// </summary>
    /// <remarks>
    /// Once we've transitioned to the new game state, this method will figure out what the new UI and camera should be based on the new game state, and then call the methods to enable the correct UI and camera while disabling all others.
    /// </remarks>
    private void HandleViewChange(GameStateManager.GameState newState)
    {
        // Set the current UI based on the new game state
        switch (newState)
        {
            case GameStateManager.GameState.MainMenu:
                _targetUI = mainMenuUI;
                _targetCamera = menuCamera;
                break;
            case GameStateManager.GameState.Exploration:
                _targetUI = explorationUI;
                _targetCamera = playerCamera;
                break;
            case GameStateManager.GameState.Puzzle:
                _targetUI = puzzleUI;
                _targetCamera = puzzleCamera;
                break;
            // case GameStateManager.GameState.Dialogue:
            //     _targetUI = dialogueUI;
            //     _targetCamera = dialogueCamera;
            //     break;
            case GameStateManager.GameState.Paused:
                _targetUI = pausedUI;
                _targetCamera = menuCamera;
                break;
            case GameStateManager.GameState.Settings:
                _targetUI = settingsUI;
                _targetCamera = menuCamera;
                break;
        }
        
        HandleCanvasChange(_targetUI);
        HandleCameraChange(_targetCamera);
    }
    
    
    /// <summary>
    /// Handles UI Canvas switching.
    /// </summary>
    /// <remarks> 
    /// This method enables the correct UI and disables all others.
    /// </remarks>
    private void HandleCanvasChange(GameObject targetCanvas)
    {
        foreach (GameObject canvas in uiCanvases)
        {
            if (canvas != null)
            {
                if (canvas == targetCanvas)
                {
                    canvas.SetActive(true);
                    Debug.Log($"ViewManager.cs >> Enabled UI Canvas: {canvas.name}");
                }
                else
                {
                    canvas.SetActive(false);
                    Debug.Log($"ViewManager.cs >> Disabled UI Canvas: {canvas.name}");
                }
            }
        }
    }
    
    /// <summary>
    /// Handles camera switching.
    /// </summary>
    /// <remarks> 
    /// This method enables the correct camera and disables all others.
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
                    Debug.Log($"ViewManager.cs >> Disabled camera: {cam.name}");
                }
            }
        }
    }
}
