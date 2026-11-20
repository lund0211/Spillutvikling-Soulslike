using UnityEngine;
using UnityEngine.InputSystem;

public class AltarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    PlagueWitchBehavior plagueWitchBehavior;
    [SerializeField]
    GameObject fire;
    
    bool hasBeenActivated = false;

    bool PlayerNearby = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerNearby && Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame && !hasBeenActivated)
        {
            fire.SetActive(true);
            plagueWitchBehavior.WeakenWitch();
            hasBeenActivated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNearby = false;
        }
    }
}
