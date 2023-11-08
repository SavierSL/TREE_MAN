using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected EnemyEntity enemyEntity;
    protected float startTime;
    protected string animBoolName;

    public State(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, String animBoolName)
    {
        this.enemyEntity = enemyEntity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        DoChecks();
        enemyEntity.Animator.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        enemyEntity.Animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
