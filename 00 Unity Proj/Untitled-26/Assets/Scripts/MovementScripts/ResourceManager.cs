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
    
    // TODO: Add movement card data here as well? Right now I'm just
    //       building out a functional version, but want to refactor
    //       code later on for clarity and reusability. Perhaps there
    //       should be a separate class for textual UI updates. Let
    //       me know your thoughts -- Nikki

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMana = startingMana;
        Debug.Log("ResourceManager.cs >> Starting Mana: " + currentMana);
        UpdateManaText(currentMana);
    }

    public bool UseMana(int amount)
    {
        
        if (currentMana < amount)
        {
            Debug.Log("ResourceManager.cs >> No mana to move.");
            return false;
        }

        currentMana -= amount;
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
}