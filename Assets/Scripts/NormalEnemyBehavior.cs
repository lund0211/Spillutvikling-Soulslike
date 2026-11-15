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

    Vector3 PlayerDirection;

    float TurnSpeed = 10f;

    public enum AttackType
    {
        SwordAttack,
        KickAttack

    }

    AttackType CurrentAttack;

    bool IsDead = false;

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


        if (PlayerBeingChased && CanAttack && !IsDead)
        {
            Agent.destination = Player.transform.position;
            RotateEnemy();
        }

        //Agent.updateRotation = PlayerBeingChased;

        //PlayerDirection = (Player.transform.position - transform.position).normalized;

        if (Vector3.Distance(Agent.transform.position, Player.transform.position) < 1.5f && CanAttack && Vector3.Angle(transform.forward, PlayerDirection) <= 30f)
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
            DisableHitboxLeg();
            IsDead = true;
        }
        else
        {
            animator.SetBool("Dead", false);
            IsDead = false; 
        }
    }

    private IEnumerator AttackCooldownTimer()
    {
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }
    private void RotateEnemy()
    {
        PlayerDirection = Player.transform.position - transform.position;
        PlayerDirection.y = 0f;
        PlayerDirection.Normalize();
        Quaternion PlayerRotation = Quaternion.LookRotation(PlayerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, PlayerRotation, TurnSpeed * Time.deltaTime);
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
