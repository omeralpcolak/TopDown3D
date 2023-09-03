using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyJumpAttack : MonoBehaviour
{
    public float attackCooldown = 2f;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Transform player;
    private Animator animator;
    private float lastAttackTime;

    private void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is in range and attack cooldown has elapsed.
        if (Time.time - lastAttackTime > attackCooldown && Vector3.Distance(transform.position, player.position) < 5f)
        {
            JumpAttack();
        }
    }

    private void JumpAttack()
    {
        // Trigger the jump attack animation.
        animator.SetTrigger("JumpAttack");

        // Set the destination of the NavMeshAgent to the player's position.
        navMeshAgent.SetDestination(player.position);

        // Update the attack cooldown time.
        lastAttackTime = Time.time;
    }
}
