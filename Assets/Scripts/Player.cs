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
    GameObject WeaponHitBox;

    [SerializeField]
    private float MovementSpeed = 1f;
    [SerializeField]
    private float MovementRunSpeed = 10f;
    bool IsSprinting = false;

    Animator animator;

    private Vector2 MoveInput;
    private Vector3 Movement;

    bool IsDodging = false;
    [SerializeField]
    float IFramesTime = 0.5f;
    [SerializeField]
    float DodgeCooldown = 0.3f;
    bool CanDodge = true;

    [SerializeField]
    float DodgeSpeedTime = 0.4f;
    float RemainingDodgeSpeedTime = 0f;
    [SerializeField]
    float DodgeSpeed = 5f;
    Vector3 DodgeDirection;

    HealthBehavior Health;

    int HealthPotionCount = 3;


    bool IsAttacking = false;
    bool DisableMovement = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Health = GetComponent<HealthBehavior>();
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
            if (Movement != Vector3.zero)
            {
                DodgeDirection = Movement;
            }
            else
            {
                DodgeDirection = transform.forward;
            }
                RemainingDodgeSpeedTime = DodgeSpeedTime;
            StartCoroutine(DodgeTimer());
        }
    }

    public void OnSprint(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            IsSprinting = true;
            animator.SetBool("IsSprinting", true);
        }
        else if(Context.canceled)
        {
            IsSprinting = false;
            animator.SetBool("IsSprinting", false);
        }
        
    }

    public void OnAttack(InputAction.CallbackContext Context)
    {
        if (Context.started && !IsAttacking)
        {
            StartCoroutine(AttackTimer());
        }
    }

    public void OnHeal(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            if (Health != null)
            {
                if (HealthPotionCount > 0) { 
                    Health.AdjustHealthBy(50f);
                    HealthPotionCount--;
                }

            }
       
        }
    }

    private IEnumerator AttackTimer() { 
    {
        animator.SetTrigger("Attack"); 
        IsAttacking = true;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
        {
            yield return null;
        }
          
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
            IsAttacking = false;
            Debug.Log("Attack Finished");
        }
    }

    private IEnumerator DodgeTimer()
    {

        animator.SetTrigger("Dodge");
        IsDodging = true;
        CanDodge = false;

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
        if(IsAttacking == true || DisableMovement == true)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }
        Vector3 Forward = Camera.main.transform.forward;
        Vector3 Right = Camera.main.transform.right;

        Forward.y = 0;
        Right.y = 0;

        Forward.Normalize();
        Right.Normalize();
        

        Movement = MoveInput.y * Forward + MoveInput.x * Right;

        if(RemainingDodgeSpeedTime > 0)
        {
            rb.linearVelocity = new Vector3(DodgeDirection.x * DodgeSpeed, rb.linearVelocity.y, DodgeDirection.z * DodgeSpeed);

            RemainingDodgeSpeedTime -= Time.deltaTime;
        }
        else if (IsSprinting)
        {
            rb.linearVelocity = new Vector3(Movement.x * MovementRunSpeed, rb.linearVelocity.y, Movement.z * MovementRunSpeed);
            if (Movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(Movement);
            }
        }
        else
        {
            rb.linearVelocity = new Vector3(Movement.x * MovementSpeed, rb.linearVelocity.y, Movement.z * MovementSpeed);
            if (Movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(Movement);
            }
        }

        

    }



    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovementFunc();
        animator.SetFloat("Speed", Movement.magnitude);
    }

    public void EnableHitbox()
    {
        WeaponHitBox.SetActive(true);
    }
    public void DisableHitbox()
    {
        WeaponHitBox.SetActive(false);
    }

    public void DisableMovementFunc()
    {
        DisableMovement = true;
    }

    public void EnableMovementFunc()
    {
        DisableMovement = false;
    }

}
