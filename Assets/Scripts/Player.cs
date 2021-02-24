using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region
    //better to use 'const' everytime for unchangeable value
    const float gravity = -9.81f;
    public float jumpPower = 5;
    [SerializeField]
    float yVelocity;
    float jumpForce = 1f;
    [SerializeField]
    private float climbSpeed = 3f;

    public float walkSpeed = 3;
    public float runSpeed = 6;
    CharacterController cc;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity; 

    [Header("Animator")]
    [SerializeField]
    Animator animator;
    private int onGrounded;
    private int isJumping;

    [Header("Effect")]
    [SerializeField]
    ParticleSystem footFX;

    [SerializeField]
    private float HammertimeDuration = 5f;

    [SerializeField] PlayerState playerState = PlayerState.Idle;
    public List<Interacable> interacablesList = new List<Interacable>();

    public enum PlayerState
    {
        Idle,
        HammerTime
    }

    #endregion
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //Set animator and check 
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("ERROR::THERE IS NO ANIMATOR ON THE CHARACTER");
        onGrounded = Animator.StringToHash("onGrounded");
        isJumping = Animator.StringToHash("isJumping");

        footFX = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        CharacterMovement(out h, out v);
        MovementState(h, v);
        //CallHammerTime();
    }

    private void CallHammerTime()
    {
        ChangeState(PlayerState.HammerTime);

        if (playerState == PlayerState.HammerTime)
        {
            foreach (var interactable in interacablesList)
            {
                interactable.OnHammerTime();
            }
        }
        StartCoroutine(OffHammerTime());
    }
    IEnumerator OffHammerTime()
    {
        yield return new WaitForSeconds(HammertimeDuration);
        CallOffHammerTime();
    }
    private void CallOffHammerTime()
    {
        ChangeState(PlayerState.Idle);

        foreach (var interactable in interacablesList)
        {
            interactable.OffHammerTime();
        }
    }


    private void CharacterMovement(out float h, out float v)
    {
        yVelocity += gravity * Time.deltaTime;
        if (cc.isGrounded)
        {
            //jumpCount = 0;
            yVelocity = 0;
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;
            }
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Reverse direction due to local to world direction
        Vector3 dir = new Vector3(-h, 0, -v).normalized;
        Vector2 inputDir = new Vector2(h, v).normalized;

        dir.y = yVelocity;

        //Aplly rotation
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);

        //Running Algorithms

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = ((isRunning) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, speed, ref speedSmoothVelocity, speedSmoothTime);

        float animationSpeedPercent = ((isRunning) ? 1 : 0.5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        cc.Move(dir.normalized * currentSpeed * Time.deltaTime);
    }
    private void MovementState(float horizontal, float vertical)
    {
        //isGrounded Check

        if(cc.isGrounded && !animator.GetBool(isJumping))
        {
            animator.SetBool(onGrounded, true);
        }

        else if (cc.isGrounded && !animator.GetBool(onGrounded))
        {
            animator.SetBool(onGrounded, true);
        }

        else if (!cc.isGrounded && animator.GetBool(isJumping))
        {
            animator.SetBool(onGrounded, false);
        }

        if (Input.GetButton("Jump") && animator.GetBool(onGrounded))
        {
            animator.SetBool(isJumping, true);
        }

        else if (animator.GetBool(isJumping) && !cc.isGrounded)
        {
            animator.SetBool(isJumping, false);
        }

        if (this.playerState == PlayerState.HammerTime && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //통돌이 죽음
        if (hit.collider.CompareTag("Drum"))//통돌이
        {
            //print a collision between the character and drums
            //cc.Move(Vector3.up * jumpForce);

            //Destroy(hit.gameObject);

            //disable cc for desabling cc && death animation
            //cc.enabled = false;
        }

        //Coin 획득 & GameManager 소환 

        //if (hit.collider.CompareTag("Coin"))
        //{

        //    Destroy(hit.gameObject);
        //    ScoreManager.instance.SCORE++;
        //}

        //망치 획득
        if (hit.collider.CompareTag("Hammer"))
        {
            //Change PlayerState to HammerTime
            //Start Timer
            //Call Idle State
            print("Hammer");
            Destroy(hit.gameObject);
            ChangeState(PlayerState.HammerTime);
            CallHammerTime();
        }

        //열쇠 획득
        if (hit.collider.CompareTag("Key"))
        {
            Destroy(hit.gameObject);
        }

        //Hammer 획득
        if (hit.collider.CompareTag("Enemy")) //FireEnemy
        {
            Destroy(hit.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ladder")
        {
            animator.SetBool("onLadder", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ladder" && Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isClimbing", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            animator.SetBool("onLadder", false);
        }
    }

    void ChangeState(PlayerState playerState)
    {
        this.playerState = playerState;
    }
}
