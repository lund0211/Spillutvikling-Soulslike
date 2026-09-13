using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    [SerializeField]
    private float JumpHeight = 200f;

    [SerializeField]
    private float MovementSpeed = 10f;

    [SerializeField]
    private Transform CameraTransform;

    private Vector2 MoveInput;
    private Vector3 Movement;

    bool IsDodging = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    /* Unused test function
    public void OnJump(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            Debug.Log("Player has jumped");
            rb.AddForce(new Vector3(0, JumpHeight, 0));
        }

    }
    */

    public void OnMovement(InputAction.CallbackContext Context)
    {
        MoveInput = Context.ReadValue<Vector2>();

    }

    public void OnDodge(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            IsDodging = true;
            Debug.Log("You are dodging");
        }
    }

    public bool GetIsDodging()
    {
        return IsDodging;
    }

    void MovementFunc()
    {

        Vector3 Forward = Camera.main.transform.forward;
        Vector3 Right = Camera.main.transform.right;

        Forward.y = 0;
        Right.y = 0;

        Forward.Normalize();
        Right.Normalize();
        

        Movement = MoveInput.y * Forward + MoveInput.x * Right;
        rb.linearVelocity = new Vector3(Movement.x* MovementSpeed, rb.linearVelocity.y, Movement.z* MovementSpeed);

        if(Movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Movement);
        }

    }



    void Update()
    {
        MovementFunc();
    }
}
