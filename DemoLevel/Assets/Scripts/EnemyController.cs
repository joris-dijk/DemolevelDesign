using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public float patrolRadius = 10f;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!agent.hasPath || agent.remainingDistance < 1f)
        {
            var randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, patrolRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
            }
        }
    }
}
