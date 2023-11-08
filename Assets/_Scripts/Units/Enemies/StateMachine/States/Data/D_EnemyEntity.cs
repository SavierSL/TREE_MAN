using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Enemy Entity Data/Base Data")]
public class D_EnemyEntity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float damageHopSpeed = 3f;
    public float ledgeCheckDistance = 0.4f;
    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;
    public float closeRangeActionDistance = 1f;
    public float maxHealth = 5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
