using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private float MaxHealth = 100;
    [SerializeField]
    public float CurrentHealth;




    void Start()
    {
        CurrentHealth = MaxHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AdjustHealthBy(float Amount)
    {
        CurrentHealth += Amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            Debug.Log("You have died");
        }

    }

    public float GetHealth()
    {
        return CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

}
