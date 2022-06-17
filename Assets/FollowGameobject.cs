using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameobject : MonoBehaviour
{
    [SerializeField] private Transform gameobjectToFollow;
    private Vector3 offset;
    private Vector3 offset2;

    void Start()
    {
        offset = gameobjectToFollow.position - transform.position;
        //offset = gameobjectToFollow.position - transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gameobjectToFollow.position - offset;
        //transform.rotation = gameobjectToFollow.rotation;
    }
}
