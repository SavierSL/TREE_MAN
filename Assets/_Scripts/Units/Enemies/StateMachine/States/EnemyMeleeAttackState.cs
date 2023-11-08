using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    protected D_EnemyMeleeAttackData stateData;
    protected AttackDetails attackDetails;
    protected bool isPlayerInMinAgroRange;
    public EnemyMeleeAttackState(EnemyEntity enemyEntity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_EnemyMeleeAttackData stateData) : base(enemyEntity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        attackDetails.damageAmount = stateData.attakDamage;
        attackDetails.position = enemyEntity.AliveGO.transform.position;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = enemyEntity.CheckPlayerInMinAgroRange();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Debug.Log("ENEMY ATTACK STATE MELEE");
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}
