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
    GameObject Hitbox;

    bool PlayerBeingChased = false;

    float MovementSpeed;

    bool CanAttack = true;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("Attack Sword");
            StartCoroutine(AttackCooldownTimer());
        }
    }

    private IEnumerator AttackCooldownTimer()
    {
        yield return new WaitForSeconds(4f);
        CanAttack = true;
    }

    public void EnablePlayerFollow()
    {
        PlayerBeingChased = true;
    }

    public void EnableHitboxSword()
    {
        Hitbox.SetActive(true);
    }

    public void DisableHitboxSword()
    {
        Hitbox.SetActive(false);
    }

    public void ResetChase()
    {
        PlayerBeingChased = false;
    }
}
