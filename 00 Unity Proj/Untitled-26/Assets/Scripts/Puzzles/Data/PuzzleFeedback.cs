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
    public GameObject feedbackText;
    public float elapsedTime;
    private TMP_Text messageText;
    private TimerManager.Timer flickerTimer;
    private SelectableTile tile;
    private Renderer tileRenderer;
    private Color originalColor;
    private Color flashColor = Color.red;

    private bool isFlashing = false;
    
    private PuzzleInformation thisPuzzle;
    
    // Intervals used to determine when to flash the tile's color during the timer
    // Flash intervals
    private float errorLastIntervalTime = 0f;
    private float errorInterval = 0.25f;
    
    [Space]
    [Title("Debugging Options", "Settings for quick debugging options.")]
    [PropertyTooltip("Print feedback text updates. False by default.")]
    public bool printFeedbackUpdates = false;
    [PropertyTooltip("Print out info regarding the flicker timer. False by default.")]
    public bool printTimerInfo = false;
    
    void Start()
    {
        // Get reference to the text component
        messageText = feedbackText.GetComponent<TMP_Text>();
        
        // Identify what puzzle this script belongs to
        thisPuzzle = GetComponentInParent<PuzzleInformation>();
        
        // Hide popup text at start
        feedbackText.SetActive(false);
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

    private void CellIsOccupied(PuzzleInformation puzzleInfo, SelectableTile selectableTile)
    {
        if (puzzleInfo == thisPuzzle)
        {
            StartFeedback(selectableTile, "That space is occupied!");
        }
    }
    
    private void MoveIsOutOfBounds(PuzzleInformation puzzleInfo, SelectableTile selectableTile)
    {
        if (puzzleInfo == thisPuzzle)
        {
            StartFeedback(selectableTile, "You cannot move outside the grid!");
        }
    }

    /// <summary>
    /// NEW: Triggered when player has no more card usages left
    /// </summary>
    public void NoMoreCardUses(PuzzleInformation puzzleInfo, string message)
    {
        if (puzzleInfo == thisPuzzle)
        {
            StartFeedback(null, "No more card usages!");
        }
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

        // Store original color to reset to later
        if (tileRenderer != null) originalColor = tileRenderer.material.color;

        // Reset interval timing every time an error happens
        errorLastIntervalTime = 0f;
        isFlashing = false;

        // Play SFX for invalid move
        PlayInvalidMoveSFX();
        
        // Stop previous timer if it exists
        if (flickerTimer != null) TimerManager.DeleteTimer(flickerTimer);

        // Show popup text
        if (feedbackText != null)
        {
            messageText.text = message;
            feedbackText.SetActive(true);
            if (printFeedbackUpdates) Debug.Log($"PuzzleFeedback.cs >> The onScreenText message was set to {messageText.text}");
        }

        // Create a 2-second timer with tick for flashing
        flickerTimer = TimerManager.CreateTimer(2.0f, OnTimerFinished, OnTimerTick);
    }

    private void OnTimerFinished()
    {
        if (printTimerInfo) Debug.Log("PuzzleFeedback.cs >> Timer finished!");

        // Reset tile color
        tileRenderer.material.color = originalColor;
        
        // Disable popup text
        feedbackText.SetActive(false);

        // Ensure the timer is deleted
        TimerManager.DeleteTimer(flickerTimer);
    }

    private void OnTimerTick()
    {
        if (tileRenderer == null) return;
        
        elapsedTime = flickerTimer.GetElapsedTime();
        bool intervalHit = elapsedTime - errorLastIntervalTime >= errorInterval;

        if (intervalHit)
        {
            errorLastIntervalTime = elapsedTime;
            
            Debug.Log("PuzzleFeedback.cs >> Timer tick: " + elapsedTime + " seconds elapsed. Flashing tile color.");

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
        messageText.text = "";
    }
}