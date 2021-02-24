using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRigidbody;

    //Physics
    private const float GRAVITY = 9.81f;

    public CharacterController controller;

    public float speed = 6f;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    //Ladder
    bool onLadder;
    //public float climbingSpeed = 1f;

    //Aimation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("ERROR::THERE IS NO ANIMATOR ON THE CHARACTER");
        //onLadder = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Gravity
        direction.y -= GRAVITY;
        /*if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDamp(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //controller.Move(direction * speed * Time.deltaTime);
        }*/

        controller.Move(direction.normalized * speed * Time.deltaTime);

        if (onLadder)
        {
            if(Input.GetKey(KeyCode.Z))
            {
                controller.Move(Vector3.up * speed * Time.deltaTime);
                Debug.Log("Z is typed");
            }
        }

        MovementState(horizontal, vertical);
    }

    private void MovementState(float horizontal, float vertical)
    {
        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            //Debug.Log("Moving");
            animator.SetBool("isWalking", true);
        }
        else
        {
            //Debug.Log("Stop Moving");
            animator.SetBool("isWalking", false);
        }
        /*
        if (onLadder && Input.GetKeyDown(KeyCode.Z))
        {
            playerRigidbody.useGravity = false;
        }
        if (onLadder && Input.GetKey(KeyCode.Z))
        {
            Debug.Log("GetKey is input");
            playerRigidbody.AddForce(Vector3.up);
        }
        if (onLadder && Input.GetKeyUp(KeyCode.Z))
        {
            playerRigidbody.useGravity = true;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ladder")
        {
            Debug.Log("In the Ladder spot");
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            onLadder = false;
        }
    }
}
