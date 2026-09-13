using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private HealthBehavior Health;

    [SerializeField]
    private Slider Slider;


    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Slider.maxValue = Health.GetMaxHealth();
        Slider.value = Health.GetHealth();
    }
}
