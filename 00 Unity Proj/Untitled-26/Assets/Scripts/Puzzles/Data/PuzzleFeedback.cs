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

    void Start()
    {
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