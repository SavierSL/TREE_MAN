using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroesDigMovement : MonoBehaviour
{
  [SerializeField] Rigidbody2D playerRB;
  private HeroesMovement heroesMovement;
  private Vector3 touchPositionRelative;
  private LineRenderer lineRenderer;
  public Material dottedLineMaterial;
  public float dottedLineWidth = 0.1f;
  public float dottedLineSpacing = 1f;
  Vector3 touchPositionWorld;
  private Vector2 distancePointed;
  private bool isKinematic;
  private bool isMouseUp;
  // Start is called before the first frame update
  void Start()
  {
    heroesMovement = GetComponent<HeroesMovement>();
    lineRenderer = gameObject.AddComponent<LineRenderer>();

    lineRenderer.sharedMaterial = dottedLineMaterial;
    lineRenderer.startWidth = dottedLineWidth;
    lineRenderer.endWidth = dottedLineWidth;
    lineRenderer.material.mainTextureScale = new Vector2(1f / dottedLineWidth, 1.0f);
  }
  private void Update()
  {

    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
      Vector3 touchPositionScreen = touch.position;
      touchPositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(touchPositionScreen.x, touchPositionScreen.y, Camera.main.nearClipPlane));
      touchPositionWorld.z = transform.position.z; // Make sure to set the z-coordinate to be the same as the player's position
      touchPositionRelative = touchPositionWorld - transform.position;
    }

    DigPointer();
    MobileDigWalk();
  }
  private void OnMouseDown()
  {
    isMouseUp = false;

    // Player character object is touched (for mobile devices).
    heroesMovement.canMovePlayer = false;
    heroesMovement.isTPOn = true;
    // touchPositionRelative.y = transform.position.y;
    heroesMovement.clone = (GameObject)Instantiate(heroesMovement.TPInstantiate, touchPositionRelative, Quaternion.identity);
    // Add your code here to handle the touch event on the player character.
  }
  private void OnMouseUp()
  {

    isMouseUp = true;
    // Player character object is touched (for mobile devices).
    isKinematic = true;
    heroesMovement.shallMove = true;
    heroesMovement.isMoving = true;
    heroesMovement.isTPOn = false;

    Destroy(heroesMovement.clone);
    distancePointed = touchPositionRelative;


    // Add your code here to handle the touch event on the player character.
  }

  private void DigPointer()
  {

    if (FindAnyObjectByType<HeroesTeleport>() != null)
    {

      Vector3 charPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
      Vector3 targetPosition = FindAnyObjectByType<HeroesTeleport>().transform.position;

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

    if (FindAnyObjectByType<HeroesTeleport>() != null)
    {
      FindAnyObjectByType<HeroesTeleport>().SetUpdatedPosition(touchPositionWorld);
      // isTPOn = true;
    }
    else
    {
      heroesMovement.isTPOn = false;
    }
  }

  private void MobileDigWalk()
  {
    if (Vector3.Distance(new Vector3(touchPositionWorld.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && heroesMovement.shallMove)
    {
      // movementAnimator.SetBool("isDigging", shallMove);
      playerRB.bodyType = RigidbodyType2D.Kinematic;
      playerRB.constraints = RigidbodyConstraints2D.FreezePositionY;
      StartCoroutine(ChangeCapsule());


      // Instantiate(textDamage, distancePointed, Quaternion.identity);
      if (!heroesMovement.onGround)
      {

        heroesMovement.Walk(new Vector2(distancePointed.x > 0 ? heroesMovement.velocity * 1.1f : -heroesMovement.velocity * 1.1f, 0));
      }
      else
      {

        DOVirtual.Float(14, 0, .8f, heroesMovement.RigidbodyDrag);
        heroesMovement.Walk(new Vector2(distancePointed.x > 0 ? heroesMovement.velocity : -heroesMovement.velocity, 0));
      }
    }
    else if (!heroesMovement.canMovePlayer && isMouseUp)
    {
      StartCoroutine(ChangeCapsule());
      playerRB.bodyType = RigidbodyType2D.Dynamic;
      playerRB.constraints = RigidbodyConstraints2D.None;
      playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
      isKinematic = false;
      isMouseUp = false;

      heroesMovement.isMoving = false;
      heroesMovement.shallMove = false;
      heroesMovement.canMovePlayer = true;
      // movementAnimator.SetBool("isDigging", shallMove);
      heroesMovement.capsuleCollider.enabled = true;
      heroesMovement.Walk(new Vector2(0, 0));

    }
  }
  IEnumerator ChangeCapsule()
  {
    yield return new WaitForSeconds(0.1f);
    if (isKinematic)
    {

      heroesMovement.capsuleCollider.enabled = false;

    }
    else
    {

      heroesMovement.capsuleCollider.enabled = true;

    }
  }
}
