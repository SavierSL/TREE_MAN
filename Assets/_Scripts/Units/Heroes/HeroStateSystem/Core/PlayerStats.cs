using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;

    private float currentHealth;
    private HeroCore heroCore;
    private ExampleGameManager exampleGameManager;
    private Healthbar healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar = FindAnyObjectByType<Healthbar>();
    }
    private void OnEnable()
    {
        heroCore = FindAnyObjectByType<HeroCore>();
        exampleGameManager = GameObject.Find("Managers").transform.Find("Example Game Manager").gameObject.GetComponent<ExampleGameManager>();
    }
    public void SetHealth()
    {
        healthbar.SetHealth(maxHealth);
        Debug.Log("SET HEALTH NA!");
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        healthbar.DecreaseHealth(amount);
        if (currentHealth < 0.0f)
        {
            Die();
            exampleGameManager.ChangeState(GameState.RespawningHeroes);
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
}
