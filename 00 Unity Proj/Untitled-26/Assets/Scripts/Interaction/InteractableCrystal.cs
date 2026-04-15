using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableCrystal : MonoBehaviour, IInteractable
{

    // TODO: Update with proper scene options. Also need to link
    //       this together with the current island completion
    //       system. However, I tested it on Ice Island and can
    //       confirm it sent me to Mother Island. -- Nikki

    [SerializeField]
    PuzzleInformation finalPuzzle;

    private void FixedUpdate()
    {

        if (finalPuzzle != null && finalPuzzle.puzzleSolved == true && this.transform.position.y < 3.2)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        }

    }

    public void Interaction()
    {

        SceneManager.LoadScene("Mother_Island");

    }

}
