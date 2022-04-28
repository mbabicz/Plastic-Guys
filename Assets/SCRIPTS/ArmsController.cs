using System.Runtime.CompilerServices;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{

    //[SerializeField] private Transform hand;
    [SerializeField] private ConfigurableJoint leftShoulderJoint;
    [SerializeField] private ConfigurableJoint rightShoulderJoint;    
    [SerializeField] private Rigidbody hip;
    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 armPosition;



    void Start()
    {

    }


    private void Update() {

        //armPosition = hip.position - cam.position;
        SetDefaultRotation();

        if(Input.GetButton("Fire1")){//left arm
          leftShoulderJoint.targetRotation = Quaternion.Euler(0f,-100f,-332f);
          //leftShoulderJoint.targetRotation = Quaternion.Euler(armPosition);
        }
        if(Input.GetButton("Fire2")){//right arm
          rightShoulderJoint.targetRotation = Quaternion.Euler(0f,100f,340f);
          //qrightShoulderJoint.targetRotation = Quaternion.Euler(armPosition);
        }        
    }


    private void SetDefaultRotation(){
        leftShoulderJoint.targetRotation = Quaternion.Euler(0f,0f,0f);
        rightShoulderJoint.targetRotation = Quaternion.Euler(0f,0f,0f);


    }

}