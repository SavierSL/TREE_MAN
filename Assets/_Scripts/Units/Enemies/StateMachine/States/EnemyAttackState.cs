using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;
    public EnemyAttackState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(enemyEntity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }
    public override void Enter()
    {
        base.Enter();
        enemyEntity.Atsm.attackState = this;
        isAnimationFinished = false;
        enemyEntity.SetVeolicty(0f);
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
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
    }
    public virtual void TriggerAttack()
    {

    }
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
