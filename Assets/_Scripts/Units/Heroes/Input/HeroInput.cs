using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroInput : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public RaycastHit2D Hit { get; private set; }
    public int MoveDirection { get; private set; }
    public Vector2 SwipeDirection { get; private set; }

    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;
    public float minSwipeDistance = 50f;
    public bool ShouldJump { get; private set; }
    public bool isMoving;
    public Text showText;
    public float moveSpeed, getBackSpeed;

    public bool rightPressed, leftPressed;
    public float inputValue = 0f;
    public string horizontalAxisName = "Horizontal"; // The name of the horizontal axis in Input Manager
    public float acceleration; // Acceleration when pressing the buttons
    public float deceleration; // Deceleration when not pressing the buttons

    public float horizontalMovement = 0f; // The current horizontal movement value
    bool isPressing;
    public float decelerationFactor = 1f; // Factor to control deceleration speed
    public Vector2 currentVelocity;
    public bool attackClick;

    public void RightButton_Down()
    {
        inputValue = 1f; // Set the horizontal movement to 1 for right movement
        isPressing = true;
        isMoving = true;
    }

    public void RightButton_Up()
    {
        // Stop right movement if the horizontal movement is positive

        isPressing = false;
        isMoving = false;
        inputValue = 0f;

    }

    public void LeftButton_Down()
    {
        inputValue = -1f; // Set the horizontal movement to 1 for right movement

        isPressing = true;
        isMoving = true;
    }

    public void LeftButton_Up()
    {

        isMoving = false;
        isPressing = false;
        inputValue = 0f;

    }
    public void AttackButton_Down()
    {
        attackClick = true;
    }

    public void AttackButton_Up()
    {
        attackClick = false;
    }
    void Update()
    {

        currentVelocity = FindAnyObjectByType<Hero>().playerRB.velocity;
        // Gradually decrease the axis value when not pressing any buttons
        if (inputValue != 0 && !isPressing)
        {
            currentVelocity *= Mathf.Pow(decelerationFactor, Time.deltaTime);
            float sign = Mathf.Sign(inputValue);
            inputValue -= sign * deceleration * Time.deltaTime;
            if (Mathf.Abs(inputValue) < 5f)
            {
                FindAnyObjectByType<Hero>().playerRB.velocity = new Vector2(inputValue, FindAnyObjectByType<Hero>().playerRB.velocity.y);
                inputValue = 0f; // Ensure axis value becomes zero when close to it

            }

        }


        if (!isPressing)
        {
            // Apply direct deceleration to the current velocity
            currentVelocity *= Mathf.Pow(decelerationFactor, Time.deltaTime);

            // Limit the velocity to a very low value once it's close to stopping
            if (currentVelocity.sqrMagnitude < 5f)
            {
                FindAnyObjectByType<Hero>().playerRB.velocity = new Vector2(inputValue, FindAnyObjectByType<Hero>().playerRB.velocity.y);
                currentVelocity = Vector2.zero;

            }
        }

        MobileSwipe();
    }
    public void MobileWalkTouch(int direction)
    {

        MoveDirection = direction;

    }
    public void OnJumpInput(bool isJumpTouched)
    {
        ShouldJump = isJumpTouched;
    }
    private void MobileSwipe()
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
                // DetectSwipe();
            }
        }
    }
    // private void DetectSwipe()
    // {
    //     SwipeDirection = swipeEndPosition - swipeStartPosition;
    //     if (SwipeDirection.magnitude >= minSwipeDistance)
    //     {
    //         // Swipe to the right
    //         if (SwipeDirection.x > 0 && SwipeDirection.y < Mathf.Abs(SwipeDirection.x))
    //         {
    //             ShouldJump = true;
    //         }
    //         // Swipe to the left
    //         else if (SwipeDirection.x < 0 && Mathf.Abs(SwipeDirection.y) < Mathf.Abs(SwipeDirection.x))
    //         {
    //             ShouldJump = true;
    //         }
    //         // Swipe up
    //         else if (SwipeDirection.y > 0 && SwipeDirection.x < Mathf.Abs(SwipeDirection.y))
    //         {
    //             ShouldJump = true;
    //         }
    //         // Swipe down
    //         else if (SwipeDirection.y < 0 && Mathf.Abs(SwipeDirection.x) < Mathf.Abs(SwipeDirection.y))
    //         {
    //             ShouldJump = true;
    //         }
    //     }
    // }
}

