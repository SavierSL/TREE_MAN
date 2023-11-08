using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatinEventHelper : MonoBehaviour
{
    // Start is called before the first frame update
    private Hero hero;
    private Collisions collisions;
    private PlayerCombatController playerCombatController;
    void Start()
    {
        hero = GetComponentInParent<Hero>();
        collisions = GetComponentInParent<Collisions>();
        playerCombatController = GetComponentInParent<PlayerCombatController>();

    }

    public void AnimationFinishTriggerEvent() => hero.AnimationFinishTrigger();
    public void LedgeOverEvent() => collisions.LedgeOver();
    public void PlayerCombatController() => playerCombatController.FinishAttack1();
}
