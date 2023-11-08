using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Movement : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] Rigidbody2D playerRB;
  [SerializeField] MovementData movementData;
  [SerializeField] GhostTrail ghostTrail;
  public Animator movementAnimator;
  private Collision collision;
  private float directionXaxis;
  private float directionYaxis;
  private float directionXaxisRaw;
  private float directionYaxisRaw;
  public float wallJumpLerp = 10;
  public CapsuleCollider2D capsuleCollider;

  [Space]
  [Header("Booleans")]
  public bool onGround;
  public bool hasDashed;
  public bool isDashing;
  public bool isDashingAndCollidedEnemy;
  public bool wallJumped;
  public bool wallGrab;
  public int side = 1;
  public bool canMove;
  private bool shallMove;
  private Vector2 distanceToMove;
  private Vector2 distancePointed;
  public Camera mainCamera;
  private float velocity;
  private float timer;
  public float velocityChangeInterval = 0.1f;
  public GameObject textDamage;
  // private CombatSystem combatSystem;
  // private StateMachine stateMachine;
  public Teleport TPObject;
  public GameObject TPInstantiate;
  private bool isTPOn;
  private Vector3 TPNewPosition;


  //LINES
  public Transform targetObject;
  public Material dottedLineMaterial;
  public float dottedLineWidth = 0.1f;
  public float dottedLineSpacing = 0.2f;

  private LineRenderer lineRenderer;

  GameObject clone;
  private Transform characterTransform;


  //touch
  bool isMoving;
  bool canMovePlayer;


  //SWIPE
  // Minimum swipe distance (in pixels) to register as a swipe
  public float minSwipeDistance = 50f;

  private Vector2 swipeStartPosition;
  private Vector2 swipeEndPosition;
  void Start()
  {

    lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.sharedMaterial = dottedLineMaterial;
    lineRenderer.startWidth = dottedLineWidth;
    lineRenderer.endWidth = dottedLineWidth;
    characterTransform = GetComponent<Transform>();
    // combatSystem = GetComponent<CombatSystem>();
    // velocity = 0f;
    // timer = 0f;
    collision = FindAnyObjectByType<Collision>();
    // stateMachine = GetComponent<StateMachine>();
    capsuleCollider = GetComponent<CapsuleCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    directionXaxis = Input.GetAxis("Horizontal");
    directionYaxis = Input.GetAxis("Vertical");
    directionXaxisRaw = Input.GetAxisRaw("Horizontal");
    directionYaxisRaw = Input.GetAxisRaw("Vertical");
    Vector2 direction = new Vector2(directionXaxis, directionYaxis);
    Vector3 mouseScreenPosition = Input.mousePosition;
    mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
    mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.width);
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    float distanceDiff = distancePointed.x - transform.position.x;
    //SWIPE
    if (Input.touchCount > 0 && !isTPOn)
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
    // MobileDigWalk();
    MobileWalk();
    TouchChar();
    if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && shallMove)
    {

      movementAnimator.SetBool("isDigging", shallMove);
      capsuleCollider.enabled = false;
      playerRB.bodyType = RigidbodyType2D.Kinematic;

      // Instantiate(textDamage, distancePointed, Quaternion.identity);

      float speedModifier = directionXaxis > 0 ? 1f : 2;
      float speed = 5f;
      // Debug.Log(distanceToMove.x >= 0 ? step : step * -1f * Time.deltaTime);
      // float distance = Vector3.Distance(transform.transform.position, new Vector2(distanceToMove.x, transform.position.y));
      // float finalSpeed = (distance / speed);

      // transform.position = Vector2.Lerp(transform.position, new Vector2(distanceToMove.x, transform.position.y), finalSpeed);
      if (!onGround)
      {

        Walk(new Vector2(distancePointed.x > 0 ? velocity * 1.1f : -velocity * 1.1f, 0));
      }
      else
      {

        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);


        Walk(new Vector2(distancePointed.x > 0 ? velocity : -velocity, 0));
      }
    }
    else if (!canMovePlayer)
    {

      shallMove = false;
      canMovePlayer = true;
      movementAnimator.SetBool("isDigging", shallMove);
      capsuleCollider.enabled = true;
      Walk(new Vector2(0, 0));
      playerRB.bodyType = RigidbodyType2D.Dynamic;
    }

    // //TEST FOR MOBILE REMOVE FOR THE MEANTIME
    // // Debug.Log(isTPOn + "TPON2");
    // // //DIG
    // // if (isTPOn)
    // // {
    // if (Input.GetKeyDown(KeyCode.X))
    // {

    //   distanceToMove = worldPosition - transform.position;
    //   distancePointed = worldPosition;
    //   shallMove = true;
    //   Destroy(clone);
    //   // isTPOn=false;


    // TPNewPosition = FindAnyObjectByType<Teleport>().updatedPosition;
    // transform.position = TPNewPosition;

    // Vector2 currentPosition = characterTransform.position;

    // Do something with the current position (e.g., log it)

    //LINES
    if (FindAnyObjectByType<Teleport>() != null)
    {
      Vector3 charPosition = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
      Vector3 targetPosition = FindAnyObjectByType<Teleport>().transform.position;

      Vector3[] linePoints = { charPosition, targetPosition };
      lineRenderer.positionCount = linePoints.Length;
      lineRenderer.SetPositions(linePoints);

      // Calculate the distance between the cursor and the target object
      float distance = Vector3.Distance(charPosition, targetPosition);

      // Set the number of segments to draw for the dotted line based on the distance
      int numDots = Mathf.FloorToInt(distance / dottedLineSpacing);
      lineRenderer.material.mainTextureScale = new Vector2(numDots, 1f);
    }
    else
    {
      lineRenderer.positionCount = 0; // Hide the line if the target object is null
    }



    mousePos.z = 0;
    if (FindAnyObjectByType<Teleport>() != null)
    {
      FindAnyObjectByType<Teleport>().SetUpdatedPosition(mousePos);
      // isTPOn = true;
    }
    else
    {
      isTPOn = false;
    }

    Vector2 mousePosition2D = new Vector2(mousePos.x, mousePos.y);
    // Walk(direction); TEST FOR MOBILE REMOVE FOR THE MEANTIME
    timer += Time.deltaTime;
    if (timer >= 0)
    {
      timer = 0f;
      velocity = Mathf.Clamp01(velocity + 0.05f); // Adjust the increment value as desired
    }

    // Apply the velocity to move the player
    // Vector3 movement = transform.forward * velocity * 5;
    // transform.position += movement * Time.deltaTime;

    // if (Input.GetKeyDown(KeyCode.Mouse0))
    // {
    //   RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
    //   if (hit.collider != null && hit.collider.gameObject.name == "Collision")
    //   {
    //     distanceToMove = worldPosition - transform.position;
    //     distancePointed = worldPosition;
    //     shallMove = true;
    //   }
    //   // if (hit.collider != null && hit.collider.gameObject.name == "Square")
    //   // {
    //   //   Jump(Vector2.up);
    //   // }

    // }

    //COMMENT FOR THE MEANTIME MOBILE
    // if (Input.GetKeyDown(KeyCode.F) & !isTPOn)
    // {
    //   RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
    //   Vector2 directionDash = ((Vector2)worldPosition - (Vector2)transform.position).normalized;
    //   if (hit.collider != null && hit.collider.gameObject.name == "Square")
    //   {

    //     Dash(directionDash.x, directionDash.y);
    //   }

    // }
    // if (isDashing && combatSystem.enemyCollided)
    // {

    //   stateMachine.SetNextState(new MeleeComboState());

    // }
    // else
    // {
    //   combatSystem.enemyCollided = false;
    // }





    // }
    // else
    // {

    // }

    //TEST MOBILE REMOVE FOR THE MEANTIME
    // if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && shallMove)
    // {

    //   movementAnimator.SetBool("isDigging", shallMove);
    //   capsuleCollider.enabled = false;
    //   playerRB.bodyType = RigidbodyType2D.Kinematic;
    //   Debug.Log("WALK");
    //   // Instantiate(textDamage, distancePointed, Quaternion.identity);
    //   float speedModifier = directionXaxis > 0 ? 1f : 2;
    //   float speed = 5f;
    //   // Debug.Log(distanceToMove.x >= 0 ? step : step * -1f * Time.deltaTime);
    //   float distance = Vector3.Distance(transform.transform.position, new Vector2(distanceToMove.x, transform.position.y));
    //   float finalSpeed = (distance / speed);

    //   // transform.position = Vector2.Lerp(transform.position, new Vector2(distanceToMove.x, transform.position.y), finalSpeed);
    //   // if (!onGround)
    //   // {
    //   //   Walk(new Vector2(distanceToMove.x > 0 ? velocity * 1.1f : -velocity * 1.1f, 0));
    //   // }
    //   // else
    //   // {


    //   //   Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    //   // }



    //   Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    // }
    // else
    // {

    //   shallMove = false;
    //   movementAnimator.SetBool("isDigging", shallMove);
    //   capsuleCollider.enabled = true;
    //   playerRB.bodyType = RigidbodyType2D.Dynamic;
    // }



    // if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && shallMove && !collision.isRightWall && !collision.isLeftWall)
    // {


    //   Debug.Log("WALK");
    //   Instantiate(textDamage, distancePointed, Quaternion.identity);
    //   float speedModifier = directionXaxis > 0 ? 1f : 2;
    //   float speed = 5f;
    //   // Debug.Log(distanceToMove.x >= 0 ? step : step * -1f * Time.deltaTime);
    //   float distance = Vector3.Distance(transform.transform.position, new Vector2(distanceToMove.x, transform.position.y));
    //   float finalSpeed = (distance / speed);

    //   // transform.position = Vector2.Lerp(transform.position, new Vector2(distanceToMove.x, transform.position.y), finalSpeed);
    //   if (!onGround)
    //   {
    //     Walk(new Vector2(distanceToMove.x > 0 ? velocity * 1.1f : -velocity * 1.1f, 0));
    //   }
    //   else
    //   {


    //     Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    //   }

    // }
    // else
    // {
    //   if (onGround)
    //   {
    //     if (collision.isRightWall)
    //     {
    //       Debug.Log("MOVING LEFT");
    //       timer = 0f;
    //       shallMove = false;
    //       Walk(new Vector2(-velocity, 0));
    //       return;
    //     }
    //     if (collision.isLeftWall)
    //     {
    //       Debug.Log("MOVING RIGHT");
    //       timer = 0f;
    //       shallMove = false;
    //       Walk(new Vector2(velocity, 0));
    //       return;
    //     }
    //     // if (!collision.isRightWall && !collision.isLeftWall)
    //     // {
    //     //   Debug.Log("MOVING FREELY");
    //     //   Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    //     // }
    //   }
    //   Debug.Log("STOP");
    //   velocity = 0f;
    //   timer = 0f;
    //   shallMove = false;
    //   // Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    // }

    // if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && shallMove)
    // {
    //   // Debug.Log("STOP");
    //   // velocity = 0f;
    //   // timer = 0f;
    //   // shallMove = false;
    //   if (onGround)
    //   {
    //     if (collision.isRightWall)
    //     {
    //       Debug.Log("MOVING LEFT");
    //       Walk(new Vector2(-velocity, 0));
    //       return;
    //     }
    //     if (collision.isLeftWall)
    //     {
    //       Debug.Log("MOVING RIGHT");
    //       Walk(new Vector2(velocity, 0));
    //       return;
    //     }
    //     if (!collision.isRightWall && !collision.isLeftWall)
    //     {
    //       Debug.Log("MOVING FREELY");
    //       Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    //     }
    //   }
    //   else
    //   {
    //     Debug.Log("STOP");
    //     // velocity = 0f;
    //     // timer = 0f;
    //     // shallMove = false;
    //     // Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    //   }
    // }

    //COMMENT FOR THE MEANTIME FOR MOBILE
    // if (Input.GetKey(KeyCode.Space))
    // {
    //   Jump(Vector2.up);
    // }



    // if (Input.GetButtonDown("Fire1") && !hasDashed)
    // {

    //   if (directionXaxisRaw != 0 || directionYaxisRaw != 0)
    //   {
    //     Dash(directionXaxisRaw, directionYaxisRaw);
    //   }
    // }

    //COMMENT FOR THE MEANTIME FOR MOBILE
    // if (collision.isGrounded && !onGround)
    // {
    //   GroundTouch();
    //   onGround = true;
    // }
    // else if (!collision.isGrounded && onGround)
    // {
    //   onGround = false;
    // }

    // if (collision.isGrounded && !isDashing)
    // {
    //   wallJumped = false;
    //   GetComponent<BetterJumping>().enabled = true;
    // }

    // if (!isDashing && wallGrab)
    // {
    //   playerRB.gravityScale = 0;
    //   if (directionXaxis > .2f || directionXaxis < -2f)
    //   {
    //     playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
    //     float speedModifier = directionYaxis > 0 ? .5f : 1;
    //     playerRB.velocity = new Vector2(playerRB.velocity.x, directionYaxis * (movementData.speed * speedModifier));
    //   }
    // }
    // else
    // {
    //   playerRB.gravityScale = 8;

    // }




  } ///////////////////////////////////////////////////////////////////////////////////// UPDATE

  void DetectSwipe()
  {
    if (!canMovePlayer)
    {
      return;
    }

    // if (!isTPOn) return;
    Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;

    // Ensure the swipe distance is greater than the minimum threshold
    if (swipeDirection.magnitude >= minSwipeDistance)
    {
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

  // private void OnMouseOver()
  // {
  //   if (Input.GetMouseButtonDown(0))
  //   {
  //     shallMove = true;

  //     isTPOn = true;
  //     clone = (GameObject)Instantiate(TPInstantiate, transform.position, Quaternion.identity);
  //     // Instantiate(TPInstantiate, transform.position, Quaternion.identity);

  //   }

  // }



  private void TouchChar()
  {
    if (Input.touchCount > 0)
    {

      Touch touch = Input.GetTouch(0);
      Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
      Vector3 touchFromChar = touchPosition - transform.position;
      if (Input.GetMouseButtonDown(0))
      {
        // Convert the screen point to world point

        isTPOn = true;
        // Cast a ray to check for collisions with the object
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        // Check if the ray hits the object's collider
        if (hit.collider != null && hit.collider == FindAnyObjectByType<CapsuleCollider2D>())
        {
          // The object has been touched

          clone = (GameObject)Instantiate(TPInstantiate, touchFromChar, Quaternion.identity);
          // Instantiate(clone, touchFromChar, Quaternion.identity);
          // Add your desired actions here
        }
      }
      // if (touch.phase == TouchPhase.Began)
      // {

      // }

      if (touch.phase == TouchPhase.Ended && isTPOn)
      {
        shallMove = true;

        isTPOn = false;
        Destroy(clone);
        distanceToMove = touchFromChar - transform.position;
        distancePointed = touchFromChar;
        canMovePlayer = false;

      }


      // Instantiate(TPInstantiate, transform.position, Quaternion.identity);
    }
  }

  void GroundTouch()
  {
    hasDashed = false;
    isDashing = false;
  }




  private void MobileWalk()
  {



    if (Input.touchCount > 0 && !isTPOn)
    {
      isMoving = true;
      if (collision.isGrounded)
      {
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
      }
      Touch touch = Input.GetTouch(0);
      Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
      Vector3 touchFromChar = touchPosition - transform.position;
      Walk(new Vector2(touchFromChar.x > 0 ? velocity : -velocity, 0));

    }
    else if (!shallMove)
    {

      Walk(new Vector2(0, 0));
    }
    // else
    // {
    //   if (collision.isGrounded)
    //   {
    //     isMoving = false;

    //   }
    // }

  }


  private void MobileDigWalk()
  {
    // }
    //TOUCH


    if (Input.touchCount > 0 && isTPOn)
    {

      Touch touch = Input.GetTouch(0);
      Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
      Vector3 touchFromChar = touchPosition - transform.position;
      if (touch.phase == TouchPhase.Began)
      {

      }

      if (touch.phase == TouchPhase.Ended)
      {
        distanceToMove = touchFromChar - transform.position;
        distancePointed = touchFromChar;
        shallMove = true;
        Destroy(clone);
      }




      // isTPOn=false;

      // TPNewPosition = FindAnyObjectByType<Teleport>().updatedPosition;
      // transform.position = TPNewPosition;


    }
    if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && isTPOn)
    {

    }

    // if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && shallMove && isTPOn)
    // {

    //   movementAnimator.SetBool("isDigging", shallMove);
    //   capsuleCollider.enabled = false;
    //   playerRB.bodyType = RigidbodyType2D.Kinematic;
    //   Debug.Log("WALK");
    //   // Instantiate(textDamage, distancePointed, Quaternion.identity);
    //   float speedModifier = directionXaxis > 0 ? 1f : 2;
    //   float speed = 5f;
    //   // Debug.Log(distanceToMove.x >= 0 ? step : step * -1f * Time.deltaTime);
    //   float distance = Vector3.Distance(transform.transform.position, new Vector2(distanceToMove.x, transform.position.y));
    //   float finalSpeed = (distance / speed);

    //   transform.position = Vector2.Lerp(transform.position, new Vector2(distanceToMove.x, transform.position.y), finalSpeed);
    //   if (!onGround)
    //   {

    //     Walk(new Vector2(distanceToMove.x > 0 ? velocity * 1.1f : -velocity * 1.1f, 0));
    //   }
    //   else
    //   {

    //     DOVirtual.Float(14, 0, .8f, RigidbodyDrag);


    //     Walk(new Vector2(distanceToMove.x > 0 ? velocity : -velocity, 0));
    //   }
    // }
    // else
    // {
    //   isTPOn = false;
    //   shallMove = false;
    //   movementAnimator.SetBool("isDigging", shallMove);
    //   capsuleCollider.enabled = true;
    //   playerRB.bodyType = RigidbodyType2D.Dynamic;
    // }
  }

  private void Walk(Vector2 dir)
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

  private void Jump(Vector2 direction)
  {
    // if (!onGround)
    // {
    //   return;
    // }
    playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
    playerRB.velocity += direction * movementData.jumpForce;
  }
  private void Dash(float x, float y)

  {
    Camera.main.transform.DOComplete();
    Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
    FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
    hasDashed = true;
    playerRB.velocity = Vector2.zero;
    Vector2 direction = new Vector2(x, y);

    playerRB.velocity += direction.normalized * movementData.dashSpeed;
    StartCoroutine(DashWait());

  }
  IEnumerator DashWait()
  {
    FindObjectOfType<GhostTrail>().ShowGhost();
    StartCoroutine(GroundDash());
    DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
    playerRB.gravityScale = 0;
    GetComponent<BetterJumping>().enabled = false;
    wallJumped = true;
    isDashing = true;

    yield return new WaitForSeconds(.3f);

    playerRB.gravityScale = 8;
    GetComponent<BetterJumping>().enabled = true;
    wallJumped = false;
    isDashing = false;
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

    // isDashing = true;
    // playerRB.gravityScale = 0;
    // GetComponent<BetterJumping>().enabled = false;
    // playerRB.AddForce(directionToDash * 50f, ForceMode2D.Impulse);

    // yield return new WaitForSeconds(0.5f);
    // GetComponent<BetterJumping>().enabled = true;
    // playerRB.gravityScale = 8;
    // isDashing = false;

  }

  private void WallJumped()
  {
    if ((side == 1 && collision.isRightWall) || side == -1 && !collision.isRightWall)
    {
      side *= -1;
    }
    wallJumped = true;
  }
  IEnumerator GroundDash()
  {
    yield return new WaitForSeconds(.15f);
    if (collision.isGrounded)
    {
      hasDashed = false;
    }
  }
  void RigidbodyDrag(float x)
  {
    playerRB.drag = x;
  }



}
