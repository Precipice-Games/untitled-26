using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using Sirenix.OdinInspector;

/// <summary>
/// This class handles physics-related functionality, particularly when transitioning to a new game state.
/// </summary>

public class PhysicsManager : MonoBehaviour
{
    public static event Action<bool> kinematicsUpdated;
    
    // Subscribe to events
    private void OnEnable()
    {
        // GameStateManager.transitionedToNewState += ConfigureKinematics;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        // GameStateManager.transitionedToNewState -= ConfigureKinematics;
    }
    
    // Switches the Player's Rigidbody Kinematics based on the game state
    // private void ConfigureKinematics(GameStateManager.GameState newState)
    // {
    //     bool kinematics;
    //
    //     // Cursor should only be locked and invisible during Exploration mode.
    //     if (newState != GameStateManager.GameState.Puzzle)
    //     {
    //         kinematics = false;
    //     }
    //     else
    //     {
    //         kinematics = true;
    //     }
    //     
    //     kinematicsUpdated?.Invoke(kinematics);
    // }
}