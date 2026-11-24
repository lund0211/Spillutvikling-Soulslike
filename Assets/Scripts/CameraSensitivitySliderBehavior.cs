using UnityEngine;
using UnityEngine.UI;

public class CameraSensitivitySliderBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Slider SensitivitySlider;
    //[SerializeField]
    //private CameraSensitivityBehavior CameraSensitivity;

    [SerializeField]
    private float MinSensitivity = 1;
    [SerializeField]
    private float MaxSensitivity = 30;
    [SerializeField]
    private float DefaultSensitivity = 10;

    
    void Start()
    {
        SensitivitySlider.minValue = MinSensitivity;
        SensitivitySlider.maxValue = MaxSensitivity;
        SensitivitySlider.SetValueWithoutNotify(CameraSensitivityBehavior.Instance.CameraSensitivity);
        SensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
        //SensitivitySlider.value = DefaultSensitivity;
        UpdateSensitivity(SensitivitySlider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateSensitivity(float value)
    {
        CameraSensitivityBehavior.Instance.CameraSensitivity = value;
    }
}
