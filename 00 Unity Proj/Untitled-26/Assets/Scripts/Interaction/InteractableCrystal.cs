using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableCrystal : MonoBehaviour, IInteractable
{
    // TODO: Update with proper scene options. Also need to link
    //       this together with the current island completion
    //       system. However, I tested it on Ice Island and can
    //       confirm it sent me to Mother Island. -- Nikki

    public void Interaction()
    {

        SceneManager.LoadScene("Mother_Island");

    }

}
