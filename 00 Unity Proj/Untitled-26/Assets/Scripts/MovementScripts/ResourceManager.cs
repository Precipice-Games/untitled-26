using UnityEngine;

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
        Debug.Log("Starting Mana: " + currentMana);
    }

    public bool UseMana(int amount)
    {
        if (currentMana < amount)
        {
            Debug.Log("No mana to move");
            return false;
        }

        currentMana -= amount;
        Debug.Log("Mana after move: " + currentMana);
        return true;
    }

    public int GetMana()
    {
        return currentMana;
    }
}