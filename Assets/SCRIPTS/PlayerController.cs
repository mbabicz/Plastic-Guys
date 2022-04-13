using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Assingables
    [SerializeField]
    private Transform thirdCamera;
    public Rigidbody rb;


    public bool isGamePaused; //will be controlled by gamemanager
    [SerializeField]
    private LayerMask whatIsGround;

    //jumping
    private bool readyToJump = true;
    private bool jumping;
    private float jumpForce = 250f;
    private float jumpCooldown = 0.25f;

    //isGrounded
    [SerializeField]
    private bool grounded;
    private bool cancellingGrounded;
    private float maxSlopeAngle = 35f;
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    //movement
    private float multiplier, multiplierV; //multipliers for movement - graunded/in the air
    private float moveSpeed = 4500;
    private float x,y;

    //PlayerDirection
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;


    //counterMovement
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;


    //animator
    [SerializeField]
    private Animator anim;


    void Awake() //find rb
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start(){
        isGamePaused = false;
    }

    private void FixedUpdate()
    {
        if (isGamePaused) return;

    }

    private void Update()
    {
        if (isGamePaused) return;
        InputSystem();
        Movement();
    }


    private void InputSystem(){
        jumping = Input.GetButton("Jump");

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }


    public Vector2 FindVelRelativeToLook() //for counterMovement
    {
        float lookAngle = thirdCamera.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
    private void Movement(){

        rb.AddForce(Vector3.down * Time.deltaTime * 1000); //add gravity

        //jumping
        if (readyToJump && jumping) Jump();

        //counterMovement - sliding controller
        Vector2 mag = FindVelRelativeToLook();
        CounterMovement(x, y, mag);


        //multipliers
        multiplier = 1f;
        multiplierV = 1f;

        //multipliers for movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }


        //direction ---- rotation
        Vector3 direction = new Vector3 (x, 0f, y).normalized;

        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + thirdCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f)* Vector3.forward;
            rb.AddForce(moveDir.normalized * moveSpeed * Time.deltaTime);
            //anim.SetFloat("vertical",Input.GetAxis("Vertical"));
            //anim.SetFloat("horizontal",Input.GetAxis("Horizontal"));
        }
    }

    private void Jump(){ //jump function
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


    /*private void PlayerDirection(){
        Vector3 direction = new Vector3 (x, 0f, y).normalized;

        if(direction.magnitude>0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg + thirdCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
        }
    }
    */



    private void  CounterMovement(float x, float y, Vector2 mag){

        if (!grounded || jumping) return;

        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * thirdCamera.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * thirdCamera.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
    }
}