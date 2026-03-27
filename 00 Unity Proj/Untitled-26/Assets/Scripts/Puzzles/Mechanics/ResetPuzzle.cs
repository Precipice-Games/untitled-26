using System;
using UnityEngine;

public class ResetPuzzle : MonoBehaviour
{

    public static event Action resetPuzzle;

    public void OnReset()
    {
        resetPuzzle.Invoke();
    }
}
