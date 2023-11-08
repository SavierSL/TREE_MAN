using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroGroundState : HeroState
{
    private bool isGrounded;
    protected bool JumpInput;

    public HeroGroundState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }
    public override void Enter()
    {
        base.Enter();
        HeroCore.HeroInput.OnJumpInput(false);
    }
    public override void Exit()
    {
        base.Exit();
        HeroCore.HeroInput.OnJumpInput(false);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isGrounded = hero.HeroInput.ShouldJump;
        JumpInput = hero.HeroInput.ShouldJump;
        if (JumpInput && isGrounded)
        {
            Debug.Log("GROUND JUMP");
            HeroCore.Movements.playerRB.drag = 0;
            stateMachine.ChangeState(hero.JumpState);
            HeroCore.HeroInput.OnJumpInput(false);
        }
        // if (!HeroCore.Collisions.IsGrounded) //HeroCore.Movements.playerRB.velocity.y < 0 &&
        // {
        //     stateMachine.ChangeState(hero.InAirState);
        // }
    }
}
