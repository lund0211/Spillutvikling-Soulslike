using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private HealthBehavior Health;

    [SerializeField]
    private Image FillImage;

    float maxValue = 0.889f;
    float minValue = 0.211f;


    void Start()
    {
        Health = GetComponent<HealthBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        //Slider.maxValue = Health.GetMaxHealth();
        float HealthPercent = Mathf.Clamp01(Health.GetHealth() / Health.GetMaxHealth());
        FillImage.fillAmount = Mathf.Lerp(minValue, maxValue, HealthPercent);
        
    }
}
