using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnotherMovementController : MonoBehaviour
{
    public bool isDead;
    public Transform player;
    public GameObject playerModel;
    public Animator playerAnim;
    public float moveSpeed=1;

    public GameObject playerRagdoll;

    private void Start()
    {
        
    }

    void Update()
    {


        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        player.position += Time.deltaTime * moveSpeed * moveDir;
        player.rotation = Quaternion.LookRotation(moveDir);
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            playerAnim.SetBool("Walk", true);
        }
        else 
        {
            playerAnim.SetBool("Walk", false);
        }
    }
}