using System.Collections;
using UnityEngine;

public class PitchforkBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rb;
    [SerializeField]
    Transform Player;
    Vector3 PlayerPosition;
    Vector3 Direction;

    [SerializeField]
    float PitchforkSpeed = 20.0f;
    [SerializeField]
    float PitchForkFlightTime = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ThrowPitchfork();
    }

    // Update is called once per frame
    void Update()
    {
     
    }


    public void ThrowPitchfork()
    {
        StartCoroutine(FlightTime());
    }

    private IEnumerator FlightTime()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        PlayerPosition = Player.position;
        Direction = PlayerPosition - rb.position;
        Direction.y = 0;
        Direction.Normalize();
        rb.linearVelocity = Direction * PitchforkSpeed;
        yield return new WaitForSeconds(PitchForkFlightTime);
        rb.linearVelocity = -Direction * PitchforkSpeed;
        yield return new WaitForSeconds(PitchForkFlightTime);
        rb.linearVelocity = Vector3.zero;
        Transform Hand = GameObject.Find("mixamorig:LeftHand").transform;
        transform.SetParent(Hand);
        rb.isKinematic = true;
    }
}
