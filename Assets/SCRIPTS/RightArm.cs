using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint shoulderJoint;
    [SerializeField] private bool isHoldingArm = false;
    [SerializeField] private Rigidbody hip;
    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 armDirection; 
    [SerializeField] private ConfigurableJoint handJoint;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        armDirection = hip.position - cam.position;
        if(Input.GetButton("Fire2")){
            shoulderJoint.targetRotation = Quaternion.Euler(0f,-armDirection.y * 10f,340f);
            isHoldingArm = true;
        }
        else {
            isHoldingArm = false;
            SetDefaultRotation();
            Destroy(GetComponent<FixedJoint>());
        }

        
    }

        private void SetDefaultRotation(){
            shoulderJoint.targetRotation = Quaternion.Euler(0f,0f,0f);

    }

    private void OnCollisionEnter(Collision other){
        if(isHoldingArm){   
            Debug.Log("collision");
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            if(rb !=null){
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }
        }
        
    }
    


}
