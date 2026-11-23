using UnityEngine;

public class HugeRatTriggerBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    HugeRatBehavior hugeRatBehavior;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //HugeRatBehavior hugeRatBehavior = GetComponentInParent<HugeRatBehavior>();
            if (hugeRatBehavior != null)
            {
                hugeRatBehavior.EnableMovement();
            }
        }
    }
}
