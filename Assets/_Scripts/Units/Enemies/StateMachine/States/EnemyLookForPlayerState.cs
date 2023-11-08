using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookForPlayerState : State
{
    protected D_EnemyLookForPlayerData stateData;
    protected bool turnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    protected float lastTurnTime;
    protected int amountOfTurnsDone;
    public EnemyLookForPlayerState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyLookForPlayerData stateData) : base(enemyEntity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        enemyEntity.SetVeolicty(0);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (turnImmediately)
        {
            enemyEntity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            enemyEntity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }
        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }
        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
    }
    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
