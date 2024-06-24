using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCCatchState : BaseState
{
    public float catchDistance;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnEnterState");
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;
        npcStateMachine.SetDestination(npcStateMachine.PlayerPosition);

        if (Vector3.Distance(npcStateMachine.NPCPosition, npcStateMachine.PlayerPosition) <= catchDistance)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Transitions
        // Can't see or hear player
        if (!npcStateMachine.CanHearPlayer && !npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnExitState");
    }
}
