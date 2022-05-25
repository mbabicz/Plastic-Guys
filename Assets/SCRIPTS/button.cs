using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Animator LeftDoorAnim;
    private Animator RightDoorAnim;
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;



    // Start is called before the first frame update
    void Start()
    {
        LeftDoorAnim = LeftDoor.GetComponent<Animator>();
        RightDoorAnim = RightDoor.GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerArm")){
            Debug.Log("button click");
            LeftDoorAnim.SetTrigger("LeftDoor");
            RightDoorAnim.SetTrigger("RightDoor");
            Debug.Log("Doooooors");


        }
    }
}
