using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private bool isFollowingPlayer = false;
    private float randomPathDistance = 10.0f; // Distance for the random path

    private Vector3 randomPathTarget;
    private bool hasReachedRandomPathTarget = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomPathTarget();
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 8.0f)
        {
            isFollowingPlayer = true;
        }
        else if (!isFollowingPlayer)
        {
            if (hasReachedRandomPathTarget)
            {
                SetRandomPathTarget();
            }

            navMeshAgent.SetDestination(randomPathTarget);

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                hasReachedRandomPathTarget = true;
            }
        }

        if (isFollowingPlayer)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void SetRandomPathTarget()
    {
        // Find a reachable random point around the enemy
        Vector3 randomOffset = Random.insideUnitCircle * randomPathDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + randomOffset, out hit, randomPathDistance, NavMesh.AllAreas);

        randomPathTarget = hit.position;
        hasReachedRandomPathTarget = false;
    }
}
