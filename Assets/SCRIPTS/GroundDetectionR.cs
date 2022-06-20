using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GroundDetectionR : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    private float maxSlopeAngle = 35f;
    private bool cancellingGrounded;
    public ThirdPersonMovement TPM;
    PhotonView view;



    void Start()
    {
        //TPM = FindObjectOfType<ThirdPersonMovement>();
        view = transform.root.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }
    private void OnCollisionStay(Collision other)
    {
        if(view.IsMine){
            int layer = other.gameObject.layer;
            if (whatIsGround != (whatIsGround | (1 << layer))) return;

            //Iterate through every collision in a physics update
            for (int i = 0; i < other.contactCount; i++)
            {
                Vector3 normal = other.contacts[i].normal;
                //FLOOR
                if (IsFloor(normal))
                {
                    TPM.isGroundedR = true;
                    cancellingGrounded = false;
                    CancelInvoke(nameof(StopGrounded));
                }
            }

            float delay = 3f;
            if (!cancellingGrounded)
            {
                cancellingGrounded = true;
                Invoke(nameof(StopGrounded), Time.deltaTime * delay);
            }
        }
    }

    private void StopGrounded()
    {
        TPM.isGroundedR = false;
    }
}
