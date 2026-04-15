using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public FSMSystem fsm;
    public int health = 100;
    [SerializeField] Transform [] patrolPoints;
    
    private void Start()
    {
        InitFSM();
    }
    
    private void InitFSM()
    {
        // === Estado: Patrulha ===
        fsm = new FSMSystem();
        PatrolState patrol = new PatrolState(patrolPoints);
        fsm.AddState(patrol);
        patrol.AddTransition(FSMTransition.SawPlayer, FSMStateID.Chase);

        // === Estado: Perseguir ===
        ChaseState chase = new ChaseState();
        fsm.AddState(chase);
        chase.AddTransition(FSMTransition.LostPlayer, FSMStateID.Patrol);
        chase.AddTransition(FSMTransition.LowHealth, FSMStateID.Flee);
        chase.AddTransition(FSMTransition.RecoveredHealth, FSMStateID.Patrol);
        chase.AddTransition(FSMTransition.CloseCombat, FSMStateID.Attack);

        // === Estado: Atacar ===
        AttackState attack = new AttackState();
        fsm.AddState(attack);
        attack.AddTransition(FSMTransition.LowHealth, FSMStateID.Flee);
        attack.AddTransition(FSMTransition.LostPlayer, FSMStateID.Chase);

        // === Estado: Fugir ===
        FleeState flee = new FleeState();
        fsm.AddState(flee);
        flee.AddTransition(FSMTransition.RecoveredHealth, FSMStateID.Patrol);
        flee.AddTransition(FSMTransition.Dying, FSMStateID.Dead);
        flee.AddTransition(FSMTransition.LostPlayer, FSMStateID.Patrol);

        DeadState dead = new DeadState();
        fsm.AddState(dead);
        dead.AddTransition(FSMTransition.Revived, FSMStateID.Patrol);
    }
    
    private void Update()
    {
        if (fsm == null) return;
        if (health < 0) health = 0;
            

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            fsm.Update(player, this.gameObject);
        }
    }
}