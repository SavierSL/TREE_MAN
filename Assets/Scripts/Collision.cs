using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
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


  [Header("Collision")]
  public float collisionRadius = 0.25f;
  public Vector2 bottomOffSet, rightOffset, leftOffset;
  public Color debugCollisionColliderColor = Color.red;

  private void Update()
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
