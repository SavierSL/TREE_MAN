using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public D_EnemyEntity enemyEntityData;
    public FiniteStateMachine stateMachine;
    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject AliveGO { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 velocityWorkspace;
    private float currentHealth;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected Transform minAgroCheck;
    [SerializeField] protected Transform maxAgroCheck;
    [SerializeField] protected Transform closeAgroCheck;
    public AnimationToStateMachine Atsm { get; private set; }
    private int lastDamageDirection;


    public virtual void Start()
    {
        currentHealth = enemyEntityData.maxHealth;
        FacingDirection = 1;
        stateMachine = new FiniteStateMachine();
        AliveGO = transform.Find("Alive").gameObject;
        RB = AliveGO.GetComponent<Rigidbody2D>();
        Animator = AliveGO.GetComponent<Animator>();
        Atsm = AliveGO.GetComponent<AnimationToStateMachine>();
    }

    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        Debug.Log(AliveGO.transform);
        Debug.Log(AliveGO.name);
    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVeolicty(float velocity)
    {
        velocityWorkspace.Set(FacingDirection * velocity, RB.velocity.y);
        RB.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, enemyEntityData.wallCheckDistance, enemyEntityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, enemyEntityData.ledgeCheckDistance, enemyEntityData.whatIsGround);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, enemyEntityData.minAgroDistance, enemyEntityData.whatIsPlayer);
    }
    public virtual bool CHeckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, enemyEntityData.maxAgroDistance, enemyEntityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, enemyEntityData.closeRangeActionDistance, enemyEntityData.whatIsPlayer);
    }
    public virtual void OnDrawGizmos()
    {

        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * enemyEntityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * enemyEntityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(new Vector3(closeAgroCheck.position.x + enemyEntityData.closeRangeActionDistance * FacingDirection, closeAgroCheck.position.y, closeAgroCheck.position.z), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(minAgroCheck.position.x + enemyEntityData.minAgroDistance * FacingDirection, minAgroCheck.position.y, minAgroCheck.position.z), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(maxAgroCheck.position.x + enemyEntityData.maxAgroDistance * FacingDirection, maxAgroCheck.position.y, maxAgroCheck.position.z), 0.2f);

    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(RB.velocity.x, velocity);
        RB.velocity = velocityWorkspace;
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        DamageHop(enemyEntityData.damageHopSpeed);
        currentHealth -= attackDetails.damageAmount;
        if (attackDetails.position.x > AliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
    }
}


// public override void Enter()
// {
//     base.Enter();
// }
// public override void Exit()
// {
//     base.Exit();
// }
// public override void LogicUpdate()
// {
//     base.LogicUpdate();
// }
// public override void PhysicsUpdate()
// {
//     base.PhysicsUpdate();
// }
// public override void DoChecks()
// {
//     base.DoChecks();
// }