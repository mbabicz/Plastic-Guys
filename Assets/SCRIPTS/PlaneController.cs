using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour

{

    [SerializeField] private GameObject expectedBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other, expectedBox)){

        }
    }

    private void OnTriggerExit(Collider other){

         if(GameObject.ReferenceEquals(other, expectedBox)){

        }
    }
        
    
}
