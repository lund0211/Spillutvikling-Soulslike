using System;
using UnityEngine;
using System.Collections;

public class WitchAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AudioSource audioSource;
    [SerializeField]
    AudioClip WitchAggroedSound;
    [SerializeField]
    AudioClip WitchThrowGruntSound;
    [SerializeField]
    AudioClip WitchVeryAngrySound;

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

    public void PlayWitchAggroedSound()
    {
        audioSource.PlayOneShot(WitchAggroedSound, volume);
    }
    public void PlayWitchThrowGruntSound()
    {
        StartCoroutine(DelayPlayedSound(audioSource, WitchThrowGruntSound, 0.8f, 1f));
    }

    public void PlayWitchVeryAngrySound()
    {
        StartCoroutine(DelayPlayedSound(audioSource, WitchVeryAngrySound, 0.5f, 1f));
    }

    public IEnumerator DelayPlayedSound(AudioSource audioSource, AudioClip clip, float delay, float audioVolumeMultiplier)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip, volume * audioVolumeMultiplier);
    }
}
