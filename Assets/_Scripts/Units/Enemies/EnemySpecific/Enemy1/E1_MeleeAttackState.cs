using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : EnemyMeleeAttackState
{
    private Enemy1 enemy;
    public E1_MeleeAttackState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_EnemyMeleeAttackData stateData, Enemy1 enemy) : base(enemyEntity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT ATTACK");
    }
    public override void LogicUpdate()
    {
        Debug.Log("AAAA" + isAnimationFinished);
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.PlayerDetectedState);
                Debug.Log("EXIT ATTACK 1");
            }
            else
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
                Debug.Log("EXIT ATTACK 2");
            }
        }
        else
        {
            stateMachine.ChangeState(enemy.LookForPlayerState);
            Debug.Log("EXIT ATTACK 2");
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
