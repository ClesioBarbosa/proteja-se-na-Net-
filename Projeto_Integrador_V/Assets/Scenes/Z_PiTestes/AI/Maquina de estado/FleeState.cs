using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;


public class FleeState : FSMState
{
    public FleeState() {stateID = FSMStateID.Flee;}

    public override void Reason(GameObject player, GameObject npc)
    {
        if (npc.GetComponent<NPCController>().health > 50)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.RecoveredHealth);
        }

        if (Vector3.Distance(player.transform.position, npc.transform.position) > 20f)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.RecoveredHealth);
        }

        if (npc.GetComponent<NPCController>().health <= 0)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.Dying);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        Vector3 dir = npc.transform.position - player.transform.position;
        npc.GetComponent<NavMeshAgent>().destination = dir;
    }
}
