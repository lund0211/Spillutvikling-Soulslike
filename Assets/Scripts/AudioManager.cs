using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static AudioManager Instance;
    [SerializeField]
    AudioSource PlayerAudioSource;
    [SerializeField]
    AudioClip SwordSound;
    [SerializeField]
    AudioClip Walking;
    [SerializeField]
    AudioClip DyingSound;
    [SerializeField]
    AudioClip HurtSound;

    bool PlayZomebieSounds = false;

    public float masterVolume = 1f;


    void Start()
    {

    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySwordSound(float delay, float audioVolumeMultiplier)
    {
        StartCoroutine(DelayPlayedSound(PlayerAudioSource, SwordSound, delay, audioVolumeMultiplier));
    }
    public void PlayWalkSound(float delay, float audioVolumeMultiplier)
    {
        StartCoroutine(DelayPlayedSound(PlayerAudioSource, Walking, delay, audioVolumeMultiplier));
    }
    public void PlayDyingSound(float delay, float audioVolumeMultiplier)
    {
        StartCoroutine(DelayPlayedSound(PlayerAudioSource, DyingSound, delay, audioVolumeMultiplier));
    }

    public void PlayHurtSound(float delay, float audioVolumeMultiplier)
    {
        StartCoroutine(DelayPlayedSound(PlayerAudioSource, HurtSound, delay, audioVolumeMultiplier));
    }

    public IEnumerator DelayPlayedSound(AudioSource audioSource, AudioClip clip, float delay, float audioVolumeMultiplier)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip, masterVolume * audioVolumeMultiplier);
    }


}