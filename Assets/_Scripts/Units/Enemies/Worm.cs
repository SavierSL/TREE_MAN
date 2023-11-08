using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Worm : EnemyUnitBase
{
    [SerializeField] private AudioClip _someSound;

    void Start()
    {
        // Example usage of a static system
        AudioSystem.Instance.PlaySound(_someSound);
        base.SetHealth(15);

    }
    private void Update()
    {
        ExecuteMove();
        base.TrackPlayer();
        base.LookAtPlayer();
        base.AttackPlayer();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                base.TakeDamage(1);

            }

        }


    }

    public override void ExecuteMove()
    {
        // Perform tarodev specific animation, do damage, move etc.
        // You'll obviously need to accept the move specifics as an argument to this function. 
        // I go into detail in the Grid Game #2 video
        base.ExecuteMove(); // Call this to clean up the move
    }
}
