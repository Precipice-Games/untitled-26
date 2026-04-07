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
    
    void Start()
    {
        // Hide popup text at start
        if (onScreenText != null)
            onScreenText.gameObject.SetActive(false);
    }
    
    // Subscribe to events
    private void OnEnable()
    {
        SelectableTile.cellOccupied += CellIsOccupied;
        SelectableTile.moveOutOfBounds += MoveIsOutOfBounds;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        SelectableTile.cellOccupied -= CellIsOccupied;
        SelectableTile.moveOutOfBounds -= MoveIsOutOfBounds;
    }

    private void CellIsOccupied(SelectableTile selectableTile)
    {
        StartFeedback(selectableTile, "That space is occupied!");
    }
    
    private void MoveIsOutOfBounds(SelectableTile selectableTile)
    {
        StartFeedback(selectableTile, "You can't move outside the grid!");
    }

    private void StartFeedback(SelectableTile selectableTile, string message)
    {
        tile = selectableTile;
        tileRenderer = tile.GetComponent<Renderer>();
        originalColor = tileRenderer.material.color;

        // Reset interval timing every time an error happens
        errorLastIntervalTime = 0f;
        isFlashing = false;

        // Show popup text
        if (onScreenText != null)
        {
            onScreenText.text = message;
            onScreenText.gameObject.SetActive(true);
        }

        Debug.Log($"PuzzleFeedback.cs >> {message}");
        flickerTimer = TimerManager.CreateTimer(2.0f, OnTimerFinished, OnTimerTick);
    }

    void OnTimerFinished()
    {
        Debug.Log("Timer finished!");
        tileRenderer.material.color = originalColor;

        // Hide popup text
        if (onScreenText != null)
            onScreenText.gameObject.SetActive(false);
        
        TimerManager.DeleteTimer(flickerTimer);
    }

    void OnTimerTick()
    {
        float elapsedTime = flickerTimer.GetElapsedTime();
        bool intervalHit = elapsedTime - errorLastIntervalTime >= errorInterval;

        if (intervalHit)
        {
            errorLastIntervalTime = elapsedTime;

            // Toggle color
            isFlashing = !isFlashing;

            if (isFlashing)
                tileRenderer.material.color = flashColor;
            else
                tileRenderer.material.color = originalColor;
        }
    }
}