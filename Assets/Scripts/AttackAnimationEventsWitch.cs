using UnityEngine;

public class AttackAnimationEvents : MonoBehaviour
{
    PlagueWitchBehavior Witch;

    [SerializeField]
    PitchforkBehavior Pitchfork;


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
    public void ThrowPitchfork()
    {
        Pitchfork.ThrowPitchfork();
    }

    public void LockRotationFunc()
    {
        Witch.LockRotation = true;
    }

    public void UnlockRotationFunc()
    {
        Witch.LockRotation = false;
    }
}