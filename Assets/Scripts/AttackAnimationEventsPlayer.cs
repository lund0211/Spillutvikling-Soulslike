using UnityEngine;

public class AttackAnimationEventsPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float lastWalkSoundTime = 0f;

    Player PlayerComponent;
    void Start()
    {
        PlayerComponent = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void EnableHitbox()
    {
        PlayerComponent.EnableHitbox();
    }
    public void DisableHitbox()
    {
        PlayerComponent.DisableHitbox();
    }

    public void DisableMovementFunc()
    {
        PlayerComponent.DisableMovementFunc();
    }

    public void EnableMovementFunc()
    {
        PlayerComponent.EnableMovementFunc();
    }

    public void ShowPotionInHand()
    {
        PlayerComponent.ShowPotionInHand();
    }

    public void HidePotionInHand()
    {
        PlayerComponent.HidePotionInHand();
    }
    public void PlayWalkSound()
    {
       
        AudioManager.Instance.PlayWalkSound(0f, 0.5f);

       

    }


}
