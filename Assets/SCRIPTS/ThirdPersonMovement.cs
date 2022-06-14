using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] private Transform cam;
    private float turnsmoothTime = 0.1f;
    private float turnsmoothVelocity;
    [SerializeField] private float horizontal,vertical;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;
    [SerializeField] private float speed = 8f;
    [SerializeField] private bool isWalking;
    [SerializeField] private Animator m_Animator;

    //* jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    private float jumpForce = 250f;
    private bool jumping;


    public bool isGroundedL, isGroundedR;

    void Update()
    {
        InputSystem();
        

    }

    private void FixedUpdate()
    {
        Movement();    
    }


    void InputSystem(){

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
    
    }

    void Movement(){

        //*extra gravity
        if(!isGroundedR && !isGroundedR){
            hip.AddForce(Vector3.down * Time.deltaTime * 500,ForceMode.Impulse);
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnsmoothVelocity, turnsmoothTime);
            
            
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            
            if(vertical == 1){
                Vector3 forwardDir = hip.position - cam.position;

                forwardDir.Normalize();

                hip.AddForce((forwardDir.x * speed ), 0, (forwardDir.z * speed ),ForceMode.Impulse);

            }
            if(vertical == -1){
                Vector3 forwardDir = hip.position - cam.position;

                forwardDir.Normalize();

                hip.AddForce((-forwardDir.x * speed ),0 ,(-forwardDir.z * speed ), ForceMode.Impulse);
            }
            if(horizontal == 1){
                hip.AddForce((hip.transform.forward * speed),ForceMode.Impulse);
            }
            if(horizontal == -1){
                hip.AddForce((hip.transform.forward * speed),ForceMode.Impulse);
            }            

            isWalking = true; 
        }  else {
           isWalking = false;
        }

       m_Animator.SetBool("Walk", isWalking);

        //jump
        if (readyToJump && jumping) Jump();

    }
      private void Jump()
    {
        if (isGroundedR || isGroundedL && readyToJump)
        {
            readyToJump = false;

            //Add jump force
            hip.AddForce(Vector2.up * jumpForce * 1.5f,ForceMode.Impulse);

            //If jumping while falling, reset y velocity.
            Vector3 vel = hip.velocity;
            if (hip.velocity.y < 0.5f)
                hip.velocity = new Vector3(vel.x, 0, vel.z);
            else if (hip.velocity.y > 0)
                hip.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
