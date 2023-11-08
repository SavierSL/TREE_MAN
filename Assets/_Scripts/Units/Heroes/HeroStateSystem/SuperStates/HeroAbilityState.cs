using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAbilityState : HeroState
{
    protected bool isGrounded;
    protected bool isAbilityDone;


    public HeroAbilityState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = hero.HeroCore.Collisions.IsGrounded;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (isGrounded && hero.playerRB.velocity.y < 0.01f && !hero.HeroInput.ShouldJump)
            {
                stateMachine.ChangeState(hero.IdleState);
            }
            // else
            // {
            //     stateMachine.ChangeState(hero.InAirState);
            // }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
