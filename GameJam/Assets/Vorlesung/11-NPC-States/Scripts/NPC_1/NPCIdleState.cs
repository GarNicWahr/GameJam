using System;
using UnityEngine;

[Serializable]
public class NPCIdleState : BaseState
{
    public float MinWaitTime;
    public float MaxWaitTime;

    private float _leaveTime;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCIdleState:OnEnterState");

        _leaveTime = Time.time + UnityEngine.Random.Range(MinWaitTime, MaxWaitTime);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCIdleState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;

        // Transitions
        //Can see or hear player > Switch to catch
        if(npcStateMachine.CanHearPlayer || npcStateMachine.CanSeePlayer)
        {
            //npcStateMachine.SwitchToState(npcStateMachine.FleeState);
            npcStateMachine.SwitchToState(npcStateMachine.CatchState);
        }

        //Time is up > Switch to patrol
        if (Time.time > _leaveTime)
        {
            npcStateMachine.SwitchToState(npcStateMachine.PatrolState);
        }

    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCIdleState:OnExitState");
    }
}
