using System;
using UnityEngine;

public class OnCastleEnterBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    PlagueWitchBehavior witch;

    [SerializeField]
    private HagHealthBar hagHealthBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            witch.GainAggro();


            // Show the Hag's health bar
            if (hagHealthBar != null)
            {
                hagHealthBar.ShowHealthBar();
            }
        }
    }
}
