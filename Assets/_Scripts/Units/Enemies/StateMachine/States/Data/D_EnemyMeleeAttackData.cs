using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack Data")]
public class D_EnemyMeleeAttackData : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attakDamage = 1f;
    public LayerMask whatIsPlayer;
}
