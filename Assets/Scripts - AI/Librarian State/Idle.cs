using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public float patrolTimer = 5f;
    private float timer;
    public Idle(GameObject _npc, NavMeshAgent _agent, Transform _player)
        : base(_npc, _agent, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        timer = 0;
        base.Enter();
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        //if (Random.Range(0, 100) < 2) //Randomly enter the wander state
        //{
        //    nextState = new Wander(npc, agent, player);
        //    stage = EVENT.EXIT;
        //}
        
        if (timer > patrolTimer)
        {
            nextState = new Patrol(npc, agent, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
