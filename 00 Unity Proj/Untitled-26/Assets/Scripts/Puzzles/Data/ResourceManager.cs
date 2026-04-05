using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script stores data regarding the resources provided during each puzzle.
// Specifically, it keeps track of Mana points and available movement cards for
// the Player to use. Note that it maintains a tight relationship with
// TileSelector.cs to check resource availability for performing a move.

public class ResourceManager : MonoBehaviour
{
    [Title("Resource Data", "Set the appropriate Mana and Move Card count for this puzzle.")]
    [PropertyTooltip("The starting Mana count. Set to 5 by default, but should be updated per puzzle.")]
    public int startingMana = 5;
    [PropertyTooltip("The starting Left Card move count. Set to 3 by default, but should be updated per puzzle.")]
    public int moveLeftUses = 3;
    [PropertyTooltip("The starting Right Card move count. Set to 3 by default, but should be updated per puzzle.")]
    public int moveRightUses = 3;
    [PropertyTooltip("The starting Up/Forward Card move count. Set to 3 by default, but should be updated per puzzle.")]
    public int moveForwardUses = 3;
    [PropertyTooltip("The starting Down/Back Card move count. Set to 3 by default, but should be updated per puzzle.")]
    public int moveBackUses = 3;
    private int currentMana;

    [Space]
    [Title("UI Data", "Attach the UI elements for this puzzle's resources.")]
    public TMP_Text manaLabel;
    public TMP_Text leftLabel;
    public TMP_Text rightLabel;
    public TMP_Text upLabel;
    public TMP_Text downLabel;
    
    [Space]
    [Title("Debugging Options", "Settings for quick debugging options.")]
    [PropertyTooltip("Prints out the starting value for each resource. True by default.")]
    public bool printStartingValues = true;
    [PropertyTooltip("Print out the value of the Mana resource after being used. True by default.")]
    public bool printManaDeductions = true;
    [PropertyTooltip("Print out the value of a movement card after being used. True by default.")]
    public bool printCardDeductions = true;
    [PropertyTooltip("Print out when a specific movement card has no uses left. True by default.")]
    public bool printCardDrainage = true;
    
    // Subscribe to events
    private void OnEnable()
    {
        ResetPuzzle.resetPuzzle += ResetResources;
    }

    // Unsubscribe from events
    private void OnDisable()
    {
        ResetPuzzle.resetPuzzle -= ResetResources;
    }

    void Start()
    {
        currentMana = startingMana;
        if (printStartingValues) Debug.Log("ResourceManager.cs >> Starting Mana: " + currentMana);
        if (printStartingValues) Debug.Log("ResourceManager.cs >> Starting Left Card Uses: " + moveLeftUses);
        if (printStartingValues) Debug.Log("ResourceManager.cs >> Starting Right Card Uses: " + moveRightUses);
        if (printStartingValues) Debug.Log("ResourceManager.cs >> Starting Up Card Uses: " + moveForwardUses);
        if (printStartingValues) Debug.Log("ResourceManager.cs >> Starting Down Card Uses: " + moveBackUses);
        
        UpdateManaText(currentMana);

        UpdateLeftText(moveLeftUses);
        UpdateRightText(moveRightUses);
        UpdateUpText(moveForwardUses);
        UpdateDownText(moveBackUses);
    }

    /// <summary>
    /// Called in TileSelector.cs when the Player tries to use a movement card.
    /// Checks if the Player has enough resources before deducting anything. If
    /// a move is valid, return true. Otherwise, return false.
    /// </summary>
    /// <param name="moveType"></param>
    /// <returns></returns>
    public bool UseMove(string moveType)
    {
        // Ensure that the Player has enough Mana to make a move
        if (currentMana <= 0)
        {
            Debug.Log("ResourceManager.cs >> No mana to move.");
            return false;
        }

        // Ensure that there's enough card uses for the specified move
        switch (moveType)
        {
            case "Left":
                if (moveLeftUses <= 0)
                {
                    if (printCardDrainage) Debug.Log("ResourceManager.cs >> No Left card uses remaining");
                    return false;
                }
                moveLeftUses--;
                UpdateLeftText(moveLeftUses);
                break;


            case "Right":
                if (moveRightUses <= 0)
                {
                    if (printCardDrainage) Debug.Log("ResourceManager.cs >> No Right card uses remaining");
                    return false;
                }
                moveRightUses--;
                UpdateRightText(moveRightUses);
                break;


            case "Forward":
                if (moveForwardUses <= 0)
                {
                    if (printCardDrainage) Debug.Log("ResourceManager.cs >> No Forward card uses remaining");
                    return false;
                }
                moveForwardUses--;
                UpdateUpText(moveForwardUses);
                break;


            case "Back":
                if (moveBackUses <= 0)
                {
                    if (printCardDrainage) Debug.Log("ResourceManager.cs >> No Back card uses remaining");
                    return false;
                }
                moveBackUses--;
                UpdateDownText(moveBackUses);
                break;
        }
        
        currentMana--;

        Debug.Log("ResourceManager.cs >> Mana remaining: " + currentMana);
        Debug.Log("ResourceManager.cs >> Card used: " + moveType);
        UpdateManaText(currentMana);
        Debug.Log("ResourceManager.cs >> Mana remaining after move: " + currentMana);

        return true;
    }
    
    /// <summary>
    /// Returns the current Mana count.
    /// </summary>
    /// <returns></returns>
    public int GetMana()
    {
        return currentMana;
    }
    
    /// <summary>
    /// Updates the Mana text on the UI.
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateManaText(int amount)
    {
        manaLabel.text = $"Mana\n{amount}";
    }

    /// <summary>
    /// Updates the Left card uses text on the UI.
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateLeftText(int amount)
    {
        leftLabel.text = amount.ToString();
    }

    /// <summary>
    /// Updates the Right card uses text on the UI.
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateRightText(int amount)
    {
        rightLabel.text = amount.ToString();
    }

    /// <summary>
    /// Updates the Up card uses text on the UI.
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateUpText(int amount)
    {
        upLabel.text = amount.ToString();
    }
    
    /// <summary>
    /// Updates the Down card uses text on the UI.
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateDownText(int amount)
    {
        downLabel.text = amount.ToString();
    }
    
    /// <summary>
    /// Resets the Mana and movement card resources to their starting
    /// values. Called when the resetPuzzle event is triggered.
    /// </summary>
    private void ResetResources()
    {
        currentMana = startingMana;
        moveLeftUses = 3;
        moveRightUses = 3;
        moveForwardUses = 3;
        moveBackUses = 3;
        UpdateManaText(currentMana);
        UpdateLeftText(moveLeftUses);
        UpdateRightText(moveRightUses);
        UpdateUpText(moveForwardUses);
        UpdateDownText(moveBackUses);
    }
}