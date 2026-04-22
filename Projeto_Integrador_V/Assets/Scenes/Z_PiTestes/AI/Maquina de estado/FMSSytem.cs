using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private List<FSMState> states;
    private FSMState currentState;

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    public void AddState(FSMState s)
    {
        if (s == null){
            
            Debug.LogError("FSMSystem Error: Estado nulo.");
            return;}

        states.Add(s);

        if (currentState == null) currentState = s;
    }

    public void PerformTransition(FSMTransition trans)
     {
        if (trans == FSMTransition.NullTransition){

            Debug.LogError("FSMSystem Error: Transição nula.");
            return;}

        FSMStateID id = currentState.GetOutputState(trans);

        if (id == FSMStateID.NullStateID){
            Debug.LogWarning("FSMSystem Warning: Nenhuma transição para o estado.");
            return;}

        foreach (FSMState s in states)
        {
            if (s.ID == id)
            {
                currentState = s;
                break;
            }
        }
    }

    public void Update(GameObject player, GameObject npc, bool detecao)
    {
        currentState.Reason(player, npc, detecao);
        currentState.Act(player, npc, detecao);
    }
}

