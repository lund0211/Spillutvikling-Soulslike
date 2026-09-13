using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    float Damage = 10;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Other)
    {
        Debug.Log("You have been hit");
        Other.GetComponent<HealthBehavior>().AdjustHealthBy(-Damage);
    }
}
