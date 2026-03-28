using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleTimer;

/// <summary>
/// This script takes in and communicates data regarding the feedback of the puzzle.
/// </summary>

public class PuzzleFeedback : MonoBehaviour
{
    public GameObject feedbackPrefab;
    public TMP_Text onScreenText;
    
    private TimerManager.Timer myTimer;
    
    // Subscribe to events
    private void OnEnable()
    {
        SelectableTile.cellOccupied += CellIsOccupied;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        SelectableTile.cellOccupied -= CellIsOccupied;
    }

    void Start()
    {
        // myTimer = TimerManager.CreateTimer(3.0f, OnTimerFinished, OnTimerTick);
    }

    private void CellIsOccupied(SelectableTile tile)
    {
        Debug.Log($"PuzzleFeedback.cs >> You tried to move {tile}, but another tile is occupying that cell!");
        myTimer = TimerManager.CreateTimer(3.0f, OnTimerFinished, OnTimerTick);
    }
    
    void OnTimerFinished()
    {
        Debug.Log("Timer finished!");
        
        // MUST delete the timer to prevent a memory leak
        TimerManager.DeleteTimer(myTimer);
    }

    void OnTimerTick()
    {
        Debug.Log("Timer running...");
    }
}