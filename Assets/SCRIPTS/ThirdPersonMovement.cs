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
    [SerializeField] private float speed = 5550f;
    [SerializeField] private bool isWalking;
    [SerializeField] private Animator m_Animator;

    void Update()
    {
        InputSystem();
        Movement();

    }

    private void FixedUpdate()
    {
        //Movement();    
    }


    void InputSystem(){

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    
    }

    void Movement(){

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnsmoothVelocity, turnsmoothTime);
            
            
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            
            if(vertical == 1){
                Vector3 forwardDir = hip.position - cam.position;

                forwardDir.Normalize();

                hip.AddForce((forwardDir.x * speed), 0, (forwardDir.z * speed));

            }
            if(vertical == -1){
                Vector3 forwardDir = hip.position - cam.position;

                forwardDir.Normalize();

                hip.AddForce((-forwardDir.x * speed),0 ,(-forwardDir.z * speed));
            }
            if(horizontal == 1 && vertical == 0){
                hip.AddForce((transform.forward * speed));

            }
            if(horizontal == -1 && vertical == 0){
                hip.AddForce((-transform.forward * speed));

            }

            isWalking = true; 
        }  else {
           isWalking = false;
        }

       m_Animator.SetBool("Walk", isWalking);

    }
}
