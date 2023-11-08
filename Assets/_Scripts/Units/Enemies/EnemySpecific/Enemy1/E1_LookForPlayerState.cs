using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : EnemyLookForPlayerState
{
    private Enemy1 enemy;
    public E1_LookForPlayerState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyLookForPlayerData stateData, Enemy1 enemy) : base(enemyEntity, stateMachine, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

}
