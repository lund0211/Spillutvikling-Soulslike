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

    [SerializeField]
    Transform WitchMesh;

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

    AttackType CurrentAttack;

    public bool LockRotation = false;


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
        if (!LockRotation)
        {
            RotateWitch();
        }
        if ((CanAttack == true) && (Vector3.Dot(transform.forward, PlayerDirection) >= 0.99))
        {
            CanAttack = false;
            CurrentAttack = (AttackType)Random.Range(0, 2);
            StartCoroutine(AttackCooldownTimer());
        }

        Distance = Vector3.Distance(Agent.transform.position, PlayerTarget.position);
        Agent.destination = PlayerTarget.position;
        animator.SetFloat("Speed", Agent.velocity.magnitude);
    }

    private IEnumerator AttackCooldownTimer()
    {

        if (Distance < 3f)
        {
            CurrentAttack = (AttackType)Random.Range(0, 2);
        } else
        {
            CurrentAttack = AttackType.Thrust;
        }

        switch (CurrentAttack)
        {
            case AttackType.Thrust:
                Pitchfork.HasReturned = false;
                Agent.isStopped = true;
                animator.SetTrigger("Thrust Attack");

                while (!Pitchfork.HasReturned)
                {
                    yield return null;
                }
                LockRotation = false;
                Agent.isStopped = false;
                animator.SetTrigger("ThrowDone");
                DisableHitbox();
                break;
            case AttackType.Swipe:
                Agent.isStopped = true;
                animator.SetTrigger("Swipe Attack");
                while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                {
                    yield return null;
                }
                Debug.Log("Swipe Attack Finished");
                Agent.isStopped = false;
                LockRotation = false;
                break;
        }

        //while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f ) {
            //Agent.velocity = new Vector3(0, 0, 0);
            //Agent.isStopped = true;
            //yield return null;
        //}
        Agent.isStopped = false;
        yield return new WaitForSeconds(AttackCooldown);


        //To make sure witch doesn't gradually lose her position over time
        WitchMesh.localPosition = Vector3.zero;
        WitchMesh.localRotation = Quaternion.identity;

        Debug.Log("Witch can Attack again");
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


    private void RotateWitch()
    {
        PlayerDirection = PlayerTarget.position - transform.position;
        PlayerDirection.y = 0f;
        PlayerDirection.Normalize();
        Quaternion PlayerRotation = Quaternion.LookRotation(PlayerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, PlayerRotation, TurnSpeed * Time.deltaTime);
    }

}
