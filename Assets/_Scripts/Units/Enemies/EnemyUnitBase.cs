
using UnityEngine;

public abstract class EnemyUnitBase : EnemyUnit
{
  protected bool _canMove;
  public float enemyRadius;
  public float enemyAtkRadius;
  public Transform enemyAttackRange;
  public LayerMask playerLayer;
  public float enemeyAttackIndicator1, enemeyAttackIndicator2, enemeyAttackIndicator3;
  public bool isFlipped;
  public Animator enemyAnimator;
  public float coolDownTimer;
  public float attackCooldown = 2f;
  public float health;
  public float currentHealth;

  private void Update()
  {

  }
  public virtual void ExecuteMove()
  {

  }

  public virtual void TrackPlayer()
  {

    Collider2D[] hit = Physics2D.OverlapCircleAll(enemyAttackRange.transform.position, enemyRadius, playerLayer);
    foreach (Collider2D player in hit)
    {

      if (player.gameObject.name == "TreeMan(Clone)")
      {
        float step = 5f * Time.deltaTime;

        Transform playerPos = FindAnyObjectByType<TreeMan>().transform;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.transform.position.x, transform.position.y), step);
      }
    }
  }

  public virtual void AttackPlayer()
  {
    coolDownTimer += Time.deltaTime;
    Collider2D[] hit = Physics2D.OverlapCircleAll(enemyAttackRange.transform.position, enemyAtkRadius, playerLayer);
    foreach (Collider2D player in hit)
    {

      if (player.gameObject.name == "TreeMan(Clone)")
      {
        if (coolDownTimer >= attackCooldown)
        {
          coolDownTimer = 0;
          enemyAnimator.SetTrigger("Enemy_Attack");
        }
      }
    }
  }

  public virtual void LookAtPlayer()
  {
    Transform playerPosition = FindAnyObjectByType<TreeMan>().transform;
    if (playerPosition.position.x > transform.position.x)
    {
      transform.localScale = new Vector3(-1, 1, 1);
    }
    else
    {
      transform.localScale = new Vector3(1, 1, 1);
    }
  }

  public virtual void SetHealth(float setHealth)
  {
    health = setHealth;
    currentHealth = setHealth;
    FindAnyObjectByType<Healthbar>().SetHealth(setHealth);
    FindAnyObjectByType<Healthbar>().CurrentValue(setHealth);
  }

  public virtual void TakeDamage(float damage)
  {
    FindAnyObjectByType<Healthbar>().DecreaseHealth(damage);
  }
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(enemyAttackRange.transform.position, enemyRadius);
    Gizmos.DrawWireSphere(enemyAttackRange.transform.position, enemyAtkRadius);
  }
}
