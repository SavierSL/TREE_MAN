using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
public class HeroesMovement : MonoBehaviour

{
  [SerializeField] Rigidbody2D playerRB;
  [SerializeField] MovementData movementData;
  [SerializeField] GhostTrail ghostTrail;
  private Hero hero;
  protected Animator movementAnimator;
  protected float directionXaxis;
  protected float directionYaxis;
  protected float directionXaxisRaw;
  protected float directionYaxisRaw;
  public bool isMoving;
  public float velocity;
  public bool wallJumped;
  private float timer;
  public float wallJumpLerp = 10;
  public CapsuleCollider2D capsuleCollider;
  public bool canMovePlayer = true;
  public Camera mainCamera;
  Vector3 touchPositionRelative;
  private bool touchProcessed = false;
  public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
  public bool IsCameraPositioned;
  public bool shallMove;
  public bool isTPOn = false;
  public GameObject clone;
  public Vector2 distancePointed;
  public GameObject TPInstantiate;
  public HeroesDigMovement heroesDigMovement;
  public bool onGround;
  public int side = 1;

  private bool canWalk = false;
  RaycastHit2D hit;
  public LayerMask layerMask;

  private bool jumpClicked;
  private Vector2 lastTouchPosition;
  private void Start()
  {

    canMovePlayer = true;
    hero = FindAnyObjectByType<Hero>();
    mainCamera = FindAnyObjectByType<Camera>();
    virtualCamera = GetComponent<CinemachineVirtualCamera>();
    heroesDigMovement = GetComponent<HeroesDigMovement>();
  }

  private void Update()
  {
    if (Input.touchCount > 0)
    {
      // Get the last touch (latest touch in the array)
      Touch lastTouch = Input.GetTouch(Input.touchCount - 1);

      // Update the last touch position
      lastTouchPosition = lastTouch.position;

      // You can also check the phase of the touch if needed
      // if (lastTouch.phase == TouchPhase.Began) { ... }
      // if (lastTouch.phase == TouchPhase.Moved) { ... }
      // if (lastTouch.phase == TouchPhase.Ended) { ... }
    }
    onGround = hero.HeroCore.Collisions.IsGrounded;

    WallJumped();
    timer += Time.deltaTime;
    if (timer >= 0)
    {
      timer = 0f;
      velocity = Mathf.Clamp01(velocity + 0.05f); // Adjust the increment value as desired
    }
    float distanceDiff = distancePointed.x - transform.position.x;
    // Check if follow delay is complete and update the followDelayComplete flag accordingly
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);

      Touch lastTouch = Input.GetTouch(Input.touchCount - 1);
      Vector3 touchPositionScreen = lastTouch.position;
      // Check if the touch is not over a UI element
      if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
      {
        // Perform your raycast here using Physics2D.Raycast or Physics.Raycast
        // Example using Physics2D.Raycast for 2D

        Vector3 touchPositionWorld1 = Camera.main.ScreenToWorldPoint(new Vector3(touchPositionScreen.x, touchPositionScreen.y, Camera.main.nearClipPlane));




        // Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        // hit = Physics2D.Raycast(touchPosition, Vector2.zero, Mathf.Infinity, layerMask);
        hit = Physics2D.Raycast(touchPositionWorld1, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
          Vector3 touchPositionWorld2 = Camera.main.ScreenToWorldPoint(new Vector3(touchPositionScreen.x, touchPositionScreen.y, Camera.main.nearClipPlane));
          touchPositionWorld2.z = Camera.main.transform.position.z; // Make sure to set the z-coordinate to be the same as the player's position
                                                                    // You hit something other than a UI element, handle the raycast hit here


          canWalk = true;
          touchPositionRelative = touchPositionWorld2 - Camera.main.transform.position;
        }

      }
      else
      {
        canWalk = false;
      }

      // if (Vector3.Distance(new Vector3(Camera.main.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.5)
      // {

      // }

      // Now 'touchPositionRelative' contains the touch position in world coordinates relative to the player's position.

      // Set the touchProcesxDampTimersed flag to true to prevent processing more touches in the same frame


      // Handle other touch phases if needed.
    }
  }

  public void MobileWalk()
  {
    foreach (Touch touch in Input.touches)
    {
      if (Input.touchCount > 0 && !isTPOn && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && canWalk)
      {
        isMoving = true;
        if (onGround)
        {
          DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
        }
        Walk(new Vector2(touchPositionRelative.x > 0 ? velocity : -velocity, 0));
      }
      else
      {
        Walk(new Vector2(0, 0));
      }
    }
  }

  public void Walk(Vector2 dir)
  {
    if (!isMoving)
    {
      velocity = 0;
      timer = 0;
      Vector2.Lerp(playerRB.velocity, (new Vector2(dir.x * movementData.speed, playerRB.velocity.y)), wallJumpLerp * Time.deltaTime);
      return;
    }

    if (!wallJumped)
    {
      playerRB.velocity = new Vector2(dir.x * movementData.speed, playerRB.velocity.y);

    }
    else
    {
      playerRB.velocity = Vector2.Lerp(playerRB.velocity, (new Vector2(dir.x * movementData.speed, playerRB.velocity.y)), wallJumpLerp * Time.deltaTime);
    }
  }

  public void RigidbodyDrag(float x)
  {
    playerRB.drag = x;
  }
  private void WallJumped()
  {
    if ((side == 1 && hero.HeroCore.Collisions.WallCollided) || side == -1 && !hero.HeroCore.Collisions.WallCollided)
    {
      side *= -1;
    }
    wallJumped = true;
  }

  public void Jump()
  {
    if (!onGround)
    {
      return;
    }

    playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
    playerRB.velocity += Vector2.up * movementData.jumpForce;
  }
}