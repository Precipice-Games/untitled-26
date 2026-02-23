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
/// TO DO: add logic for changing camera views between exploration and puzzle states.
/// </summary>
public class ViewManager : MonoBehaviour
{
    [Header("GameUIs")]
    public GameObject mainMenuUI;
    public GameObject explorationUI;
    public GameObject puzzleUI;
    public GameObject pausedUI;
    private GameObject currentUI;

    [Header("Cameras")]
    [SerializeField] private List<Camera> cameras;
    public Camera playerCamera;
    public Camera puzzleCamera;
    public Camera menuCamera;
    private Camera _targetCamera;

    private void Awake()
    {
        // Add cameras to cameras array
        cameras.Add(playerCamera);
        cameras.Add(puzzleCamera);
        cameras.Add(menuCamera);
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
        
        // Handle camera change
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
                }
                else
                {
                    cam.enabled = false;
                }
            }
        }
    }
}
