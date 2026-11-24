using UnityEngine;
using TMPro;

public class PotionCountDisplayBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Player player;
    [SerializeField]
    TMP_Text text;

    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.GetHealthPotionCount().ToString();
    }
}
