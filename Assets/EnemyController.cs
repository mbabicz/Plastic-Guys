using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool canFollow;
    Transform player;
    public float FollowRange;
    private Transform baseRotation;
    private float ZombieSpeed;

     
    private GameObject Player;
    void Start()
    {
        canFollow = false;
        StartCoroutine(RandomWalk());
        this.GetComponent<SphereCollider>().radius = FollowRange;

        Player = GameObject.FindWithTag("Player");
        ZombieSpeed = 15f;
    }

    void FixedUpdate()
    {
        if (canFollow){
            FollowPlayer();
        }
    }

    
    void OnDrawGizmosSelected() //FollowRange preview
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FollowRange);
    }

    void OnTriggerEnter(Collider other) // enemy detection 
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.transform;
            canFollow = true;
            StopCoroutine(RandomWalk());
            Debug.Log("CanFollow = " +  canFollow);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") canFollow = false;
        StartCoroutine(RandomWalk());
        
    }

    void FollowPlayer(){
        Vector3 TargetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        this.transform.LookAt(TargetPosition);
        float distanceBetweenObjects = Vector3.Distance(transform.position, player.transform.position);
        if(distanceBetweenObjects > 2){
            this.GetComponent<Rigidbody>().AddForce(transform.forward * ZombieSpeed);
        }
        if(distanceBetweenObjects <= 2){
            //attack
        }
    }

    IEnumerator RandomWalk(){
        
        Vector3 RandomPosition = new Vector3(Random.Range(0,360), this.transform.position.y, Random.Range(0,360));

        this.transform.LookAt(RandomPosition);
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
        yield return new WaitForSeconds(Random.Range(3,10));
    }
        
}

