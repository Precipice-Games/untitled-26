using UnityEngine;

public class DevBuildGameObject : MonoBehaviour
{
    private void Awake()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log($"Debug.isDebugBuild >> {Debug.isDebugBuild}");
        }
        
        gameObject.SetActive(Debug.isDebugBuild);
    }
}
