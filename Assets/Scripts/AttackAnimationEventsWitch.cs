using UnityEngine;

public class AttackAnimationEvents : MonoBehaviour
{
    PlagueWitchBehavior Witch;

    void Start()
    {
        Witch = GetComponentInParent<PlagueWitchBehavior>();
    }

    public void EnableHitbox()
    {
        Witch.EnableHitbox();
    }

    public void DisableHitbox()
    {
        Witch.DisableHitbox();
    }
}