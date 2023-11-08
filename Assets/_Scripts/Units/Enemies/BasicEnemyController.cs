using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }
    private State currentState;

    [SerializeField]
    private Transform
    groundCheck, touchDamageCheck,
    wallCheck, slimePos;

    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    private float
    groundCheckDistance, wallCheckDistance, movementSpeed, maxHealth, knockBackDuration, lastTouchDamagTime, touchDamageCooldown, touchDamage, touchDamageWidth, touchDamageHeight;

    private float[] attackDetails = new float[2];

    [SerializeField] private Vector2 knockBackSpeed, touchDamageBotLeft, touchDamageTopRight;

    [SerializeField] private GameObject hitParticle, deathJunkParticle, deathBloodParticle, slimeParticle;

    private float currentHealth, knockbackStartTime;



    private int facingDirection, damageDirection;
    private Vector2 movement;
    private GameObject alive;
    private Rigidbody2D aliveRB;
    private Animator aliveAnim;
    [SerializeField] float slimeTimer;
    [SerializeField] EnemyHealthbar healthbar;
    private float nextActionTime = 0.0f;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.CurrentValue(maxHealth);
        facingDirection = -1;
    }


    private bool groundDetected, wallDetected;
    private void Update()
    {

        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbacktate();
                break;
            case State.Dead:
                UpdateDeadtate();
                break;
        }
    }
    //Walking State ___________________________________________________
    private void EnterWalkingState()
    {
        nextActionTime = Time.time;
    }
    private void UpdateWalkingState()
    {

        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        CheckTouchDamage();
        if (!groundDetected || wallDetected)
        {
            //Flip
            Flip();
        }
        else
        {

            movement.Set(movementSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;

            if (Time.time > nextActionTime)
            {
                nextActionTime += slimeTimer;
                // execute block of code here
                Instantiate(slimeParticle, slimePos.transform.position, slimeParticle.transform.rotation);
            }
        }
    }

    private void ExitWalkingState() { }
    //Knockback State ___________________________________________________
    private void EnterKnockbacktate()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockBackSpeed.x * damageDirection, knockBackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnim.SetBool("knockback", true);
    }
    private void UpdateKnockbacktate()
    {
        if (Time.time >= knockbackStartTime + knockBackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbacktate()
    {
        aliveAnim.SetBool("knockback", false);
    }
    //Dead State ___________________________________________________
    private void EnterDeadtate()
    {
        //Spawn Chunks and blood
        Instantiate(deathJunkParticle, alive.transform.position, deathJunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateDeadtate() { }

    private void ExitDeadtate() { }



    //OTHER FUNCTIONS ____________________________________
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                ExitKnockbacktate();
                break;
            case State.Dead:
                ExitDeadtate();
                break;
        }
        switch (state)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                EnterKnockbacktate();
                break;
            case State.Dead:
                EnterDeadtate();
                break;
        }
        currentState = state;
    }
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
    private void Damage(float[] attackDetails)
    {
        healthbar.DecreaseHealth(attackDetails[0]);
        healthbar.Show();
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f)));
        currentHealth -= attackDetails[0];
        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit Particles
        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time > lastTouchDamagTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);
            if (hit != null)
            {
                lastTouchDamagTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}
