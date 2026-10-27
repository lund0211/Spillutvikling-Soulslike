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

    public void EnableHitboxPitchfork()
    {
        Witch.EnableHitboxPitchfork();
    }

    public void DisableHitboxPitchfork()
    {
        Witch.DisableHitboxPitchfork();
    }

    public void EnableHitboxBroom()
    {
        Witch.EnableHitboxBroom();
    }

    public void DisableHitboxBroom()
    {
        Witch.DisableHitboxBroom();
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