using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script takes in and communicates data regarding the feedback of the puzzle.
/// </summary>

public class PuzzleFeedback : MonoBehaviour
{
    public GameObject feedbackPrefab;
    public TMP_Text manaLabel;
    
    // Subscribe to Events
    // private void OnEnable()
    // {
    //     PlayerFixedMovement.playerOccupiedTile += PlayerOccupiedTile;
    //     PlayerFixedMovement.moveBlocked += MoveBlocked;
    //     PlayerFixedMovement.outsideGrid += OutsideGrid;
    // }
    //
    // // Unsubscribe from Events
    // private void OnDisable()
    // {
    //     ResetPuzzle.resetPuzzle -= ResetResources;
    // }
    //
    // private void MoveBlocked(parameters)
    // {
    //     manaLabel.text = $"Mana\n{amount}";
    // }
    //
    // private void OutsideGrid(parameters)
    // {
    //     manaLabel.text = $"Mana\n{amount}";
    // }
}