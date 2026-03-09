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

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMana = startingMana;
        Debug.Log("Starting Mana: " + currentMana);
    }

    public bool UseMove(string moveType)
    {
        if (currentMana <= 0)
        {
            Debug.Log("No mana to move");
            return false;
        }

        switch (moveType)
        {
            case "Left":
                if (moveLeftUses <= 0)
                {
                    Debug.Log("No Left card uses remaining");
                    return false;
                }
                moveLeftUses--;
                break;

            case "Right":
                if (moveRightUses <= 0)
                {
                    Debug.Log("No Right card uses remaining");
                    return false;
                }
                moveRightUses--;
                break;

            case "Forward":
                if (moveForwardUses <= 0)
                {
                    Debug.Log("No Forward card uses remaining");
                    return false;
                }
                moveForwardUses--;
                break;

            case "Back":
                if (moveBackUses <= 0)
                {
                    Debug.Log("No Back card uses remaining");
                    return false;
                }
                moveBackUses--;
                break;
        }

        currentMana--;

        Debug.Log("Mana remaining: " + currentMana);
        Debug.Log("Card used: " + moveType);

        return true;
    }

    public int GetMana()
    {
        return currentMana;
    }
}