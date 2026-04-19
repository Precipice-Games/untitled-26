using UnityEngine;
using Yarn.Unity;

public class YarnPlatformTrigger : MonoBehaviour
{
    public MovingPlatform[] platforms;
    public Collider boxColliderToDisable;

    [YarnCommand("activatePlatforms")]
    public void ActivatePlatforms()
    {
        // Activate all platforms
        foreach (var platform in platforms)
        {
            platform.ActivatePlatform();
        }

        // Disable collider
        if (boxColliderToDisable != null)
        {
            boxColliderToDisable.enabled = false;
        }
    }
}