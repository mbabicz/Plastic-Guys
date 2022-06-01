using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour

{

    [SerializeField] private GameObject expectedBox;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other, expectedBox)){
            if(expectedBox.name == "redPlate"){
                GM.redPlate = true;
            }
                else if (expectedBox.name =="bluePlate") GM.bluePlate = true;
            if(GM.redPlate && GM.bluePlate) GM.OpenGate();
        }
    }

    private void OnTriggerExit(Collider other){

         if(GameObject.ReferenceEquals(other, expectedBox)){
             if(expectedBox.name == "redPlate"){
                GM.redPlate = false;
             }
            else if(expectedBox.name == "bluePlate") GM.bluePlate = false;
             if(!GM.redPlate || !GM.bluePlate) GM.CloseGate();
             

        }
    }
        
    
}
