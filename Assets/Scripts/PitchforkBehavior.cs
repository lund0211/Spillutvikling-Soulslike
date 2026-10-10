using System.Collections;
using UnityEngine;

public class PitchforkBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rb;
    [SerializeField]
    Transform Player;
<<<<<<< Updated upstream
    Vector3 PlayerPosition;
=======
    [SerializeField]
    Transform Witch;
    [SerializeField]
    Transform WitchHand;
    

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        PlayerPosition = Player.position;
        Direction = PlayerPosition - rb.position;
        Direction.y = 0;
=======
        //Direction = transform.forward;
        //Direction = (Player.position - transform.position);
        //Direction = Witch.forward;
        //Direction = new Vector3(Witch.forward.x, Player.position.y - transform.position.y, Witch.forward.z);
        //Direction.y = 0;

        float YDistance = Player.position.y - transform.position.y;
        float XZDistance = Vector3.Distance(new Vector3(Player.position.x, 0, Player.position.z), new Vector3(transform.position.x, 0, transform.position.z));
        float Ratio = YDistance / XZDistance;
        Direction = new Vector3(Witch.forward.x, Ratio + AimHeight , Witch.forward.z);

>>>>>>> Stashed changes
        Direction.Normalize();
        rb.linearVelocity = Direction * PitchforkSpeed;
        yield return new WaitForSeconds(PitchForkFlightTime);
        rb.linearVelocity = -Direction * PitchforkSpeed;
        yield return new WaitForSeconds(PitchForkFlightTime);
        rb.linearVelocity = Vector3.zero;
<<<<<<< Updated upstream
        Transform Hand = GameObject.Find("mixamorig:LeftHand").transform;
        transform.SetParent(Hand);
        rb.isKinematic = true;
=======
        rb.isKinematic = true;

        while (Vector3.Distance(transform.position, PitchforkStartPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PitchforkStartPosition, PitchforkSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = PitchforkStartPosition;

        //rb.isKinematic = false;
        //rb.linearVelocity = Vector3.zero;
        //Transform Hand = GameObject.Find("mixamorig:RightHand").transform;
        transform.SetParent(WitchHand);
        transform.localPosition = LocalPitchforkPosition;
        transform.localRotation = LocalPitchforkRotation;
        HasReturned = true;
>>>>>>> Stashed changes
    }
}
