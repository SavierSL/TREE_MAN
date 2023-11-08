using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState
{
    protected HeroStateMachine stateMachine;
    protected MovementData movementData;
    protected HeroCore HeroCore;
    protected Hero hero;
    protected string animBoolName;
    protected bool animationFinishTrigger;
    public HeroState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName)
    {
        this.hero = hero;
        this.stateMachine = stateMachine;
        this.movementData = movementData;
        this.animBoolName = animBoolName;
        HeroCore = hero.HeroCore;
    }



    public virtual void Enter()
    {

        DoChecks();
        hero.Animator.SetBool(animBoolName, true);
        animationFinishTrigger = false;
    }
    public virtual void Exit()
    {

        hero.Animator.SetBool(animBoolName, false);
    }
    public virtual void DoChecks() { }
    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => animationFinishTrigger = true;
}
