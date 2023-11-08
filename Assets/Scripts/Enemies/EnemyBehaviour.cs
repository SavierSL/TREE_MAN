using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
  Animator animator;
  [SerializeField] Enemy enemy;
  private float attackCooldown = 2;
  private Rigidbody2D enemyRB;
  [Space]
  [Header("EnemyRange")]
  [SerializeField] Transform enemyAggroRange;
  [SerializeField] Vector2 range;
  [SerializeField] LayerMask layerCollider;
  [SerializeField] LayerMask wallLayer;

  [SerializeField] SpriteRenderer EnemySpriteRenderer;
  [SerializeField] Material flashMaterial;
  [SerializeField] Material originalMaterial;
  [SerializeField] float flashDuration;
  [SerializeField] Transform textDMGPoint;
  private int flipSide = -1;


  public Vector2 fontDetector;
  public float collidersRadius;


  private float coolDownTimer;
  private float currentHP;



  private void Start()
  {
    currentHP = enemy.enemyHP;
    coolDownTimer = Mathf.Infinity;
    animator = GetComponent<Animator>();
    enemyRB = GetComponent<Rigidbody2D>();
  }
  private void Update()
  {


    coolDownTimer += Time.deltaTime;
    if (PlayerInsight())
    {
      if (coolDownTimer >= attackCooldown)
      {
        coolDownTimer = 0;
        animator.SetTrigger("Enemy_Attack");
      }

    }
    // Patrol();
  }
  private bool PlayerInsight()
  {
    Collider2D hit = Physics2D.OverlapBox(enemyAggroRange.transform.position, GetTransformSize(enemyAggroRange.transform), 0, layerCollider);

    return hit != null;
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(enemyAggroRange.transform.position, range);
    Gizmos.DrawWireSphere((Vector2)transform.position + fontDetector, collidersRadius);
  }
  private void FlipSprite()
  {

    bool hitfontDetector = Physics2D.OverlapCircle((Vector2)transform.position + fontDetector, collidersRadius, wallLayer);
    if (hitfontDetector)
    {

      transform.localScale = new Vector3(flipSide * -1, 1, 1);
      fontDetector = new Vector2(fontDetector.x * -1, fontDetector.y);
      flipSide *= -1;

    }
  }
  private Vector2 GetTransformSize(Transform transform)
  {
    Vector3 scale = transform.localScale;
    Vector2 size = new Vector2(scale.x, scale.y);
    return size;
  }
  private void Patrol()
  {
    //Patrol need the rigidbody 
    enemyRB.transform.position += new Vector3((flipSide * 2) * Time.deltaTime, 0);
    Debug.Log(flipSide);

    FlipSprite();

    //Direction to move
  }
  public void TakeDamage(float damage)
  {
    Instantiate(enemy.textDMG, textDMGPoint.position, Quaternion.identity);
    currentHP -= damage;
    StartCoroutine(Flash());
  }
  public IEnumerator Flash()
  {
    EnemySpriteRenderer.material = flashMaterial;
    yield return new WaitForSeconds(flashDuration);
    EnemySpriteRenderer.material = originalMaterial;
    yield return new WaitForSeconds(flashDuration);
    EnemySpriteRenderer.material = originalMaterial;
    yield return new WaitForSeconds(flashDuration);
    EnemySpriteRenderer.material = originalMaterial;
  }



}
