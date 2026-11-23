
using UnityEngine;

public class BossHealthBarVisibility : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform witch;
    public GameObject bossHealthBar;

    [Header("Settings")]
    public float showDistance = 15f;

    void Start()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || witch == null || bossHealthBar == null)
        {
            return;
        }

        float distance = Vector3.Distance(
            player.position,
            witch.position
        );

        if (distance <= showDistance)
        {
            bossHealthBar.SetActive(true);
        }
        else
        {
            bossHealthBar.SetActive(false);
        }
    }
}
