using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Collections;


public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    //[SerializeField]
    //private float JumpHeight = 200f;

    [SerializeField]
    private float MovementSpeed = 3f;

    Animator animator;

    private Vector2 MoveInput;
    private Vector3 Movement;

    bool IsDodging = false;
    float IFramesTime = 0.5f;
    float DodgeCooldown = 0.5f;
    bool CanDodge = true;

    float DodgeSpeedTime = 0.8f;
    float RemainingDodgeSpeedTime = 0;
    float DodgeSpeed = 8f;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    
    /* Unused test function, can be implemented if we want
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


        if (Context.performed && CanDodge)
        {
            RemainingDodgeSpeedTime = DodgeSpeedTime;
            StartCoroutine(DodgeTimer());
        }
    }

    private IEnumerator DodgeTimer()
    {

        animator.SetTrigger("Dodge");
        IsDodging = true;
        CanDodge = false;

        Debug.Log("You are dodging");

        yield return new WaitForSeconds(IFramesTime);

        IsDodging = false;

        yield return new WaitForSeconds(DodgeCooldown);

        CanDodge = true;

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

        if(RemainingDodgeSpeedTime > 0)
        {
            rb.linearVelocity = new Vector3(Movement.x * DodgeSpeed, rb.linearVelocity.y, Movement.z * DodgeSpeed);
            RemainingDodgeSpeedTime -= Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = new Vector3(Movement.x * MovementSpeed, rb.linearVelocity.y, Movement.z * MovementSpeed);
        }


        //rb.linearVelocity = new Vector3(Movement.x* MovementSpeed, rb.linearVelocity.y, Movement.z* MovementSpeed);

        if(Movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Movement);
        }

    }



    void Update()
    {
        MovementFunc();
        animator.SetFloat("Speed", Movement.magnitude);
    }
}
