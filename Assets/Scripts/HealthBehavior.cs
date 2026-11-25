
using System;

using UnityEngine;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth = 100;

    [SerializeField]
    public float CurrentHealth;

    public float RespawnHealth;

    [SerializeField]
    private DeathScreenManager deathScreenManager;

    [SerializeField]
    private Slider healthSlider;

    public bool Dead = false;

    public event Action Damaged;




    void Start()
    {
        CurrentHealth = MaxHealth;

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = CurrentHealth;
        }

        RespawnHealth = MaxHealth;
    }

    public void AdjustHealthBy(float Amount)
    {
        if (CurrentHealth <= 0 && Amount < 0)
        {
            return;
        }

        CurrentHealth += Amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("You have died");
            Dead = true;

            if (deathScreenManager != null)
            {
                deathScreenManager.ShowDeathScreen();
            }
        }

        if (Amount < 0)
        {
            Damaged?.Invoke();

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

    public void ReduceRespawnHealth(float amount)
    {
        RespawnHealth = MaxHealth - amount;
    }
}
