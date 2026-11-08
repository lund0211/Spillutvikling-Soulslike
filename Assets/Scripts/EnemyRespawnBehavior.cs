using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRespawnBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    bool CanRespawn = true;
    bool EnemyIsDead = false;

    NormalEnemyBehavior EnemyBehavior;

    private Vector3 StartPosition;
    private Quaternion StartRotation;

    private HealthBehavior Health;
    private NavMeshAgent agent;

    void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.rotation;

        Health = GetComponent<HealthBehavior>();
        agent = GetComponent<NavMeshAgent>();

        EnemyBehavior = GetComponent<NormalEnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        if(EnemyIsDead && !CanRespawn)
        {
            return;
        }
        agent.ResetPath();
        transform.SetPositionAndRotation(StartPosition, StartRotation);
        agent.Warp(StartPosition);
        Health.CurrentHealth = Health.GetMaxHealth();
        if (EnemyBehavior != null)
        {
            EnemyBehavior.ResetChase();
        }
        EnemyIsDead = false;
    }

    public void IsDead()
    {
        EnemyIsDead = true;
    }
}
