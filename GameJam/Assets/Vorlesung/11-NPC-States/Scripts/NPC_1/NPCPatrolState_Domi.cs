using System;
using UnityEngine;

[Serializable]
public class NPCPatrolState_Domi : BaseState
{
    public Transform[] Waypoints;

    private int currentWaypointIndex;

    private Vector3 targetPosition;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState_Domi:OnEnterState");
        var npcStateMachine = controller as NPCStateMachine_Domi;

        if(targetPosition == Vector3.zero)
        {
            targetPosition = Waypoints[0].position;
        }

        // StateMachine sagen das der NavAgent bewegt wird
        npcStateMachine.SetDestination(targetPosition);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState_Domi:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine_Domi;

        float sqrDistance = (npcStateMachine.transform.position - targetPosition).sqrMagnitude;

        //Transitions
        //NPC reached Waypoint? > Switch to Idle
        if(sqrDistance < 0.1f)
        {
            targetPosition = GetNextWaypoint();
            npcStateMachine.SwitchToState(npcStateMachine.IdleState_Domi);
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState_Domi:OnExitState");
    }

    public Vector3 GetNextWaypoint()
    {
        currentWaypointIndex = ++currentWaypointIndex % Waypoints.Length;
        return Waypoints[currentWaypointIndex].position;
    }
}
