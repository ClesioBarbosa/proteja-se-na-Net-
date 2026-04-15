 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    public ChaseState()
    {
        stateID = FSMStateID.Chase;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) > 7f){

            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LostPlayer);
        }

        if(npc.GetComponent<NPCController>().health <= 50)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LowHealth);
        }

        if(Vector3.Distance(player.transform.position, npc.transform.position) < 1f)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.CloseCombat);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        npc.transform.LookAt(player.transform);
        npc.GetComponent<NavMeshAgent>().destination = player.transform.position;
    }
}
