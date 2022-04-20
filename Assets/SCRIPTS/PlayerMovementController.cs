using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 
public class PlayerMovementController : MonoBehaviour
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
 
    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
 
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        player.position += Time.deltaTime * moveSpeed * moveDir;
        player.rotation = Quaternion.LookRotation(moveDir);
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else 
        {
            playerAnim.SetBool("isWalking", false);
        }
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDead = true;
            playerModel.SetActive(false);
            playerRagdoll.transform.position = playerModel.transform.position;
            playerRagdoll.SetActive(true);
        }
    }
}