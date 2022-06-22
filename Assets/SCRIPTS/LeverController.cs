using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeverController : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    private float platformSpeed = 2f;
    PhotonView view;
    private void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.z > 220){
            platform.transform.GetComponent<PhotonView>().RequestOwnership();
            PushPlatform();
        }
        if (transform.rotation.eulerAngles.z < 150){
            platform.transform.GetComponent<PhotonView>().RequestOwnership();
            PullPlatform();
        }
    }

    void PushPlatform(){
        if(platform.transform.position.x < 136){
            platform.transform.Translate(Vector3.right * Time.deltaTime * platformSpeed, Space.World);
        }
        
    }

    void PullPlatform(){
        if(platform.transform.position.x >108){
            platform.transform.Translate(Vector3.left * Time.deltaTime * platformSpeed, Space.World);
        }


    }
}
