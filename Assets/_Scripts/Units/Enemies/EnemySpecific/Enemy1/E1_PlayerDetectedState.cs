using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : EnemyPlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyPlayerDetectedState stateData, Enemy1 enemy) : base(enemyEntity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        enemyEntity.SetVeolicty(0);
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("PLAYER DETECTED");
        if (performCloseRangeAction)
        {
            Debug.Log("PLAYER DETECTED 1");
            stateMachine.ChangeState(enemy.MeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            Debug.Log("PLAYER DETECTED 2");
            stateMachine.ChangeState(enemy.EnemyChargeState);
        }
        else if (!isPlayerInMinAgroRange)
        {
            Debug.Log("PLAYER DETECTED 3");
            stateMachine.ChangeState(enemy.LookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            enemyEntity.Flip();
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
