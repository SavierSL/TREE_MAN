using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// An example of a scene-specific manager grabbing resources from the resource system
/// Scene-specific managers are things like grid managers, unit managers, environment managers etc
/// </summary>
public class ExampleUnitManager : StaticInstance<ExampleUnitManager>
{
  private float respawnTimeStart;
  [SerializeField] float respawnTime;


  private bool isRespawn;

  public void SpawnHeroes()
  {
    SpawnHeroUnit(ExampleHeroType.TreeMan, new Vector3(1, 0, 0));
  }
  public void ReSpawnHeroes()
  {
    respawnTimeStart = Time.time;
    isRespawn = true;
  }

  public void CheckIfShouldRespawn()
  {
    if (isRespawn && Time.time >= respawnTimeStart + respawnTime)
    {
      SpawnHeroUnit(ExampleHeroType.TreeMan, new Vector3(1, 0, 0));
      Debug.Log("RESPAWN NA");
      isRespawn = false;
    }
  }
  public void SpawnEnemies()
  {
    // SpwanEnemyUnit(ExampleEnemyType.Worm, new Vector3(1, 0, 0));
  }

  void SpawnHeroUnit(ExampleHeroType t, Vector3 pos)
  {
    var treeManScriptable = ResourceSystem.Instance.GetExampleHero(t);

    var spawned = Instantiate(treeManScriptable.PrefabHero, pos, Quaternion.identity);

    // Apply possible modifications here such as potion boosts, team synergies, etc
    var stats = treeManScriptable.BaseStats;
    stats.Health += 20;

    spawned.SetStats(stats);
  }

  void SpwanEnemyUnit(ExampleEnemyType t, Vector3 pos)
  {
    var wormScriptable = ResourceSystem.Instance.GetExampleEnemy(t);
    var spawned = Instantiate(wormScriptable.PrefabEnemy, pos, Quaternion.identity);

    // Apply possible modifications here such as potion boosts, team synergies, etc
    var stats = wormScriptable.BaseStats;
    stats.Health += 20;

    spawned.SetStats(stats);
  }

}