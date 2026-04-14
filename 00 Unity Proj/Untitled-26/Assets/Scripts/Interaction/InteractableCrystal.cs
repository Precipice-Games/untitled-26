using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableCrystal : MonoBehaviour, IInteractable
{

    public void Interaction()
    {

        SceneManager.LoadScene("Mother_Island");

    }

}
