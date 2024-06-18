using System;
using UnityEngine;

[Serializable]
public class NPCIdleState_Domi : BaseState
{
    public float MinWaitTime;
    public float MaxWaitTime;

    private float _leaveTime;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCIdleState_Domi:OnEnterState");

        _leaveTime = Time.time + UnityEngine.Random.Range(MinWaitTime, MaxWaitTime);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCIdleState_Domi:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine_Domi;

        // Transitions
        //Can see or hear player > Switch to flee
        if(npcStateMachine.CanHearPlayer || npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.FleeState_Domi);
        }

        //Time is up > Switch to patrol
        if (Time.time > _leaveTime)
        {
            npcStateMachine.SwitchToState(npcStateMachine.PatrolState_Domi);
        }

    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCIdleState_Domi:OnExitState");
    }
}
