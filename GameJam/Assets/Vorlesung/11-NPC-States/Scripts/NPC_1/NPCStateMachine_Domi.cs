using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine_Domi : BaseStateMachine
{
    public Vector3 PlayerPosition { get => _player.position; }
    public bool CanSeePlayer { get => _eyes.IsDetecting; }
    public bool CanHearPlayer { get => _ears.IsDetecting; } 

    public NPCIdleState_Domi IdleState_Domi;
    public NPCFleeState_Domi FleeState_Domi;
    public NPCPatrolState_Domi PatrolState_Domi;


    private Eyes _eyes;
    private Ears _ears;

    private Transform _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    public override void Initialize()
    {
        _eyes = GetComponentInChildren<Eyes>();
        _ears = GetComponentInChildren<Ears>();

        _player = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        CurrentState = IdleState_Domi;
        CurrentState.OnEnterState(this);
    }

    public override void Tick()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude);
    }

    public void SetDestination (Vector3 destination)
    {
        _agent.SetDestination(destination);
    }
}


