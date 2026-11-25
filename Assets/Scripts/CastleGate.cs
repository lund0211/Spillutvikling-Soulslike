using UnityEngine;

public class CastleGate : MonoBehaviour
{
    public Transform leftGate;
    public Transform rightGate;

    public float leftClosedY = -150f;
    public float rightClosedY = -20f;
    public float closeSpeed = 90f;

    Quaternion leftOpen;
    Quaternion rightOpen;
    Quaternion leftClosed;
    Quaternion rightClosed;

    Quaternion leftTarget;
    Quaternion rightTarget;

void Start()
{
    // Start at open (starting) position
    leftOpen = leftGate.localRotation;
    rightOpen = rightGate.localRotation;

    Vector3 leftAngles = leftGate.localEulerAngles;
    Vector3 rightAngles = rightGate.localEulerAngles;

    leftClosed = Quaternion.Euler(leftAngles.x, leftClosedY, leftAngles.z);
    rightClosed = Quaternion.Euler(rightAngles.x, rightClosedY, rightAngles.z);

    leftTarget = leftOpen;
    rightTarget = rightOpen;
}

void Update()
{
    float step = closeSpeed * Time.deltaTime;
    leftGate.localRotation = Quaternion.RotateTowards(leftGate.localRotation, leftTarget, step);
    rightGate.localRotation = Quaternion.RotateTowards(rightGate.localRotation, rightTarget, step);
}

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        leftTarget = leftClosed;
        rightTarget = rightClosed;
    }
}

// When player dies
public void ResetGate()
{
    leftTarget = leftOpen;
    rightTarget = rightOpen;
}
}