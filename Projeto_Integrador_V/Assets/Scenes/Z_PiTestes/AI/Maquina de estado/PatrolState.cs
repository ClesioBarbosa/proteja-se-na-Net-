using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolState : FSMState
{
    private Transform[] waypoints;
    private int currentWaypoint;


    public PatrolState(Transform[] points)
    {
        waypoints = points;
        currentWaypoint = 0;
        stateID = FSMStateID.Patrol;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) < 7f){
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.SawPlayer);
        }
    }
    //

    public override void Act(GameObject player, GameObject npc)
    {
        npc.GetComponent<NavMeshAgent>().destination = Vector3.MoveTowards(npc.transform.localPosition, new Vector3(waypoints[currentWaypoint].position.x, npc.transform.localPosition.y, waypoints[currentWaypoint].position.z), 3.5f);

        if (Vector3.Distance(npc.transform.localPosition, waypoints[currentWaypoint].position) < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}
