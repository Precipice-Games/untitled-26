using UnityEngine;

/// <summary>
/// This script stores data regarding the resources provided during each puzzle. Specifically,
/// it keeps track of Mana points and available movement cards for the Player to use.
/// </summary>

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int startingMana = 5;
    private int currentMana;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMana = startingMana;
        Debug.Log("ResourceManager.cs >> Starting Mana: " + currentMana);
    }

    public bool UseMana(int amount)
    {
        if (currentMana < amount)
        {
            Debug.Log("ResourceManager.cs >> No mana to move.");
            return false;
        }

        currentMana -= amount;
        Debug.Log("ResourceManager.cs >> Mana remaining after move: " + currentMana);
        return true;
    }

    public int GetMana()
    {
        return currentMana;
    }
}