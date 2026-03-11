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
    public static ResourceManager Instance;


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
    
    [Title("Debug Mode")]
    [InfoBox("Check this variable if you want messages to be debugged from this script. If not, uncheck it.")]
    [PropertyTooltip("Enables or disables debug logs in a given script.")]
    public bool debugMode = true;


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        currentMana = startingMana;
        Debug.Log("ResourceManager.cs >> Starting Mana: " + currentMana);
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
                    if (debugMode) Debug.Log("No Left card uses remaining");
                    return false;
                }
                moveLeftUses--;
                UpdateLeftText(moveLeftUses);
                break;


            case "Right":
                if (moveRightUses <= 0)
                {
                    if (debugMode) Debug.Log("No Right card uses remaining");
                    return false;
                }
                moveRightUses--;
                UpdateRightText(moveRightUses);
                break;


            case "Forward":
                if (moveForwardUses <= 0)
                {
                    if (debugMode) Debug.Log("No Forward card uses remaining");
                    return false;
                }
                moveForwardUses--;
                UpdateUpText(moveForwardUses);
                break;


            case "Back":
                if (moveBackUses <= 0)
                {
                    if (debugMode) Debug.Log("No Back card uses remaining");
                    return false;
                }
                moveBackUses--;
                UpdateDownText(moveBackUses);
                break;
        }


        currentMana--;


        Debug.Log("Mana remaining: " + currentMana);
        Debug.Log("Card used: " + moveType);
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

}