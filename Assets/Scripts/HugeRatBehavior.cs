
using UnityEngine;
using UnityEngine.AI;

public class HugeRatBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 MovementDirection;

    Animator animator;

    NavMeshAgent agent;

    bool HasBeenTriggered = false;

    int encounterCount = 0;

    void Start()
    {

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (encounterCount == 1)
        {
            if (!HasBeenTriggered || agent.pathPending || !agent.hasPath)
            {
                return;
            }

            if (agent.remainingDistance <= 2)
            {
                Teleport();
            }
        }
       
    }

    public void EnableMovement()
    {
        agent.SetDestination(new Vector3(-93.1900024f, 1.13999999f, 34.2299995f));
        HasBeenTriggered = true;
        animator.SetTrigger("Walking");
        encounterCount++;
    }
    private void Teleport()
    {
        agent.ResetPath();
        agent.Warp(new Vector3(57.1100006f, 23.5400009f, 386.660004f));
        transform.rotation = Quaternion.Euler(0, 199.504822f, 0);
        animator.SetTrigger("Idle");
        encounterCount++;
    }
}
