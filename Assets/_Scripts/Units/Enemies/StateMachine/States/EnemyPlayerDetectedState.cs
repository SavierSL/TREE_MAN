using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : State
{
    protected D_EnemyPlayerDetectedState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;
    public EnemyPlayerDetectedState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyPlayerDetectedState stateData) : base(enemyEntity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.actionTime)
        {
            performLongRangeAction = true;
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
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyEntity.CHeckPlayerInMaxAgroRange();
        performCloseRangeAction = enemyEntity.CheckPlayerInCloseRangeAction();
    }


}
