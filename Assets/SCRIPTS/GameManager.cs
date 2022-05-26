using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool redPlate, bluePlate;
    [SerializeField] private GameObject gate;
    private Animator GateAnimator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        redPlate = false;
        bluePlate = false;

        GateAnimator = gate.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OpenGate(){
        //ANIMATION
        LeftDoorAnim.SetTrigger("OpenGate");
    }

    public void CloseGate(){

    }


}
