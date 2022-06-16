using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject crane;
    private float craneSpeed = 1.25f;

    private void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.z > 220){
            crane.GetComponent<Rigidbody>().AddForce(Vector3.right * craneSpeed,ForceMode.Impulse);
        }
        if (transform.rotation.eulerAngles.z < 150){
            crane.GetComponent<Rigidbody>().AddForce(Vector3.left * craneSpeed,ForceMode.Impulse);
        }
    }
}
