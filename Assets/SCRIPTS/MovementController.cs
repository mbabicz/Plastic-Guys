using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] private Animator m_Animator;
    [SerializeField] private bool isWalking = true;
    [SerializeField] private float speed = 5f; //movement
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;
    private Rigidbody rb;


    private float horizontal,vertical;

    //jumping
    private bool jumping;
    private bool readyToJump = true;
    [SerializeField] private bool grounded;
    private float jumpForce = 250f;
    private float jumpCooldown = 0.25f;

    [SerializeField] private LayerMask whatIsGround;
    private float maxSlopeAngle = 35f;
    private bool cancellingGrounded;
private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }


    void Awake() //find rb
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //m_Animator.SetBool("Walk", isWalking);
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
        Movement();

        //jumping
        if (readyToJump && jumping) Jump();
    }


    void InputSystem(){
        jumping = Input.GetButton("Jump");

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    
    }

    void Movement(){

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            hip.AddForce(direction * this.speed);

            isWalking = true;
        }  else {
            isWalking = false;
        }

       m_Animator.SetBool("Walk", isWalking);

    }

    void Jump(){ //jump function
        if(grounded && readyToJump){
            readyToJump = false;
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(Vector3.up * jumpForce * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
                else if (rb.velocity.y > 0)
                    rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

                Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void StopGrounded()
    {
        grounded = false;
    }

    
    private void OnCollisionStay(Collision other)//grounding
        {
            //check for ground layers
            int layer = other.gameObject.layer;
            if (whatIsGround != (whatIsGround | (1 << layer))) return;

            //Iterate through every collision in a physics update
            for (int i = 0; i < other.contactCount; i++)
            {
                Vector3 normal = other.contacts[i].normal;
                //FLOOR
                if (IsFloor(normal))
                {
                    grounded = true;
                    cancellingGrounded = false;
                    CancelInvoke(nameof(StopGrounded));
                }
            }

            //Invoke StopGround
            float delay = 3f;
            if (!cancellingGrounded)
            {
                cancellingGrounded = true;
                Invoke(nameof(StopGrounded), Time.deltaTime * delay);
            }
        }
}
