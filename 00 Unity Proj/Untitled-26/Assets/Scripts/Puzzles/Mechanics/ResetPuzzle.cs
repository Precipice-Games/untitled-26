using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetPuzzle : MonoBehaviour
{

    public static event Action resetPuzzle;


    public static void OnReset()
    {
        Debug.Log("Resetting Puzzle");
        resetPuzzle.Invoke();
    }
}
