using UnityEngine;
using System.Collections;  

public class CrowAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AudioSource CrowAudioSource;
    [SerializeField]
    AudioClip[] CrowSounds;

    [SerializeField]
    float MinDelay = 10f;
    [SerializeField]
    float MaxDelay = 20f;

    float volume;
    float volumeMultiplier = 1f;
    void Start()
    {
        StartCoroutine(PlayCrowSounds());
        CrowAudioSource = GetComponent<AudioSource>();
        volume = AudioManager.Instance.masterVolume * volumeMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayCrowSounds()
    {
        while (true)
        {
            float delay = Random.Range(MinDelay, MaxDelay);
            yield return new WaitForSeconds(delay);
            int randomIndex = Random.Range(0, CrowSounds.Length);
            AudioClip randomSound = CrowSounds[randomIndex];
            CrowAudioSource.PlayOneShot(randomSound, volume);
        }
    }
}
