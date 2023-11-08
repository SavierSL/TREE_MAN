using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movements : HeroCore
{
    public float CurrentVelocity;
    public float timer;
    public new void LogicUpdate()
    {

    }
    // public void MobileWalk(int positionX, float speed, float wallJumpLerp)
    // {
    //     if (positionX == 0)
    //     {
    //         return;
    //     }
    //     Walk(new Vector2(positionX > 0 ? CurrentVelocity : -CurrentVelocity, 0), speed, wallJumpLerp);
    //     // foreach (Touch touch in Input.touches)
    //     // {
    //     //     if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
    //     //     {
    //     //         if (Collisions.IsGrounded)
    //     //         {
    //     //             DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
    //     //         }
    //     //         Walk(new Vector2(positionX > 0 ? CurrentVelocity : -CurrentVelocity, 0), speed, wallJumpLerp);
    //     //     }
    //     //     else
    //     //     {
    //     //         Walk(new Vector2(0, 0), speed, wallJumpLerp);
    //     //     }
    //     // }

    // }
    public void Walk(float dir, float speed, float wallJumpLerp)
    {
        // if (!HeroInput.isMoving)
        //     return;

        // if (!collisions.IsGrounded)
        // {
        //     playerRB.velocity = Vector2.Lerp(playerRB.velocity, new Vector2(dir.x * speed, playerRB.velocity.y), wallJumpLerp * Time.deltaTime);
        //     return;
        // }
        // else
        // {
        Vector2 targetVelocity = Vector2.zero; // The desired final velocity (stop)

        // Smoothly interpolate the current velocity towards the target velocity

        playerRB.velocity = new Vector2(HeroInput.inputValue * 7f, playerRB.velocity.y);



        // playerRB.velocity = Vector2.Lerp(playerRB.velocity, new Vector2(dir * speed, playerRB.velocity.y), wallJumpLerp * Time.deltaTime);
        // }

    }
    public void RigidbodyDrag(float x)
    {
        playerRB.drag = x;
    }
    public void Jump()
    {
        // if (!collisions.IsGrounded)
        // {
        //     return;
        // }
        // DOVirtual.Float(0.5f, 0, 0.8f, RigidbodyDrag);
        // playerRB.velocity = Vector2.zero;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        playerRB.velocity += Vector2.up * 15;
    }
}
