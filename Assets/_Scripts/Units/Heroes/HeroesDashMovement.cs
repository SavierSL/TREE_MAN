using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroesDashMovement : MonoBehaviour
{
  [SerializeField] Rigidbody2D playerRB;
  [SerializeField] MovementData movementData;
  [SerializeField] GhostTrail ghostTrail;
  private Vector2 swipeStartPosition;
  private Vector2 swipeEndPosition;
  private HeroesMovement heroesMovement;
  public bool hasDashed;
  public float minSwipeDistance = 50f;
  public bool isDashing;
  // Start is called before the first frame update
  void Start()
  {
    heroesMovement = GetComponent<HeroesMovement>();

  }

  // Update is called once per frame
  void Update()
  {


    //SWIPE
    if (Input.touchCount > 0 && !heroesMovement.isTPOn)
    {
      Touch touch = Input.GetTouch(0);
      if (touch.phase == TouchPhase.Began)
      {
        swipeStartPosition = touch.position;
      }
      if (touch.phase == TouchPhase.Ended)
      {
        swipeEndPosition = touch.position;
        DetectSwipe();
      }
    }
  }


  void DetectSwipe()
  {
    if (!heroesMovement.canMovePlayer)
    {
      return;
    }


    // if (!isTPOn) return;
    Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;

    // Ensure the swipe distance is greater than the minimum threshold
    if (swipeDirection.magnitude >= minSwipeDistance)
    {
      if (heroesMovement.onGround)
      {
        heroesMovement.Jump();
        return;
      }
      // Swipe to the right
      if (swipeDirection.x > 0 && swipeDirection.y < Mathf.Abs(swipeDirection.x))
      {


        Dash2(swipeDirection);
      }
      // Swipe to the left
      else if (swipeDirection.x < 0 && Mathf.Abs(swipeDirection.y) < Mathf.Abs(swipeDirection.x))
      {
        Dash2(swipeDirection);
      }
      // Swipe up
      else if (swipeDirection.y > 0 && swipeDirection.x < Mathf.Abs(swipeDirection.y))
      {
        Dash2(swipeDirection);
      }
      // Swipe down
      else if (swipeDirection.y < 0 && Mathf.Abs(swipeDirection.x) < Mathf.Abs(swipeDirection.y))
      {
        Dash2(swipeDirection);
      }
    }
  }

  IEnumerator DashWait()
  {
    FindObjectOfType<GhostTrail>().ShowGhost();
    StartCoroutine(GroundDash());
    DOVirtual.Float(14, 0, .8f, heroesMovement.RigidbodyDrag);
    playerRB.gravityScale = 0;
    GetComponent<BetterJumping>().enabled = false;
    heroesMovement.wallJumped = true;
    isDashing = true;

    yield return new WaitForSeconds(.3f);

    playerRB.gravityScale = 8;
    GetComponent<BetterJumping>().enabled = true;
    heroesMovement.wallJumped = false;
    isDashing = false;
  }
  IEnumerator GroundDash()
  {
    yield return new WaitForSeconds(.15f);
    if (heroesMovement.onGround)
    {
      hasDashed = false;
    }
  }
  private void Dash2(Vector2 directionToDash)
  {
    Camera.main.transform.DOComplete();
    Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
    FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
    hasDashed = true;
    playerRB.velocity = Vector2.zero;
    Vector2 direction = new Vector2(directionToDash.x, directionToDash.y);

    playerRB.velocity += direction.normalized * movementData.dashSpeed;
    StartCoroutine(DashWait());
  }


}
