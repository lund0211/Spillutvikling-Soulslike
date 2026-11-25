
using UnityEngine;
using UnityEngine.UI;

public class HagHealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthBehavior hagHealth;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private GameObject healthBarVisuals;

    void Start()
    {
        if (hagHealth == null || healthSlider == null)
            return;

        healthSlider.minValue = 0;
        healthSlider.maxValue = hagHealth.GetMaxHealth();
        healthSlider.value = hagHealth.GetHealth();

        if (healthBarVisuals != null)
            healthBarVisuals.SetActive(false);
    }

    void Update()
    {
        if (hagHealth == null || healthSlider == null)
            return;

        healthSlider.value = hagHealth.GetHealth();
    }

    public void ShowHealthBar()
    {
        if (healthBarVisuals != null)
            healthBarVisuals.SetActive(true);
    }
}
