using UnityEngine;
using System.Collections;  

public class CrowAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AudioSource CrowAudioSource;
    [SerializeField]
    AudioClip[] CrowSounds;

    [SerializeField]
    float MinDelay = 5f;
    [SerializeField]
    float MaxDelay = 15f;
    void Start()
    {
        StartCoroutine(PlayCrowSounds());
        CrowAudioSource = GetComponent<AudioSource>();
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
            CrowAudioSource.PlayOneShot(randomSound);
        }
    }
}
