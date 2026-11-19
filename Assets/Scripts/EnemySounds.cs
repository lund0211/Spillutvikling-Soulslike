using UnityEngine;
using System.Collections;

public class EnemySounds : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    AudioClip ZombieNoises;
    AudioSource audioSource;

    NormalEnemyBehavior EnemyBehavior;

    float volume;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume = AudioManager.Instance.masterVolume;
        EnemyBehavior = GetComponent<NormalEnemyBehavior>();

        PlayZombieSound(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayZombieSound(float delay, float audioVolumeMultiplier)
    {
        StartCoroutine(LoopZombieSound(delay, audioVolumeMultiplier));
    }

    public IEnumerator LoopZombieSound(float delayBetweenSounds, float audioVolumeMultiplier)
    {
        while (true) { 
            if (EnemyBehavior.IsDead == false)
            {
                audioSource.PlayOneShot(ZombieNoises, volume * audioVolumeMultiplier);
                yield return new WaitForSeconds(delayBetweenSounds + ZombieNoises.length);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    
}
