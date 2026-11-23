using UnityEngine;
using UnityEngine.UI;

public class StaminabarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Image FillImage;

    Player player;

    float maxValue = 0.928f;
    float minValue = 0.079f;


    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float StaminaPercent = Mathf.Clamp01(player.GetCurrentStamina() / player.GetMaxStamina());
        FillImage.fillAmount = Mathf.Lerp(minValue, maxValue, StaminaPercent);

    }
}
