using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessingManager : MonoBehaviour
{
    [Title("Post Processing Variables", "Variables related to the post processor.")]
    [PropertyTooltip("The volume component of the post processor.")]
    public Volume volume;
    
    // Subscribe to events
    private void OnEnable()
    {
        ViewManager.togglePostProcessor += TogglePostProcessor;
    }
    
    // Unsubscribe from events
    private void OnDisable()
    {
        ViewManager.togglePostProcessor -= TogglePostProcessor;
    }

    /// <summary>
    /// Toggles the Volume component of the PostProcessor to enable or disable
    /// post-processing effects. This method is subscribed to the togglePostProcessor
    /// in ViewManager.cs, which is triggered by a game state change.
    /// </summary>
    private void TogglePostProcessor(bool active)
    {
        volume.enabled = active;
        Debug.Log($"PostProcessingManager.cs >> Toggled the volume on the post processor as {active}.");
    }
}
