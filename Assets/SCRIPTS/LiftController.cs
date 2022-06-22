using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    public bool upButton;
    public bool downButton;
    
    private void FixedUpdate() {
        if(upButton && !downButton && this.transform.position.y < 21) RaiseElevator();
        if(downButton && !upButton && this.transform.position.y > 9f) LowerElevator();        
    }
    void RaiseElevator(){
        transform.Translate(Vector3.up * Time.deltaTime * 1.5f, Space.World);
    }

    void LowerElevator(){
        transform.Translate(Vector3.down * Time.deltaTime * 1.5f, Space.World);
    }
}
