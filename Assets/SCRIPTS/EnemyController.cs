using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float dist, zombieRange;
    [SerializeField] private bool canFollow;
    [SerializeField] private Vector3 randomDirection;

    float lastAttack = 0;
    float attackCooldown = 2f;
    [SerializeField] private float test1;

    void Start()
    {

        player = GameObject.FindWithTag("Player");
        enemy = this.GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
        StartCoroutine(RandomWalk());
    }


    void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist < stoppingDistance){
            StopWalk();
            Attack();
            canFollow = true;
        }
        else if (dist > stoppingDistance && dist < zombieRange) {
            m_Animator.SetBool("isAttacking", false);
            canFollow = true;
            GoToPlayer();
        }
        else{
            canFollow = false;
        }
        
        //test1 = transform.GetComponent<Rigidbody>().velocity.magnitude;
    }

    IEnumerator RandomWalk(){
        if(!canFollow){
            //Vector3 RandomPosition = new Vector3(Random.Range(0,10), this.transform.position.y, Random.Range(0,360));

            //* random in a  
            float rx = Random.Range(0,10);
            float rz = Random.Range(0,10);
            randomDirection = new Vector3(rx, this.transform.position.y, rz);
            enemy.SetDestination(randomDirection);
            if(GetComponent<Rigidbody>().velocity.magnitude > 0){
                Debug.Log("zombie walk true");
            }
            else{
                m_Animator.SetBool("isWalking", false);
            }
            
        }

        yield return new WaitForSeconds(Random.Range(3,10));
    }
    void GoToPlayer(){
        enemy.isStopped = false;
        enemy.SetDestination(player.transform.position);
        m_Animator.SetBool("isAttacking", false);
        m_Animator.SetBool("isWalking", true);
        
    }

    void StopWalk(){
        enemy.isStopped = true;
        m_Animator.SetBool("isWalking", false);
    }

    void Attack(){
        if( Time.time  - lastAttack >= attackCooldown){
            lastAttack = Time.time;
            Vector3 TargetPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            this.transform.LookAt(TargetPosition);
            m_Animator.SetBool("isAttacking", true);

        }

    }
}
