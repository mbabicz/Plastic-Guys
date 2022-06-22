using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RightArm : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint shoulderJoint;
    [SerializeField] private bool isHoldingArm = false;
    [SerializeField] private Rigidbody hip;
    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 armDirection; 
    [SerializeField] private ConfigurableJoint handJoint;

    [SerializeField] private Vector3 campos;
    [SerializeField] private Vector3 hippos;

    PhotonView view;

    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine){
            armDirection = hip.position + cam.position;

            if(Input.GetButton("Fire2")){
                shoulderJoint.targetRotation = Quaternion.Euler(0,armDirection.y * 10f,320f);
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
        
        if(isHoldingArm){   
            other.transform.GetComponent<PhotonView>().RequestOwnership();
            Debug.Log("collision");
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            if(rb !=null){
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                fj.connectedBody = rb;
            }
        }
    }
}
