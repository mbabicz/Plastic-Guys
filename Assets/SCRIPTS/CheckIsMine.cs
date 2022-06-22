using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CheckIsMine : MonoBehaviour
{
    PhotonView view;

    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(!view.IsMine){
            Destroy(this.gameObject);
        }
    }
}
