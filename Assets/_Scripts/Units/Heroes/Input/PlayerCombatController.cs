using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer, attackRadius, attack1Dmg;
    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;

    private HeroCore heroCore;
    private bool gottInput, isAttacking, isFirstAttack;
    private float lastInputTime = Mathf.NegativeInfinity; //always ready to atk
    private bool firstAttackButton;
    private AttackDetails attackDetails;
    private PlayerStats playerStats;


    [SerializeField] Animator animator;

    private void Start()
    {

        animator.SetBool("canAttack", combatEnabled);
        heroCore = GetComponent<HeroCore>();
        playerStats = GetComponent<PlayerStats>();
        firstAttackButton = true;
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacts();
        //last input = 1
        //time.time = 1.2
        //inputtimer = .2


    }
    private void CheckCombatInput()
    {
        if (FindAnyObjectByType<Hero>().HeroCore.HeroInput.attackClick)
        {
            if (lastInputTime <= Time.time - inputTimer)
            {
                firstAttackButton = true;
            }
            else
            {
                firstAttackButton = false;
            }

            //combat button
            if (combatEnabled)
            {
                gottInput = true;
                lastInputTime = Time.time;
            }
        }

    }
    private void CheckAttacts()
    {
        if (gottInput)
        {
            //Perform attack
            if (!isAttacking)
            {
                gottInput = false;
                isAttacking = true;

                isFirstAttack = !isFirstAttack;
                if (firstAttackButton)
                {
                    isFirstAttack = true;
                }
                float atkCap = lastInputTime + inputTimer;
                Debug.Log("atkCap" + atkCap);
                Debug.Log("Time.time" + Time.time);

                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
                CheckAttakHitBox();
            }
        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            //wait for new input

            gottInput = false;

        }
    }
    private void CheckAttakHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsDamageable);
        attackDetails.damageAmount = attack1Dmg;
        attackDetails.position = transform.position;
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }
    public void FinishAttack1()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1", false);
    }

    public void Damage(AttackDetails attackDetails)
    {
        playerStats.DecreaseHealth(attackDetails.damageAmount);
        int direction;
        if (attackDetails.position.x <= FindAnyObjectByType<Hero>().transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        FindAnyObjectByType<TreeMan>().HeroCore.Collisions.Knockback(direction);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }

}
