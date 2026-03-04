using UnityEngine;

public class OptionsExitButton : MonoBehaviour
{

    public UIChangerButton exitButton;
    public GameObject mainMenuUI;
    public GameObject pausedUI;
    
    public void returnToMenu()
    {
        if (GameStateManager.CurrentGameState == GameStateManager.GameState.Paused)
        {
            exitButton.targetUI = pausedUI;
        }
        else if (GameStateManager.CurrentGameState == GameStateManager.GameState.MainMenu)
        {
            exitButton.targetUI = mainMenuUI;
        }

        exitButton.swapUI();
    }
}
