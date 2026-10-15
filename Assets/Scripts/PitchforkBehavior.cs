using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PitchforkBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rb;
    [SerializeField]
    Transform Player;
    [SerializeField]
    Transform Witch;
    [SerializeField]
    Transform WitchHand;


    Vector3 Direction;

    [SerializeField]
    float PitchforkSpeed = 20.0f;
    [SerializeField]
    float PitchforkFlightTime = 1.0f;

    public bool HasReturned = true;

    Vector3 PitchforkStartPosition;
    Vector3 LocalPitchforkPosition;
    Quaternion LocalPitchforkRotation;
    float AimHeight = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LocalPitchforkPosition = transform.localPosition;
        LocalPitchforkRotation = transform.localRotation;
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
        PitchforkStartPosition = transform.position;
        transform.SetParent(null);
        rb.isKinematic = false;

        float YDistance = (Player.position.y + AimHeight) - transform.position.y;
        float XZDistance = Vector3.Distance(new Vector3(Player.position.x, 0, Player.position.z), new Vector3(transform.position.x, 0, transform.position.z));
        float Ratio = YDistance / XZDistance;
        Direction = new Vector3(Witch.forward.x, Ratio, Witch.forward.z);




        Direction.Normalize();
        transform.rotation = Quaternion.LookRotation(Direction);

        rb.linearVelocity = Direction * PitchforkSpeed;
        yield return new WaitForSeconds(PitchforkFlightTime);
        rb.isKinematic = false;

        rb.linearVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        while (Vector3.Distance(transform.position, PitchforkStartPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PitchforkStartPosition, PitchforkSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = PitchforkStartPosition;

        transform.SetParent(WitchHand);
        transform.localPosition = LocalPitchforkPosition;
        transform.localRotation = LocalPitchforkRotation;
        HasReturned = true;
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (!HasReturned) { 
            Debug.Log("Pitchfork hit the ground");
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
        }
    }
}