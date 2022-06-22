using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class button : MonoBehaviour
{

    private Animator LeftDoorAnim;
    private Animator RightDoorAnim;
    [SerializeField] private LiftController LF;
    [SerializeField] private GameObject Lift;
    [SerializeField] private GameManager GM;

    //lift
    [SerializeField] private bool liftUpBtn;
    [SerializeField] private bool liftDownBtn;
    //doors
    [SerializeField] private bool leftDoorBtn;
    [SerializeField] private bool rightDoorBtn;
    [SerializeField] private bool lastDoor1;
    [SerializeField] private bool lastDoor2;
    PhotonView view;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerArm")){
            if(liftDownBtn){
                Debug.Log("lift down button clicked");
                Lift.transform.GetComponent<PhotonView>().RequestOwnership();
                LF.downButton = true;
            }
            if(liftUpBtn){
                Debug.Log("lift up button clicked");
                Lift.transform.GetComponent<PhotonView>().RequestOwnership();
                LF.upButton = true;
            }
            if(leftDoorBtn){
                Debug.Log("Left door btn pressed");
                GM.LeftBtnPressed = true;
            }
            if(rightDoorBtn){
                Debug.Log("Right door btn pressed");
                GM.RightBtnPressed = true;
            }
            if(lastDoor1){
                Debug.Log("Right door btn pressed");
                GM.LastDoorBtnPressed1 = true;
            }
            if(lastDoor2){
                Debug.Log("Right door btn pressed");
                GM.LastDoorBtnPressed2 = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("PlayerArm")){
            if(liftDownBtn){
                Debug.Log("lift down button unclicked");
                LF.downButton = false;
                
            }
            if(liftUpBtn){
                Debug.Log("lift up button unclicked");
                LF.upButton = false;
            }
            if(leftDoorBtn){
                Debug.Log("Left door btn unpressed");
                GM.LeftBtnPressed = false;
            }
            if(rightDoorBtn){
                Debug.Log("Right door btn unpressed");
                GM.RightBtnPressed = false;
            }
        }
    }
}
