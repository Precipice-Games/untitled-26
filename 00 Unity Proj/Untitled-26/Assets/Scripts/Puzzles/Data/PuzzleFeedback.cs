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
    
    private TimerManager.Timer flickerTimer;

    private SelectableTile tile;
    private Renderer tileRenderer;
    private Color originalColor;
    private Color flashColor = Color.red;
    
    // Intervals used to determine when to flash the tile's color during the timer
    private float errorLastIntervalTime = 0f;
    private float errorInterval = 0.25f;
    
    
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

    private void CellIsOccupied(SelectableTile selectableTile)
    {
        // Get the data of the tile before the timer starts so
        // that we can use it in the tick and finish methods
        tile = selectableTile;
        tileRenderer = tile.GetComponent<Renderer>();
        originalColor = tileRenderer.material.color;
        
        Debug.Log($"PuzzleFeedback.cs >> You tried to move {tile}, but another tile is occupying that cell!");
        flickerTimer = TimerManager.CreateTimer(2.0f, OnTimerFinished, OnTimerTick);
    }
    
    void OnTimerFinished()
    {
        Debug.Log("Timer finished!");
        tileRenderer.material.color = originalColor;
        
        // MUST delete the timer to prevent a memory leak
        TimerManager.DeleteTimer(flickerTimer);
    }

    void OnTimerTick()
    {
        var color = tileRenderer.material.color;
        float elapsedTime = flickerTimer.GetElapsedTime();
        bool intervalHit = elapsedTime - errorLastIntervalTime >= errorInterval;

        if (intervalHit)
        {
            errorLastIntervalTime = elapsedTime;
            
            switch (color)
            {
                case var c when c == originalColor:
                    tileRenderer.material.color = flashColor;
                    break;
                case var c when c == flashColor:
                    tileRenderer.material.color = originalColor;
                    break;
            }
            Debug.Log("Interval hit!");
        }
        Debug.Log("Timer running...");
    }
}