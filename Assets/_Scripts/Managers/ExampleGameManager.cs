using System;
using UnityEngine;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class ExampleGameManager : StaticInstance<ExampleGameManager>
{
  public static event Action<GameState> OnBeforeStateChanged;
  public static event Action<GameState> OnAfterStateChanged;

  public GameState State { get; private set; }

  // Kick the game off with the first state
  void Start() => ChangeState(GameState.Starting);
  void Update() => ExampleUnitManager.Instance.CheckIfShouldRespawn();
  public void ChangeState(GameState newState)
  {
    OnBeforeStateChanged?.Invoke(newState);

    State = newState;
    switch (newState)
    {
      case GameState.Starting:
        HandleStarting();
        break;
      case GameState.SpawningHeroes:
        HandleSpawningHeroes();
        break;
      case GameState.SpawningEnemies:
        HandleSpawningEnemies();
        break;
      case GameState.RespawningHeroes:
        HandleRespawnHero();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnAfterStateChanged?.Invoke(newState);

    Debug.Log($"New state: {newState}");
  }

  private void HandleStarting()
  {
    // Do some start setup, could be environment, cinematics etc

    // Eventually call ChangeState again with your next state

    ChangeState(GameState.SpawningHeroes);
  }

  private void HandleSpawningHeroes()
  {
    ExampleUnitManager.Instance.SpawnHeroes();
    ExampleUnitManager.Instance.SpawnEnemies();
    ChangeState(GameState.SpawningEnemies);
  }

  private void HandleRespawnHero()
  {
    ExampleUnitManager.Instance.ReSpawnHeroes();
    ExampleUnitManager.Instance.SpawnEnemies();
    ChangeState(GameState.SpawningEnemies);
  }

  private void HandleSpawningEnemies()
  {

    // Spawn enemies



  }

}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState
{
  Starting = 0,
  SpawningHeroes = 1,
  RespawningHeroes = 2,
  SpawningEnemies = 3,
}