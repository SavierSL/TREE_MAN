using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Enemy_", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
  public float enemyHP;
  [SerializeField] bool isAlive;

  public GameObject textDMG;
  // Start is called before the first frame update
}
