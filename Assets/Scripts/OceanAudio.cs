using System;
using System.Collections;
using UnityEngine;

public class OceanAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AudioSource audioSource;
    [SerializeField]
    AudioClip oceanSounds;

    float volume;
    float volumeMultiplier = 0.2f;
    void Start()
    {
        volume = AudioManager.Instance.masterVolume * volumeMultiplier;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayOceanSounds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayOceanSounds()
    {
        while (true)
        {
            audioSource.PlayOneShot(oceanSounds, volume);
            yield return new WaitForSeconds(oceanSounds.length);
        }
    }
}
