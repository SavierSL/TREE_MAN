using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : HeroCore
{
    public HeroCore HeroCore { get; private set; }
    public HeroStateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public InAirState InAirState { get; private set; }
    public LandState LandState { get; private set; }
    public LedgeState LedgeState { get; private set; }
    public HeroInput HeroInput { get; private set; }
    public Animator Animator { get; private set; }
    public int FacingDirection { get; private set; } = 1;

    public bool isDead;

    public Stats Stats { get; private set; }
    public virtual void SetStats(Stats stats) => Stats = stats;

    [SerializeField] readonly MovementData movementData;
    private float directionXaxis;

    public virtual void Awake()
    {
        HeroCore = GetComponent<HeroCore>();
        playerRB = GetComponent<Rigidbody2D>();
        StateMachine = new HeroStateMachine();
        IdleState = new IdleState(this, StateMachine, movementData, "isIdle");
        MoveState = new MoveState(this, StateMachine, movementData, "isRunning");
        JumpState = new JumpState(this, StateMachine, movementData, "inAir");
        InAirState = new InAirState(this, StateMachine, movementData, "isAir");
        LandState = new LandState(this, StateMachine, movementData, "isLanding");
        LedgeState = new LedgeState(this, StateMachine, movementData, "isClimbing");
    }
    public virtual void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        HeroInput = GetComponent<HeroInput>();
        StateMachine.Initialize(IdleState);


    }

    void Update()
    {
        directionXaxis = Input.GetAxis("Horizontal");

        StateMachine.CurrentState.LogicUpdate();
        HeroCore.LogicUpdate();

        StateMachine.CurrentState.LogicUpdate();

    }

    public void CheckIfShouldFlip(float xInput)
    {
        int intFlip = xInput > 0 ? 1 : -1;
        if (xInput != 0 && intFlip != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        playerRB.transform.localScale = new Vector3(FacingDirection, 1, 1);
    }
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    public void SetDead() => isDead = true;
}