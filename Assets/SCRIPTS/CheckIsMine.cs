using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CheckIsMine : MonoBehaviour
{
    // Start is called before the first frame update
        PhotonView view;
        

    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!view.IsMine){
            Destroy(this.gameObject);
        }
    }
}
