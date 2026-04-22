using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyState : FSMState
{
    bool atacou;
    public HappyState() { stateID = FSMStateID.Happy;}

    public override void Reason(GameObject player, GameObject npc, bool detectao)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) > 1f)
        {
            atacou=false;
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LostPlayer);
        }

        if (atacou==true)
        {
            npc.GetComponent<Rigidbody>().AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
        }
    }

    public override void Act(GameObject player, GameObject npc, bool detectao)
    {
        if(!atacou)
        {
            npc.transform.LookAt(player.transform);
            Vector3 dirKnock = (npc.transform.position - player.transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(dirKnock * 50 * -1,ForceMode.Impulse);
            atacou=true;
        }   
    }
}
