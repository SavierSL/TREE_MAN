using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Enemy")]
public class ScriptableExampleEnemy : ScriptableExampleUnitBase
{
    public ExampleEnemyType EnemyType;
}
[Serializable]
public enum ExampleEnemyType
{
    Worm = 0,
    Bug = 1
}