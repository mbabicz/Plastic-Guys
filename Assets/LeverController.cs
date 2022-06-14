using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject crane;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (this.transform.rotation.z > 20){
            crane.transform.position = new Vector3(200,0,0) * Time.deltaTime; 
        }
        if (this.transform.rotation.z < -20){
            crane.transform.position = new Vector3(-2,0,0) * Time.deltaTime; 
        }
    }
}
