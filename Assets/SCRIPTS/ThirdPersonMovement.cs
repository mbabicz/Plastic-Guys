using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] private Transform cam;
    private float turnsmoothTime = 0.1f;
    private float turnsmoothVelocity;
    private float horizontal,vertical;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;
    [SerializeField] private float speed = 25f;


    void Update()
    {
        InputSystem();
        Movement();

    }


    void InputSystem(){

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    
    }

    void Movement(){

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnsmoothVelocity, turnsmoothTime);
            
            //float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;

            hip.AddForce(moveDir * this.speed);

           // isWalking = true;
        }  else {
            //isWalking = false;
        }

       //m_Animator.SetBool("Walk", isWalking);

    }
}
