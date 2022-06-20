using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhysicsLagCompensation : MonoBehaviourPunCallbacks, IPunObservable
{

    Rigidbody rb;
    private Vector3 _netPosition;
    private Quaternion _netRotation;
    private Vector3 _previousPos;

    public bool TeleportIfFar;
    public float teleportIfFarDistance;
    
    public float smoothPos = 5f;
    public float smoothRot = 5f;
    private void Awake() {
            rb = GetComponent<Rigidbody>();
            PhotonNetwork.SendRate = 1000;
            PhotonNetwork.SerializationRate = 10;
    }
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else{
            _netPosition = (Vector3) stream.ReceiveNext();
            _netRotation = (Quaternion) stream.ReceiveNext();
            rb.velocity = (Vector3) stream.ReceiveNext();

            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
            _netPosition +=(rb.velocity * lag);
            //rb.position += rb.velocity * lag;
        }
    }

    private void FixedUpdate() {
        if(photonView.IsMine) return;

        rb.position =Vector3.Lerp(rb.position, _netPosition, smoothPos * Time.fixedDeltaTime);
        rb.rotation = Quaternion.Lerp(rb.rotation, _netRotation, smoothRot * Time.fixedDeltaTime);

        if(Vector3.Distance(rb.position,_netPosition) > teleportIfFarDistance){
            rb.position = _netPosition;
        } 
    }

}
