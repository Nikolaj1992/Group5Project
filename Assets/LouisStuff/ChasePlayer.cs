using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private StatusEffectHandler statusEffectHandler;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        statusEffectHandler = GetComponent<StatusEffectHandler>();
    }
    
    void Update()
    {
        agent.speed = statusEffectHandler.speed;
        
        if (Vector3.Distance(player.position, transform.position) > 1.5f)
        { 
            agent.SetDestination(player.position);
            agent.isStopped = false;
        }
        else
        {
            agent.SetDestination(gameObject.transform.position);
            agent.isStopped = true;
        }
    }

}
