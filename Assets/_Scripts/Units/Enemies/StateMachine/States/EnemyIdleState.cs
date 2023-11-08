using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    protected D_EnemyIdleState stateData;
    protected bool flipAfterIdle;
    protected float IdleTime;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;

    public EnemyIdleState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData) : base(enemyEntity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        enemyEntity.SetVeolicty(0);
        isIdleTimeOver = false;
        SetRandomIdleTime();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();

    }
    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            enemyEntity.Flip();
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + IdleTime)
        {
            isIdleTimeOver = true;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        IdleTime = Random.Range(stateData.middleTime, stateData.maxIdleTime);
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
    }
}
