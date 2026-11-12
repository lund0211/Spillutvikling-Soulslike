using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NormalEnemyBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    NavMeshAgent Agent;

    Animator animator;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject HitboxSword;

    [SerializeField]
    GameObject HitboxLeg;

    HealthBehavior Health;


    bool PlayerBeingChased = false;

    float MovementSpeed;

    bool CanAttack = true;

    public enum AttackType
    {
        SwordAttack,
        KickAttack

    }

    AttackType CurrentAttack;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Health = GetComponent<HealthBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementSpeed = Agent.velocity.magnitude;
        animator.SetFloat("Speed", MovementSpeed);


        if (PlayerBeingChased && CanAttack)
        {
            Agent.destination = Player.transform.position;
        }

        if(Vector3.Distance(Agent.transform.position, Player.transform.position) < 2f && CanAttack)
        {
            CanAttack = false;
            CurrentAttack = (AttackType)Random.Range(0, 2);
            switch(CurrentAttack)
            {
                case AttackType.SwordAttack:
                    animator.SetTrigger("Attack Sword");
                    break;
                case AttackType.KickAttack:
                    animator.SetTrigger("Attack Kick");
                    break;
                }
            StartCoroutine(AttackCooldownTimer());
        }

        if(Health.CurrentHealth <= 0 )
        {
            animator.SetBool("Dead", true);
            DisableHitboxSword();
        }
        else
        {
            animator.SetBool("Dead", false);
        }
    }

    private IEnumerator AttackCooldownTimer()
    {
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }

    public void EnablePlayerFollow()
    {
        PlayerBeingChased = true;
    }

    public void EnableHitboxSword()
    {
        HitboxSword.SetActive(true);
    }

    public void DisableHitboxSword()
    {
        HitboxSword.SetActive(false);
    }

    public void EnableHitboxLeg()
    {
        HitboxLeg.SetActive(true);
    }

    public void DisableHitboxLeg()
    {
        HitboxLeg.SetActive(false);
    }

    public void ResetChase()
    {
        PlayerBeingChased = false;
    }
}
