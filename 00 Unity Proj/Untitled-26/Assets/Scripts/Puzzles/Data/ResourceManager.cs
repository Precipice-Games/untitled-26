using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// This script stores data regarding the resources provided during each puzzle. Specifically,
/// it keeps track of Mana points and available movement cards for the Player to use.
/// </summary>


public class ResourceManager : MonoBehaviour
{
    public int startingMana = 5;
    private int currentMana;

    [Title("Resource Data")]
    [EnumToggleButtons, HideLabel]
    [InfoBox("Attach the resource data related to the given puzzle.")]
    public TMP_Text manaLabel;
    public TMP_Text leftLabel;
    public TMP_Text upLabel;
    public TMP_Text downLabel;

    public TMP_Text rightLabel;

    public int moveLeftUses = 3;
    public int moveRightUses = 3;
    public int moveForwardUses = 3;
    public int moveBackUses = 3;
    
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

    public bool UseMove(string moveType)
    {
        if (currentMana <= 0)
        {
            Debug.Log("ResourceManager.cs >> No mana to move.");
            return false;
        }


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


    public int GetMana()
    {
        return currentMana;
    }

     
    private void UpdateManaText(int amount)
    {
        manaLabel.text = $"Mana\n{amount}";
    }

    private void UpdateLeftText(int amount)
    {
        leftLabel.text = amount.ToString();
    }

    private void UpdateRightText(int amount)
    {
        rightLabel.text = amount.ToString();
    }

    private void UpdateUpText(int amount)
    {
        upLabel.text = amount.ToString();
    }

    private void UpdateDownText(int amount)
    {
        downLabel.text = amount.ToString();
    }

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