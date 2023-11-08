using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveState : HeroState
{
    public MoveState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HeroCore.HeroInput.OnJumpInput(false);
    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        hero.HeroCore.Collisions.CheckKnockback();
        hero.CheckIfShouldFlip(hero.HeroInput.inputValue);
        hero.HeroCore.Movements.Walk(hero.HeroInput.inputValue, 8f, 20f);
        if (hero.HeroInput.ShouldJump && HeroCore.Collisions.IsGrounded)
        {
            Debug.Log("MOVE JUMP");
            stateMachine.ChangeState(hero.JumpState);
            HeroCore.HeroInput.OnJumpInput(false);
        }

        if (!hero.HeroInput.isMoving && HeroCore.Collisions.IsGrounded)
        {
            stateMachine.ChangeState(hero.IdleState);
        }
        if (!HeroCore.Collisions.IsGrounded)
        {
            stateMachine.ChangeState(hero.InAirState);
        }
    }

}

