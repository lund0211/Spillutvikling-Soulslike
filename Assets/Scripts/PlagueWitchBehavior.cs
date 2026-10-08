using System.Collections;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using static PlagueWitchBehavior;

public class PlagueWitchBehavior : MonoBehaviour
{

    //[SerializeField]
    //Transform PitchforkTransform;

    Rigidbody rb;

    [SerializeField]
    GameObject DamageHitboxPitchfork;
    [SerializeField]
    PitchforkBehavior Pitchfork;

    float AttackCooldown = 1f;
    bool CanAttack = true;

    Animator animator;

    [SerializeField]
    Transform PlayerTarget;

    NavMeshAgent Agent;
    float Distance;

    Vector3 PlayerDirection;

    [SerializeField]
    float TurnSpeed = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum AttackType
    {
        Thrust,
        Swipe

    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack)
        {
            StartCoroutine(AttackCooldownTimer());
        }
        if(rb.linearVelocity == Vector3.zero)
        {
            PlayerDirection = PlayerTarget.position - transform.position;
            PlayerDirection.y = 0f;
            Quaternion PlayerRotation = Quaternion.LookRotation(PlayerDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, PlayerRotation, TurnSpeed * Time.deltaTime);
        }

        Distance = Vector3.Distance(Agent.transform.position, PlayerTarget.position);
        Agent.destination = PlayerTarget.position;
    }

    private IEnumerator AttackCooldownTimer()
    {
        CanAttack = false;
        AttackType CurrentAttack = (AttackType)Random.Range(0, 2);
        switch (CurrentAttack)
        {
            case AttackType.Thrust:
                animator.SetTrigger("Thrust Attack");
                Pitchfork.ThrowPitchfork();
                break;
            case AttackType.Swipe:
                animator.SetTrigger("Swipe Attack");
                break;
        }

        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) {
            Agent.velocity = new Vector3(0, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    public void EnableHitbox()
    {
        DamageHitboxPitchfork.SetActive(true);
    }
    public void DisableHitbox()
    {
        DamageHitboxPitchfork.SetActive(false);
    }

}
