using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("1st doors")] // 1st doors
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;
    private Animator RighDoorAnim;
    private Animator LeftDoorAnim;
    public bool LeftBtnPressed;
    public bool RightBtnPressed;
    public bool LastDoorBtnPressed1;
    public bool LastDoorBtnPressed2;

    [Header("2nd doors")]//2nd doors
    [SerializeField] private GameObject LeftDoor2;
    [SerializeField] private GameObject RightDoor2;
    private Animator RighDoorAnim2;
    private Animator LeftDoorAnim2;

    [Header("3rd doors")]//2nd doors
    [SerializeField] private GameObject LeftDoor3;
    [SerializeField] private GameObject RightDoor3;
    private Animator RighDoorAnim3;
    private Animator LeftDoorAnim3;

    [Header("4rd doors")]//2nd doors
    [SerializeField] private GameObject LeftDoor4;
    [SerializeField] private GameObject RightDoor4;
    private Animator RighDoorAnim4;
    private Animator LeftDoorAnim4;

    public bool bluePlate, redPlate, greenPlate, purplePlate;
    public int points;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //1st doors buttons
        LeftBtnPressed = false;
        RightBtnPressed = false;

        //2nd doors plates
        redPlate = false;
        bluePlate = false;
        purplePlate = false;
        greenPlate = false;

        LastDoorBtnPressed1 = false;
        LastDoorBtnPressed2 = false;
        points = 0;
        //* ANIMATIONS
        //1ST DOORS
        RighDoorAnim = RightDoor.GetComponent<Animator>();
        LeftDoorAnim = LeftDoor.GetComponent<Animator>();
        //2ND DOORS
        RighDoorAnim2 = RightDoor2.GetComponent<Animator>();
        LeftDoorAnim2 = LeftDoor2.GetComponent<Animator>();
        //3RD DOORS
        RighDoorAnim3 = RightDoor3.GetComponent<Animator>();
        LeftDoorAnim3 = LeftDoor3.GetComponent<Animator>();
        //4RD DOORS
        RighDoorAnim4 = RightDoor4.GetComponent<Animator>();
        LeftDoorAnim4 = LeftDoor4.GetComponent<Animator>();

    }


    void Update()
    {
        if(LeftBtnPressed && RightBtnPressed) OpenDoors1();
        if(redPlate && bluePlate && purplePlate && greenPlate) OpenDoors2();
        if(LastDoorBtnPressed1) OpenDoors3();
        if(LastDoorBtnPressed2) OpenDoors4();

        if(points == 2) SceneManager.LoadScene("EndGame"); 
    }


    public void OpenDoors1(){
        //ANIMATION
        RighDoorAnim.SetTrigger("RightDoor");
        LeftDoorAnim.SetTrigger("LeftDoor");
    }
    public void OpenDoors2(){
        //ANIMATION
        RighDoorAnim2.SetTrigger("RightDoor");
        LeftDoorAnim2.SetTrigger("LeftDoor");
    }

    public void OpenDoors3(){
        //ANIMATION
        RighDoorAnim3.SetTrigger("RightDoor");
        LeftDoorAnim3.SetTrigger("LeftDoor");
    }

    public void OpenDoors4(){
        //ANIMATION
        RighDoorAnim4.SetTrigger("RightDoor");
        LeftDoorAnim4.SetTrigger("LeftDoor");
    }        
}
