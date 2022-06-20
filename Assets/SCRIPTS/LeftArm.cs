using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeftArm : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint shoulderJoint;
    [SerializeField] private bool isHoldingArm = false;
    [SerializeField] private Rigidbody hip;
    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 armDirection; 
    [SerializeField] private ConfigurableJoint handJoint;
    PhotonView view;


    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            armDirection = hip.position - cam.position;
            Debug.DrawLine(hip.position,cam.position, Color.green);
            if(Input.GetButton("Fire1")){
                shoulderJoint.targetRotation = Quaternion.Euler(0f,(armDirection.y*10),-332f);
                isHoldingArm = true;
            }
            else {
                isHoldingArm = false;
                SetDefaultRotation();
                Destroy(GetComponent<FixedJoint>());
            }
        }

        
    }

        private void SetDefaultRotation(){
            shoulderJoint.targetRotation = Quaternion.Euler(0f,0f,0f);

    }

   private void OnCollisionEnter(Collision other){
        if(view.IsMine){
            if(isHoldingArm){   
                Debug.Log("left arm collision");
                Rigidbody rb = other.transform.GetComponent<Rigidbody>();
                if(rb !=null){
                    FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                    fj.connectedBody = rb;
                }
            }
        }
        
    }

}
