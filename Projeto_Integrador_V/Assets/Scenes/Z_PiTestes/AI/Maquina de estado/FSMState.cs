using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public enum FSMStateID
    {
    NullStateID,
    Patrol,
    Chase,
    Attack,
    Happy
    }

public enum FSMTransition
{
    NullTransition,
    SawPlayer,
    LostPlayer,
    CloseCombat,
    HitPlayer,
    }
    
public abstract class FSMState
{ 
    protected Dictionary<FSMTransition, FSMStateID> map = new Dictionary<FSMTransition, FSMStateID>();
    protected FSMStateID stateID;
    public FSMStateID ID => stateID;

    public virtual void Reason(GameObject player, GameObject npc, bool detectao) { }
    public virtual void Act(GameObject player, GameObject npc, bool detectao) { }
    
    public void AddTransition(FSMTransition trans, FSMStateID id)
    {
        if (trans == FSMTransition.NullTransition || id == FSMStateID.NullStateID){
            Debug.LogError("FSMState Error: Transição ou Estado Nulo.");
            return;}
        
        if (map.ContainsKey(trans)){
            Debug.LogError("FSMState Error: Transição já adicionada.");
            return;}
            
    map.Add(trans, id);
    }
    
    public FSMStateID GetOutputState(FSMTransition trans)
    {
        if (map.ContainsKey(trans)){
            return map[trans];
        }

        return FSMStateID.NullStateID;
    }
}