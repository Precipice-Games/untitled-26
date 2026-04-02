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

    private bool isFlashing = false;
    
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
        tile = selectableTile;
        tileRenderer = tile.GetComponent<Renderer>();
        originalColor = tileRenderer.material.color;

        // Reset interval timing every time an error happens
        errorLastIntervalTime = 0f;
        isFlashing = false;
        // The flicker did not remember the last time it flashed so it never reset.
    
        Debug.Log($"PuzzleFeedback.cs >> You tried to move {tile}, but another tile is occupying that cell!");
        flickerTimer = TimerManager.CreateTimer(2.0f, OnTimerFinished, OnTimerTick);
    }
    
    void OnTimerFinished()
    {
        Debug.Log("Timer finished!");
        tileRenderer.material.color = originalColor;
        
        TimerManager.DeleteTimer(flickerTimer);
    }

    void OnTimerTick()
    {
        float elapsedTime = flickerTimer.GetElapsedTime();
        bool intervalHit = elapsedTime - errorLastIntervalTime >= errorInterval;

        if (intervalHit)
        {
            errorLastIntervalTime = elapsedTime;

            // Toggle color instead of comparing colors
            isFlashing = !isFlashing;

            if (isFlashing)
                tileRenderer.material.color = flashColor;
            else
                tileRenderer.material.color = originalColor;

            Debug.Log("Interval hit!");
        }

        Debug.Log("Timer running...");
    }
}