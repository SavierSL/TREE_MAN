using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : State
{
    protected D_EnemyChargeState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    public EnemyChargeState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyChargeState stateData) : base(enemyEntity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        isDetectingLedge = enemyEntity.CheckLedge();
        isDetectingWall = enemyEntity.CheckWall();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
        enemyEntity.SetVeolicty(stateData.chargeSpeed);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
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
        performCloseRangeAction = enemyEntity.CheckPlayerInCloseRangeAction();
    }

}
