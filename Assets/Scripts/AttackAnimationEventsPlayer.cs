using UnityEngine;

public class AttackAnimationEventsPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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


}
