using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpState : HeroAbilityState
{
  public JumpState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
  {
  }
  public override void Enter()
  {
    base.Enter();

    hero.HeroCore.Movements.Jump();
    HeroCore.HeroInput.OnJumpInput(false);
    isAbilityDone = true;
    // hero.HeroCore.Movements.MobileWalk(hero.HeroInput.MoveDirection, 20f, 10f);
  }
  public override void Exit()
  {
    base.Exit();
    HeroCore.HeroInput.OnJumpInput(false);


  }
  public override void DoChecks()
  {
    base.DoChecks();
  }
  public override void LogicUpdate()
  {
    base.LogicUpdate();
    if (!HeroCore.Collisions.IsGrounded)
    {
      stateMachine.ChangeState(hero.InAirState);
    }
    isAbilityDone = true;

  }
  public override void PhysicsUpdate()
  {
    base.PhysicsUpdate();
  }

}
