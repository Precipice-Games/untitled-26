using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerButton :  MonoBehaviour
{
    public string scene = "<Insert scene name>";

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}