using System;
using UnityEngine;

public class WitchAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] WitchSounds;
    [SerializeField]
    AudioClip WitchAggroedSound;

    float volume;
    float volumeMultiplier = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume = AudioManager.Instance.masterVolume * volumeMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWitchSound()
    {
        int randomIndex = UnityEngine.Random.Range(0, WitchSounds.Length);
        AudioClip randomSound = WitchSounds[randomIndex];
        audioSource.PlayOneShot(randomSound, volume);
    }
    public void PlayWitchAggroedSound()
    {
        audioSource.PlayOneShot(WitchAggroedSound, volume);
    }
}
