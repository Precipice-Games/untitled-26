using UnityEngine;

public class RunningEffect : MonoBehaviour
{

    [SerializeField]
    private Texture_Animation playerAnimations;
    [SerializeField]
    private ParticleSystem _particleSystems;
    [SerializeField]
    private ParticleSystem.EmissionModule playerRunningParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerAnimations = transform.parent.GetComponent<Texture_Animation>();
        _particleSystems = GetComponent<ParticleSystem>();
        playerRunningParticles = _particleSystems.emission;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (playerAnimations != null)
        {

            if (playerAnimations._isRunning)
            {

                playerRunningParticles.enabled = true;

            }
            else
            {

                playerRunningParticles.enabled = false;

            }

        }

    }
}
