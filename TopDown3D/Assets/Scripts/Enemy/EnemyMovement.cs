using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private float followDistance = 5.0f; // Desired distance between enemy and player

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > followDistance)
        {
            // Calculate the position the enemy should move towards
            Vector3 targetPosition = player.position + (transform.position - player.position).normalized * followDistance;

            // Move towards the calculated target position
            navMeshAgent.SetDestination(targetPosition);
        }
        else
        {
            // Stop moving when the enemy is within the desired distance
            navMeshAgent.SetDestination(transform.position);
        }
    }
}
