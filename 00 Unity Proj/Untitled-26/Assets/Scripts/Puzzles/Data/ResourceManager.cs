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
        if (debugMode) Debug.Log("Starting Mana: " + currentMana);
    }

    public bool UseMove(string moveType)
    {
        if (currentMana <= 0)
        {
            if (debugMode) Debug.Log("No mana to move");
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
                break;

            case "Right":
                if (moveRightUses <= 0)
                {
                    if (debugMode) Debug.Log("No Right card uses remaining");
                    return false;
                }
                moveRightUses--;
                break;

            case "Forward":
                if (moveForwardUses <= 0)
                {
                    if (debugMode) Debug.Log("No Forward card uses remaining");
                    return false;
                }
                moveForwardUses--;
                break;

            case "Back":
                if (moveBackUses <= 0)
                {
                    if (debugMode) Debug.Log("No Back card uses remaining");
                    return false;
                }
                moveBackUses--;
                break;
        }

        currentMana--;

        if (debugMode) Debug.Log("Mana remaining: " + currentMana);
        if (debugMode) Debug.Log("Card used: " + moveType);

        return true;
    }

    public int GetMana()
    {
        return currentMana;
    }
}