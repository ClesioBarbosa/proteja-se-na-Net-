using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    bool atacou;
    float timer;
    public AttackState() { stateID = FSMStateID.Attack;}

    public override void Reason(GameObject player, GameObject npc)
    {
        if (npc.GetComponent<NPCController>().health <= 50)
        {
            atacou=false;
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LowHealth);
        }
        
        if (Vector3.Distance(player.transform.position, npc.transform.position) > 1f)
        {
            atacou=false;
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.LostPlayer);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        if(!atacou)
        {
            npc.transform.LookAt(player.transform);
            //npc.transform.Translate(Vector3.forward * Time.deltaTime * 3f);
            Vector3 dirKnock = (npc.transform.position - player.transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(dirKnock * 150 * -1,ForceMode.Impulse);
            atacou=true;
        }   
    }
}
