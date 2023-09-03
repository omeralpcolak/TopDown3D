using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private float followDistance = 5.0f; // Desired distance between enemy and player

    public GameObject enemyHat;
    Animator enemyHatAnim;


    private void Start()
    {
        enemyHatAnim = enemyHat.GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {

        if (player != null)
        {
            FollowThePlayer();
        }
       
        
    }


    private void FollowThePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > followDistance)
        {
            Vector3 targetPosition = player.position + (transform.position - player.position).normalized * followDistance;
            navMeshAgent.SetDestination(targetPosition);
            enemyHatAnim.SetBool("enemyRun", true);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
            enemyHatAnim.SetBool("enemyRun", false);
        }
    }
}
