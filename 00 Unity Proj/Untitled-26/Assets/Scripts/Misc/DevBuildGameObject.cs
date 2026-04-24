using UnityEngine;

public class DevBuildGameObject : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(Debug.isDebugBuild);
    }
}
