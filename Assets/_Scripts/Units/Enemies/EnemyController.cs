using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTourque;
    [SerializeField] GameObject particleEffect;
    private float currentHealth, knockbackStart;
    private HeroCore heroCore;

    private Animator animatorAliveAnim;
    private GameObject aliveGO, brokenTopGO, brokenBottomGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBottom;
    private int playerFacingDirection;
    private bool playerOnLeft, knockback;
    public bool applyKnockback;
    private void Start()
    {
        currentHealth = maxHealth;
        heroCore = FindAnyObjectByType<HeroCore>();
        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("BrokenTop").gameObject;
        brokenBottomGO = transform.Find("BrokenBottom").gameObject;
        animatorAliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBottom = brokenBottomGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBottomGO.SetActive(false);

        Debug.Log(brokenBottomGO.name + "brokenBottomGO.name");
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        CheckKnockback();

        playerFacingDirection = FindAnyObjectByType<TreeMan>().FacingDirection;
    }

    private void Damage(float[] details)
    {
        Instantiate(particleEffect, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f)));
        animatorAliveAnim.SetTrigger("damage");
        currentHealth -= details[0];
        if (details[1] < aliveGO.transform.position.x)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }


        if (applyKnockback && currentHealth > 0.0f)
        {

            Knockback();
        }
        else if (currentHealth < 0.0f)
        {
            Die();
        }
    }
    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        Debug.Log("playerFacingDirection" + playerFacingDirection);

        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        Debug.Log("rbAlive.velocity" + rbAlive.velocity);
    }
    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenBottomGO.SetActive(true);
        brokenTopGO.SetActive(true);

        brokenTopGO.transform.position = new Vector2(aliveGO.transform.position.x + 2.0f, aliveGO.transform.position.y);
        brokenBottomGO.transform.position = aliveGO.transform.position;

        rbBrokenBottom.velocity = new Vector2(knockbackDeathSpeedX - 0.07f * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTourque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
