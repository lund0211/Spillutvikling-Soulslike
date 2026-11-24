using UnityEngine;
using Unity.Cinemachine;
using System;

public class CameraSensitivityBehavior : MonoBehaviour
{
    public static CameraSensitivityBehavior Instance;
    public float CameraSensitivity = 10;
    private CinemachineInputAxisController axisController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        axisController = GetComponent<CinemachineInputAxisController>();
    }

    // Update is called once per frame
    void Update()
    {
        axisController.Controllers[0].Input.Gain = CameraSensitivity;
        axisController.Controllers[1].Input.Gain = -CameraSensitivity;
    }
}
