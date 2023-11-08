using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : EnemyMoveState
{
    private Enemy1 enemy;

    public E1_MoveState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Enemy1 enemy) : base(enemyEntity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            //TODO: add idle
            enemy.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
