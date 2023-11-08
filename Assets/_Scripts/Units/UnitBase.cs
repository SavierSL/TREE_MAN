using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will share logic for any unit on the field. Could be friend or foe, controlled or not.
/// Things like taking damage, dying, animation triggers etc
/// </summary>
public class UnitBase : Hero
{
  [Header("Collision Layer")]
  [SerializeField] LayerMask collisionLayer;
  [Space]
  [Header("Booleans")]
  public bool isGrounded;
  public bool isRightWall;
  public bool isLeftWall;
  public bool onWall;
  public int wallSide;
  public Stats Stats { get; private set; }

  [Header("Collision")]
  public float collisionRadius = 0.25f;
  [SerializeField] Vector2 bottomOffSet, rightOffset, leftOffset;
  [SerializeField] Color debugCollisionColliderColor = Color.red;

  public virtual void SetStats(Stats stats) => Stats = stats;

  public virtual void TakeDamage(int dmg)
  {

  }
  public void CheckCollision()
  {
    isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffSet, collisionRadius, collisionLayer);
    isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, collisionLayer);
    isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, collisionLayer);
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = debugCollisionColliderColor;
    Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffSet, collisionRadius);
    Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
    Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
  }
}