using System;
using UnityEngine;

[Serializable]
public class NPCFleeState_Domi : BaseState
{
    public float fleeDistance;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState_Domi:OnEnterState");
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState_Domi:OnUpdateState");
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState_Domi:OnExitState");
    }
}
