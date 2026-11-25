using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlagueWitchBehavior : MonoBehaviour
{

    //[SerializeField]
    //Transform PitchforkTransform;

    Rigidbody rb;

    [SerializeField]
    GameObject DamageHitboxPitchfork;

    [SerializeField]
    GameObject DamageHitboxBroom;

    [SerializeField]
    PitchforkBehavior Pitchfork;

    [SerializeField]
    Transform WitchMesh;

    float AttackCooldown = 1f;
    bool CanAttack = true;

    Animator animator;

    [SerializeField]
    Player PlayerTarget;

    NavMeshAgent Agent;
    float Distance;

    Vector3 PlayerDirection;

    [SerializeField]
    float TurnSpeed = 0.1f;

    AttackType CurrentAttack;

    public bool LockRotation = false;

    HealthBehavior health;

    EnemyRespawnBehavior RespawnBehavior;

    bool PlayedAggroSound = false;

    WitchAudio WitchAudio;

    bool Aggro = false;

    bool IsDead = false;
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
        health = GetComponent<HealthBehavior>();
        RespawnBehavior = GetComponent<EnemyRespawnBehavior>();
        WitchAudio = GetComponent<WitchAudio>();
    }

    // Update is called once per frame
    void Update()
    {

        Distance = Vector3.Distance(Agent.transform.position, PlayerTarget.transform.position);
        //if(Distance > 50)
        //{
            //LoseAggro();
            //return;
        //}
        if (Aggro == true)
        {
            Agent.destination = PlayerTarget.transform.position;

            if (!PlayedAggroSound)
            {
                WitchAudio.PlayWitchAggroedSound();
                PlayedAggroSound = true;
            }
            if (!LockRotation)
            {
                RotateWitch();
            }
            if ((CanAttack == true) && (Vector3.Dot(transform.forward, PlayerDirection) >= 0.99) && (Distance < 15f))
            {
                CanAttack = false;
                StartCoroutine(AttackCooldownTimer());
            }

        }

        HealthBehavior PlayerHealth = PlayerTarget.GetComponent<HealthBehavior>();
        if (PlayerHealth.GetHealth() <= 0)
        {
            LoseAggro();
        }
        
        
        animator.SetFloat("Speed", Agent.velocity.magnitude);

        if (health != null && health.Dead)
        {
            Agent.isStopped = true;
            animator.SetBool("Dead", true);
            RespawnBehavior.IsDead();
            DisableHitboxBroom();
            DisableHitboxPitchfork();
            Aggro = false;
            EndGame();
            return;
        }
    }

    private IEnumerator AttackCooldownTimer()
    {
        if (Distance < 3f)
        {
            CurrentAttack = (AttackType)Random.Range(0, 2);
        }
        else if(Distance < 15f)
        {
            CurrentAttack = AttackType.Thrust;
        }
        else
        {
            yield break;
        }

        switch (CurrentAttack)
        {
            case AttackType.Thrust:
                Pitchfork.HasReturned = false;
                Agent.isStopped = true;
                animator.SetTrigger("Thrust Attack");
                WitchAudio.PlayWitchThrowGruntSound();

                while (!Pitchfork.HasReturned)
                {
                    yield return null;
                }
                LockRotation = false;
                Agent.isStopped = false;
                animator.SetTrigger("ThrowDone");
                DisableHitboxPitchfork();
                break;
            case AttackType.Swipe:
                Agent.isStopped = true;
                animator.SetTrigger("Swipe Attack");
                WitchAudio.PlayWitchVeryAngrySound();
                while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                {
                    yield return null;
                }
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

        CanAttack = true;
    }

    public void EnableHitboxPitchfork()
    {
        DamageHitboxPitchfork.SetActive(true);
    }
    public void DisableHitboxPitchfork()
    {
        DamageHitboxPitchfork.SetActive(false);
    }

    public void EnableHitboxBroom()
    {
        DamageHitboxBroom.SetActive(true);
    }

    public void DisableHitboxBroom()
    {
        DamageHitboxBroom.SetActive(false);
    }


    private void RotateWitch()
    {
        PlayerDirection = PlayerTarget.transform.position - transform.position;
        PlayerDirection.y = 0f;
        PlayerDirection.Normalize();
        Quaternion PlayerRotation = Quaternion.LookRotation(PlayerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, PlayerRotation, TurnSpeed * Time.deltaTime);
    }

    public void LoseAggro()
    {
        Agent.isStopped = true;
        Agent.ResetPath();
        LockRotation = false;
        CanAttack = true;
        DisableHitboxPitchfork();
        DisableHitboxBroom();
        PlayedAggroSound = false;
        Aggro = false;
    }
    public void GainAggro()
    {
        Aggro = true;
    }

    public void WeakenWitch()
    {
        float damage = health.GetMaxHealth() * 0.2f;
        health.AdjustHealthBy(-damage);
        health.ReduceRespawnHealth(damage);
    }

    public void EndGame()
    {
        EndOfGameBehavior VictoryScreen = GetComponent<EndOfGameBehavior>();
        VictoryScreen.ShowVictoryScreen();
    }


}
