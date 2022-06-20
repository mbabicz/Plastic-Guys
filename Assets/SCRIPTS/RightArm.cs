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

    //test
    [SerializeField] private Vector3 campos;
    [SerializeField] private Vector3 hippos;

    [SerializeField] private Vector3 test1;
    [SerializeField] private Vector3 test2;
    [SerializeField] private Vector3 test4;
    [SerializeField] private Vector3 test3;

    [SerializeField] private Vector3 test5;
    [SerializeField] private Vector3 test6;

    Vector3 temp;
    PhotonView view;


    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

        if(view.IsMine){
            //Vector3 temp = new Vector3(cam.position.x, -1* cam.position.y, cam.position.z); 
            //armDirection = hip.position + temp;

            armDirection = hip.position + cam.position;

            test1 = cam.position + hip.position;
            test2 = cam.position - hip.position;

            test3 = -cam.position + hip.position;
            test4 = -cam.position - hip.position;

            test5 = -hip.position - cam.position;
            test6 = -hip.position - cam.position;
            if(Input.GetButton("Fire2")){
                shoulderJoint.targetRotation = Quaternion.Euler(0f,armDirection.y * 10f,340f);
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
                Debug.Log("collision");
                Rigidbody rb = other.transform.GetComponent<Rigidbody>();
                if(rb !=null){
                    FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                    fj.connectedBody = rb;
                }
            }
        }
    }
    


}
