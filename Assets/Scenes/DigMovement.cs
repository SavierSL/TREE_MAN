using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigMovement : MonoBehaviour
{
  private bool isTPOn;

  GameObject clone;
  public GameObject TPInstantiate;
  private bool shallMove;
  private Vector2 distanceToMove;
  private Vector2 distancePointed;
  public bool canMovePlayer;
  Movement movement;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  // void Update()
  // {
  //   TouchChar();
  //   if (Vector3.Distance(new Vector3(distancePointed.x, 0, 0), new Vector3(transform.position.x, 0, 0)) >= 0.2 && shallMove)
  //   {

  //     movement.movementAnimator.SetBool("isDigging", shallMove);
  //     movement.capsuleCollider.enabled = false;
  //     playerRB.bodyType = RigidbodyType2D.Kinematic;
  //     Debug.Log("DIG NOW");
  //     // Instantiate(textDamage, distancePointed, Quaternion.identity);
  //     Debug.Log(distancePointed.x + " DISTANCE POINTED");
  //     float speedModifier = directionXaxis > 0 ? 1f : 2;
  //     float speed = 5f;
  //     // Debug.Log(distanceToMove.x >= 0 ? step : step * -1f * Time.deltaTime);
  //     // float distance = Vector3.Distance(transform.transform.position, new Vector2(distanceToMove.x, transform.position.y));
  //     // float finalSpeed = (distance / speed);

  //     // transform.position = Vector2.Lerp(transform.position, new Vector2(distanceToMove.x, transform.position.y), finalSpeed);
  //     if (!onGround)
  //     {

  //       Walk(new Vector2(distancePointed.x > 0 ? velocity * 1.1f : -velocity * 1.1f, 0));
  //     }
  //     else
  //     {

  //       DOVirtual.Float(14, 0, .8f, RigidbodyDrag);


  //       Walk(new Vector2(distancePointed.x > 0 ? velocity : -velocity, 0));
  //     }
  //   }
  //   else if (!canMovePlayer)
  //   {
  //     Debug.Log("STOP DIG");
  //     shallMove = false;
  //     canMovePlayer = true;
  //     movementAnimator.SetBool("isDigging", shallMove);
  //     capsuleCollider.enabled = true;
  //     Walk(new Vector2(0, 0));
  //     playerRB.bodyType = RigidbodyType2D.Dynamic;
  //   }
  // }
  // private void TouchChar()
  // {
  //   if (Input.touchCount > 0)
  //   {
  //     Debug.Log("TOUCH CHAR");
  //     Touch touch = Input.GetTouch(0);
  //     Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
  //     Vector3 touchFromChar = touchPosition - transform.position;
  //     if (Input.GetMouseButtonDown(0))
  //     {
  //       // Convert the screen point to world point

  //       isTPOn = true;
  //       // Cast a ray to check for collisions with the object
  //       RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

  //       // Check if the ray hits the object's collider
  //       if (hit.collider != null && hit.collider == FindAnyObjectByType<CapsuleCollider2D>())
  //       {
  //         // The object has been touched
  //         Debug.Log("Object touched!");
  //         clone = (GameObject)Instantiate(TPInstantiate, touchFromChar, Quaternion.identity);
  //         // Instantiate(clone, touchFromChar, Quaternion.identity);
  //         // Add your desired actions here
  //       }
  //     }
  //     // if (touch.phase == TouchPhase.Began)
  //     // {

  //     // }

  //     if (touch.phase == TouchPhase.Ended && isTPOn)
  //     {
  //       shallMove = true;

  //       isTPOn = false;
  //       Destroy(clone);
  //       distanceToMove = touchFromChar - transform.position;
  //       distancePointed = touchFromChar;
  //       canMovePlayer = false;

  //     }
  //     // Instantiate(TPInstantiate, transform.position, Quaternion.identity);
  //   }
  // }
}
