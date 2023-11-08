using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InAirState : HeroState
{
    public InAirState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        // stateMachine.ChangeState(hero.MoveState);

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("IN AIR STATE ENTER");
        HeroCore.HeroInput.OnJumpInput(false);


    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("IN AIR STATE EXIT");
        HeroCore.HeroInput.OnJumpInput(false);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        hero.HeroCore.Movements.Walk(hero.HeroInput.inputValue, 8f, 20f);
        hero.HeroCore.Collisions.DetermineCornerPosition();
        if (HeroCore.Collisions.IsGrounded)
        {
            Debug.Log("IdleState NOW");
            stateMachine.ChangeState(hero.LandState);
        }
        else if (HeroCore.Collisions.WallCollided && HeroCore.Collisions.LedgeDetected)
        {
            Debug.Log("LEDGE NOW");
            stateMachine.ChangeState(hero.LedgeState);
        }
        else
        {
            Debug.Log("AIRING NA");
            hero.Animator.SetFloat("yVelocity", hero.HeroInput.currentVelocity.y);
            hero.Animator.SetFloat("xVelocity", hero.HeroInput.currentVelocity.x);
        }

        // else
        // {
        hero.CheckIfShouldFlip(hero.HeroInput.inputValue);
        // hero.HeroCore.Movements.MobileWalk(hero.HeroInput.MoveDirection, 20f, 10f);
        // }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
