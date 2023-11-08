using System;
using UnityEngine;

/// <summary>
/// Create a scriptable hero 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Hero")]
public class ScriptableExampleHero : ScriptableExampleUnitBase
{
  public ExampleHeroType HeroType;

}

[Serializable]
public enum ExampleHeroType
{
  TreeMan = 0,
  Snorlax = 1
}


