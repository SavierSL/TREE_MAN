using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : State
{
    protected D_EnemyMoveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;
    public EnemyMoveState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData) : base(enemyEntity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        enemyEntity.SetVeolicty(stateData.movementSpeed);
        isDetectingLedge = enemyEntity.CheckLedge();
        isDetectingWall = enemyEntity.CheckWall();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = enemyEntity.CheckLedge();
        isDetectingWall = enemyEntity.CheckWall();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
    }


}
