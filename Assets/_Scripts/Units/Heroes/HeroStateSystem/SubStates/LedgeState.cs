using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class LedgeState : HeroState
{
    Vector2 detectedPos;
    private Vector2 workspace;


    public LedgeState(Hero hero, HeroStateMachine stateMachine, MovementData movementData, string animBoolName) : base(hero, stateMachine, movementData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("LEDGE STATE");
        HeroCore.Collisions.CheckLedge();
        // hero.HeroCore.Collisions.DetermineCornerPosition();
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
        hero.HeroCore.Collisions.DetermineCornerPosition();
        HeroCore.Collisions.CheckLedge();
        hero.CheckIfShouldFlip(hero.HeroInput.inputValue);
        Debug.Log(hero.HeroCore.Collisions.LedgeDetected + "LEDGE CHECK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        if (animationFinishTrigger)
        {
            stateMachine.ChangeState(hero.IdleState);
        }


    }

    public void SetDetectedPos(Vector2 pos) => detectedPos = pos;


}
