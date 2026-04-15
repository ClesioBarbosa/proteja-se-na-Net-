using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : FSMState 
{
    float timer;
    public DeadState()
    {
        stateID = FSMStateID.Dead;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (npc.GetComponent<NPCController>().health > 50)
        {
            npc.GetComponent<NPCController>().fsm.PerformTransition(FSMTransition.Revived);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        //Debug.Log("💀");
        timer += Time.deltaTime;
        if(timer >= 2.5f)
        {
            timer=0;
            npc.GetComponent<NPCController>().health+=10;
            Debug.Log("VIDA: " + npc.GetComponent<NPCController>().health);
        }
    }    
}
