using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IdleState : HeroGroundState
{
    public IdleState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
        hero.HeroCore.Movements.CurrentVelocity = 0f;

        hero.HeroCore.Movements.timer = 0f;
        // hero.playerRB.drag = 7f;
        if (hero.HeroCore.Collisions.IsGrounded && !JumpInput)
        {
            DOVirtual.Float(7f, 0, .3f, RigidbodyDrag);
        }


    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        hero.HeroCore.Collisions.CheckKnockback();
        if (hero.HeroInput.isMoving)
        {
            if (HeroCore.Collisions.IsGrounded)
            {
                stateMachine.ChangeState(hero.MoveState);
            }
        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void RigidbodyDrag(float x)
    {
        HeroCore.Movements.playerRB.drag = x;
    }
}
