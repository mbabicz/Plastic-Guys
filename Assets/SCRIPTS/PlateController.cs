using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private GameObject expectedBox;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == expectedBox.transform){
            if(expectedBox.name == "redBox") GM.redPlate = true;
            else if(expectedBox.name == "blueBox") GM.bluePlate = true;
            else if(expectedBox.name == "greenBox") GM.greenPlate = true;
            else if(expectedBox.name == "purpleBox") GM.purplePlate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == expectedBox.transform){
            if(expectedBox.name == "redBox") GM.redPlate = false;
            else if(expectedBox.name == "blueBox") GM.bluePlate = false;
            else if(expectedBox.name == "greenBox") GM.greenPlate = false;
            else if(expectedBox.name == "purpleBox") GM.purplePlate = false;
        }
    }    
}
