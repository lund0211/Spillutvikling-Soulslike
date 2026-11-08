using UnityEngine;
using UnityEngine.AI;

public class VisionConeBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject ConeSizeCollider;
    NormalEnemyBehavior Enemy;

    void Start()
    {
        Enemy = GetComponentInParent<NormalEnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {

        Debug.Log("OnTriggerStay");
        if (other.CompareTag("Player"))
        {
            Vector3 EnemyPos = transform.position;
            Vector3 EnemyVector = transform.forward;
            Vector3 PlayerPos = other.transform.position;
            Vector3 PlayerVector = PlayerPos - EnemyPos;

            float Angle = Vector3.Angle(EnemyVector, PlayerVector);
            bool IsInCone = Angle < 45f;

            Debug.Log("Angle: " + Angle);
            Debug.Log("IsInCone: " + IsInCone);

            bool IsPlayerClose = Vector3.Distance(EnemyPos, PlayerPos) < 5f;

            Debug.Log("IsPlayerClose: " + IsPlayerClose);

            if(IsInCone || IsPlayerClose)
            {
                Enemy.EnablePlayerFollow();
                Debug.Log("Player detected");
            }

        }
    }
}
