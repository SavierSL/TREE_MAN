using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyEntity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E1_EnemyChargeState EnemyChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }
    public E1_MeleeAttackState MeleeAttackState { get; private set; }
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private D_EnemyIdleState idleStateData;
    [SerializeField] private D_EnemyMoveState moveStateData;
    [SerializeField] private D_EnemyPlayerDetectedState playerDetectedData;
    [SerializeField] private D_EnemyChargeState enemyChargeData;
    [SerializeField] private D_EnemyLookForPlayerData enemyLookForPlayerData;

    [SerializeField] private D_EnemyMeleeAttackData enemyMeleeAttackData;

    public override void Start()
    {
        base.Start();
        MoveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "detect", playerDetectedData, this);
        EnemyChargeState = new E1_EnemyChargeState(this, stateMachine, "charge", enemyChargeData, this);
        LookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "look", enemyLookForPlayerData, this);
        MeleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, enemyMeleeAttackData, this);
        stateMachine.Initialize(MoveState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, enemyMeleeAttackData.attackRadius);



    }
}
