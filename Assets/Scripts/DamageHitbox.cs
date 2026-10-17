using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    float Damage = 10;
    
    public GameObject owner;


    void Start()
    {
        owner = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Other)
    {

        if (owner != null && Other.transform.root == owner.transform.root)
            return;

        //Debug.Log("You have been hit");
        Player Player = Other.GetComponent<Player>();
        HealthBehavior Health = Other.GetComponent<HealthBehavior>();
      
          
        if(Player != null)
        {
            if(Player != null && !Player.GetIsDodging())
            {
                Health.AdjustHealthBy(-Damage);
            }
        }
        else
        {  
            if(Health != null) { 
                Health.AdjustHealthBy(-Damage); 
            }
        }
    }

}
