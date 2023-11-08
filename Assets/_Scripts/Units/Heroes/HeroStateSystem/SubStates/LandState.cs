using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : HeroGroundState
{
    public LandState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("LANDED NA");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (hero.HeroInput.isMoving)
        {
            Debug.Log("LANDED NA DALAGAN NA");
            stateMachine.ChangeState(hero.MoveState);
        }
        if (animationFinishTrigger)
        {
            stateMachine.ChangeState(hero.IdleState);
        }

    }
}
