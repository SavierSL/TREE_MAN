using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping : MonoBehaviour
{
  [SerializeField] Rigidbody2D playerRigidBody;
  public float jumpMultiplier = 2.5f;
  public float fallMultiplier = 2f;

  // Update is called once per frame
  void Update()
  {
    if (playerRigidBody.velocity.y < 0)
    {
      playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
    else if (playerRigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
    {
      playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
    }
  }
}
