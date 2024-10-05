using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State
{
    public float wanderRadius = 10f;
    public float patrolTimer = 4f;
    private float timer;
    public Wander(GameObject _npc, NavMeshAgent _agent, Transform _player)
        : base(_npc, _agent, _player)
    {
        name = STATE.WANDER;
        agent.speed = 1;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        timer = 0;
        SetRandomDestination();
        base.Enter();
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= patrolTimer)
        {
            nextState = new Patrol(npc, agent, player);
            stage = EVENT.EXIT;
        }

        if (timer <= patrolTimer && Random.Range(0, 100) < 10) //Only enter Idle state if the timer hasn't expired and randomly
        {
            nextState = new Idle(npc, agent, player);
            stage = EVENT.EXIT;
        }


    }

    public override void Exit()
    {
        base.Exit();
    }

    private void SetRandomDestination()
    {
        //Generate a random point within the specified radius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += npc.transform.position; //Center the random point around the NPC

        //Check if the random point is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); //Set destination to valid NavMeshPosition
        }
        else
        {
            SetRandomDestination(); //Do it again --> maybe set to Idle
        }
    }
}
