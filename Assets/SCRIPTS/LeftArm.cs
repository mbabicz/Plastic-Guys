using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour
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

        if(Input.GetButton("Fire1")){
            shoulderJoint.targetRotation = Quaternion.Euler(0f,(armDirection.y*10),-332f);
            isHoldingArm = true;
        }
        else {
            isHoldingArm = false;
            //handJoint.connectedBody = null;
            SetDefaultRotation();
            //Destroy(GetComponent<FixedJoint>());
        }

        
    }

        private void SetDefaultRotation(){
            shoulderJoint.targetRotation = Quaternion.Euler(0f,0f,0f);

    }

}
