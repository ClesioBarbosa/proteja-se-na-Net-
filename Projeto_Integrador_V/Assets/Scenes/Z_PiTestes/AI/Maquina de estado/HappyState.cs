using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyState : FSMState
{
    public HappyState() { stateID = FSMStateID.Happy;}

    public override void Reason(GameObject player, GameObject npc, bool detectao)
    {
        
    }

    public override void Act(GameObject player, GameObject npc, bool detectao)
    {
       
    }
}
