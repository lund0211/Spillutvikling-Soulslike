
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

    [SerializeField]
    GameObject PotionInHand;


    bool IsAttacking = false;
    bool DisableMovement = false;

    float MaxStamina = 100f;
    float CurrentStamina = 0f;

    float StaminaDodgeCost = 20f;

    float StaminaAttackCost = 20f;

    float StaminaSprintCost = 10f;

    bool PauseStaminaRegen = false;

    bool DeathSoundPlayed = false;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Health = GetComponent<HealthBehavior>();
        CurrentStamina = MaxStamina;
        Health.Damaged += OnDamageTaken;

        //For testing
        //rb.position = new Vector3(14.24f, 24.433f, 332.24f);
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


        if (Context.performed && CanDodge && CurrentStamina >= StaminaDodgeCost)
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
            StartCoroutine(StaminaRegenPauseTimer());
            AdjustStamina(-StaminaDodgeCost);
            StartCoroutine(DodgeTimer());
        }
    }

    public void OnSprint(InputAction.CallbackContext Context)
    {
        if (Context.performed && CurrentStamina > 0)
        {
            IsSprinting = true;
            animator.SetBool("IsSprinting", true);
        }
        else if (Context.canceled)
        {
            IsSprinting = false;
            animator.SetBool("IsSprinting", false);
            StartCoroutine(StaminaRegenPauseTimer());
        }

    }

    public void OnAttack(InputAction.CallbackContext Context)
    {
        if (Context.started && !IsAttacking && CurrentStamina >= StaminaAttackCost)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if ((stateInfo.IsName("Idle") || stateInfo.IsName("Walking") || stateInfo.IsName("Sprint")) && (!animator.IsInTransition(0)))
            {
                StartCoroutine(StaminaRegenPauseTimer());
                AdjustStamina(-StaminaAttackCost);
                StartCoroutine(AttackTimer());
            }
        }
    }

    public void OnHeal(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            if (Health != null)
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (HealthPotionCount > 0 && (stateInfo.IsName("Idle") || stateInfo.IsName("Walking") || stateInfo.IsName("Sprint")))
                {
                    Health.AdjustHealthBy(50f);
                    HealthPotionCount--;
                    animator.SetTrigger("Drinking");
                    AudioManager.Instance.PlayDrinkingSound(0f, 1f);
                }

            }

        }
    }

    private IEnumerator AttackTimer()
    {
        {

            animator.SetTrigger("Attack");
            AudioManager.Instance.PlaySwordSound(0.2f, 1f);
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
        if (IsAttacking == true || DisableMovement == true)
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

        if (RemainingDodgeSpeedTime > 0)
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

        StaminaRegeneration();
        //Debug.Log("Current Stamina: " + CurrentStamina);

    }

    public void AdjustStamina(float amount)
    {
        CurrentStamina += amount;
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        else if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
    }

    public void StaminaRegeneration()
    {
        if (CurrentStamina < MaxStamina && !PauseStaminaRegen && IsSprinting == false)
        {
            AdjustStamina(20f * Time.deltaTime);
        }
    }

    private IEnumerator StaminaRegenPauseTimer()
    {
        PauseStaminaRegen = true;
        yield return new WaitForSeconds(2f);
        PauseStaminaRegen = false;
    }

    private void Update()
    {
        MovementFunc();
        animator.SetFloat("Speed", Movement.magnitude);

        if (IsSprinting == true && IsEnemyNearby())
        {
            AdjustStamina(-StaminaSprintCost * Time.deltaTime);
            if (CurrentStamina <= 0)
            {
                IsSprinting = false;
                animator.SetBool("IsSprinting", false);
            }
        }
        if (Health.CurrentHealth <= 0)
        {
            animator.SetBool("Dead", true);
            DisableHitbox();
            DisableMovementFunc();
            if (!DeathSoundPlayed)
            {
                AudioManager.Instance.PlayDyingSound(0f, 1f);
                DeathSoundPlayed = true;
            }
        }
        else
        {
            DeathSoundPlayed = false;
            animator.SetBool("Dead", false);
        }
    }

    public void OnDamageTaken()
    {
        if (Health.CurrentHealth > 0)
        {
            AudioManager.Instance.PlayHurtSound(0f, 1f);
        }
    }

    private bool IsEnemyNearby()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= 30f)
            {
                return true;
            }
        }
        return false;
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

    public void RestorePotionCount()
    {
        HealthPotionCount = 3;
    }

    public void ShowPotionInHand()
    {
        PotionInHand.SetActive(true);
    }

    public void HidePotionInHand()
    {
        PotionInHand.SetActive(false);
    }

    public float GetMaxStamina()
    {
        return MaxStamina;
    }

    public float GetCurrentStamina()
    {
        return CurrentStamina;
    }

    public int GetHealthPotionCount()
    {
        return HealthPotionCount;
    }


}
