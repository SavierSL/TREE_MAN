using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Collisions : HeroCore
{
    [Header("Collision Layer")]
    public LayerMask collisionLayer;
    [Space]

    [Header("Booleans")]
    public bool isGrounded;
    // private bool isRightWall;
    // private bool isLeftWall;
    private bool wallCollided;
    private int wallSide;
    private bool isLedgeHit;
    private bool canDetected, knockback;
    private bool ledgeDetected;
    private bool cangrabLedge = true;
    private bool canClimb;
    private Vector2 climbBegunPos;
    private Vector2 climbOverPos;

    private float knockbackStartTime;
    [SerializeField] private float knockbackDuration;



    [Header("Ledge Info")]

    public Vector2 offset1;
    public Vector2 offset2;

    [Header("Collision")]
    [SerializeField] Vector3 collisionWallSize;
    [SerializeField] Vector2 knockbackSpeed;


    [SerializeField] Vector3 collisionGroundSize;
    [SerializeField] float collisionLedgeRadius;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform ledgeCheck;
    [SerializeField] Transform bottomOffSet;
    [SerializeField] Color debugCollisionColliderColor = Color.red;

    public void CheckCollision()
    {
        isGrounded = Physics2D.BoxCast(bottomOffSet.transform.position, collisionGroundSize, 0, Vector2.zero, 0, collisionLayer);
        wallCollided = Physics2D.BoxCast(wallCheck.transform.position, collisionWallSize, 0, Vector2.zero, 0, collisionLayer);
        // isLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, collisionWallSize, collisionLayer);
        // isLedgeHit = Physics2D.OverlapCircle((Vector2)transform.position + ledgeCheck, collisionLedgeRadius, collisionLayer);

    }
    public bool IsGrounded
    {
        get => isGrounded;
    }
    // public bool IsRightWall
    // {
    //     get => isRightWall;
    // }
    public bool WallCollided
    {
        get => wallCollided;
    }
    public bool IsLedgeHit
    {
        get => isLedgeHit;
    }
    public bool CanDetected
    {
        get => canDetected;
    }
    public bool LedgeDetected
    {
        get => ledgeDetected;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColliderColor;
        Gizmos.DrawWireCube(wallCheck.transform.position, collisionWallSize);
        Gizmos.DrawWireCube(bottomOffSet.transform.position, collisionGroundSize);
        Gizmos.DrawWireSphere(ledgeCheck.transform.position, collisionLedgeRadius);
        // Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, collisionWallSize);

        // Gizmos.DrawWireSphere((Vector2)transform.position + wallCheck, collisionRadius);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Collider"))
        {
            canDetected = false;
            Debug.Log("ENTER LEDGE!!!!!!!!!!!!!!!!!!_________________________________" + canDetected);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Collider"))
        {
            canDetected = true;
            Debug.Log("EXIT LEDGE!!!!!!!!!!!!!!!!!!_________________________________" + canDetected);
        }
    }
    public void DetermineCornerPosition()
    {
        if (canDetected)
        {
            ledgeDetected = Physics2D.OverlapCircle(ledgeCheck.transform.position, collisionLedgeRadius, collisionLayer);
        }



        // if (ledgeDetected && cangrabLedge)
        // {
        //     cangrabLedge = false;
        //     Vector2 ledgePos = ledgeCheck.transform.position;
        //     climbBegunPos = ledgePos + offset1;
        //     climbOverPos = ledgePos + offset2;
        //     canClimb = true;
        // }
    }
    public void CheckLedge()
    {
        Debug.Log("CHECK LEDGE!!!!!!!!!!!!!!!!!!_________________________________" + cangrabLedge);


        if (ledgeDetected && cangrabLedge)
        {
            Debug.Log("CHECK LEDGE!!!!!!!!!!!!!!!!!!_________________________________2");
            cangrabLedge = false;
            Vector2 ledgePos = ledgeCheck.transform.position;
            FindAnyObjectByType<TreeMan>().playerRB.gravityScale = 0;
            climbBegunPos = ledgePos + new Vector2(FindAnyObjectByType<TreeMan>().transform.localScale.x * offset1.x, offset1.y);
            climbOverPos = ledgePos + new Vector2(FindAnyObjectByType<TreeMan>().transform.localScale.x * offset2.x, offset2.y);
            canClimb = true;
        }
        if (canClimb)
        {
            Debug.Log("FREEZE!!!!");
            FindAnyObjectByType<TreeMan>().transform.position = climbBegunPos;
        }



    }
    public void LedgeOver()
    {
        canClimb = false;

        //Lets add if animatinFinishTrigger
        FindAnyObjectByType<TreeMan>().transform.position = climbOverPos;
        FindAnyObjectByType<TreeMan>().playerRB.gravityScale = 1;
        Invoke("CanGrabLedge", .1f);
        StartCoroutine(CangrabLedge());
    }

    IEnumerator CangrabLedge()
    {
        yield return new WaitForSeconds(0.1f);
        cangrabLedge = true;
    }

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        playerRB.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    public void CheckKnockback()
    {
        if (Time.time > knockbackStartTime + knockbackDuration)
        {
            knockback = false;
            playerRB.velocity = new Vector2(0.0f, playerRB.velocity.y);
        }
    }
}

