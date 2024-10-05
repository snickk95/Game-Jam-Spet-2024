using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(GameObject _npc, NavMeshAgent _agent, Transform _player)
        : base(_npc, _agent, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }
}
