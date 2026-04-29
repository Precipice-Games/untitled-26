using System;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using SimpleTimer;

/// <summary>
/// This script takes in and communicates data regarding the feedback of the puzzle.
/// Handles puzzle feedback for invalid moves and other messages.
/// </summary>
public class PuzzleFeedback : MonoBehaviour
{
    [Title("UI Elements")]
    public GameObject feedbackObject;
    public TMP_Text onScreenText;
    
    private TimerManager.Timer flickerTimer;
    private SelectableTile tile;
    private Renderer tileRenderer;
    private Color originalColor;
    private Color flashColor = Color.red;

    private bool isFlashing = false;
    
    // Intervals used to determine when to flash the tile's color during the timer
    // Flash intervals
    private float errorLastIntervalTime = 0f;
    private float errorInterval = 0.25f;
    
    void Start()
    {
        // Hide popup text at start
        if (feedbackObject != null){
            ClearMessage();
        }
    }
    
    // Subscribe to events
    private void OnEnable()
    {
        SelectableTile.cellOccupied += CellIsOccupied;
        SelectableTile.moveOutOfBounds += MoveIsOutOfBounds;
        ResourceManager.noMoreCardUses += NoMoreCardUses;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        SelectableTile.cellOccupied -= CellIsOccupied;
        SelectableTile.moveOutOfBounds -= MoveIsOutOfBounds;
        ResourceManager.noMoreCardUses -= NoMoreCardUses;
    }

    private void CellIsOccupied(SelectableTile selectableTile)
    {
        StartFeedback(selectableTile, "That space is occupied!");
    }
    
    private void MoveIsOutOfBounds(SelectableTile selectableTile)
    {
        StartFeedback(selectableTile, "You cannot move outside the grid!");
    }

    /// <summary>
    /// NEW: Triggered when player has no more card usages left
    /// </summary>
    private void NoMoreCardUses()
    {
        StartFeedback(null, "No more card usages!");
    }

    /// <summary>
    /// General method to show feedback on screen and flash the tile.
    /// </summary>
    public void ShowMessage(string message)
    {
        StartFeedback(null, message);
    }

    private void PlayInvalidMoveSFX()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayInvalidMove();
        }
    }
    private void StartFeedback(SelectableTile selectableTile, string message)
    {
        tile = selectableTile;

        tileRenderer = tile != null ? tile.GetComponent<Renderer>() : null;

        if (tileRenderer != null)
            originalColor = tileRenderer.material.color;

        // Reset interval timing every time an error happens
        errorLastIntervalTime = 0f;
        isFlashing = false;

        // Play SFX for invalid move
        PlayInvalidMoveSFX();
        
        // Stop previous timer if it exists
        if (flickerTimer != null)
            TimerManager.DeleteTimer(flickerTimer);

        // Show popup text
        if (onScreenText != null)
        {
            onScreenText.text = message;
            Debug.Log($"PuzzleFeedback.cs >> The onScreenText message was set to {onScreenText.text}");
        }

        // Create a 2-second timer with tick for flashing
        flickerTimer = TimerManager.CreateTimer(2.0f, OnTimerFinished, OnTimerTick);
    }

    private void OnTimerFinished()
    {
        Debug.Log("PuzzleFeedback.cs >> Timer finished!");

        // Reset tile color
        if (tileRenderer != null){
            tileRenderer.material.color = originalColor;
        }

        // Hide popup text
        if (onScreenText != null)
        {
            ClearMessage();
        }

        // Ensure the timer is deleted
        if (flickerTimer != null)
        {
            TimerManager.DeleteTimer(flickerTimer);
            flickerTimer = null;
        }
    }

    private void OnTimerTick()
    {
        if (tileRenderer == null) return;

        float elapsedTime = flickerTimer.GetElapsedTime();
        bool intervalHit = elapsedTime - errorLastIntervalTime >= errorInterval;

        if (intervalHit)
        {
            errorLastIntervalTime = elapsedTime;

            // Toggle color
            isFlashing = !isFlashing;

            tileRenderer.material.color = isFlashing ? flashColor : originalColor;
        }
    }
    
    /// <summary>
    /// Helper method that clears the onscreen text. It does not disable
    /// the object, it just sets the text to an empty string.
    /// </summary>
    private void ClearMessage()
    {
        onScreenText.text = "";
    }
}