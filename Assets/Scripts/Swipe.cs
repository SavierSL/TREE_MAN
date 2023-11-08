using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
  // Minimum swipe distance (in pixels) to register as a swipe
  public float minSwipeDistance = 50f;

  private Vector2 swipeStartPosition;
  private Vector2 swipeEndPosition;

  void Update()
  {
    if (Input.touchCount > 0)
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
    Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;

    // Ensure the swipe distance is greater than the minimum threshold
    if (swipeDirection.magnitude >= minSwipeDistance)
    {
      // Swipe to the right
      if (swipeDirection.x > 0 && swipeDirection.y < Mathf.Abs(swipeDirection.x))
      {

      }
      // Swipe to the left
      else if (swipeDirection.x < 0 && Mathf.Abs(swipeDirection.y) < Mathf.Abs(swipeDirection.x))
      {

      }
      // Swipe up
      else if (swipeDirection.y > 0 && swipeDirection.x < Mathf.Abs(swipeDirection.y))
      {

      }
      // Swipe down
      else if (swipeDirection.y < 0 && Mathf.Abs(swipeDirection.x) < Mathf.Abs(swipeDirection.y))
      {

      }
    }
  }
}