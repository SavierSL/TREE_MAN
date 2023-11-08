using UnityEngine;

public abstract class HeroUnitBase : Hero
{
  protected bool _canMove;
  protected bool _isDead;
  protected HeroesMovement heroesMovement;
  protected GameState gameState;
  private ExampleGameManager exampleGameManager;
  private PlayerStats playerStats;

  private void Start()
  {
    base.Start();





  }
  protected void OnEnable()
  {
    exampleGameManager = GameObject.Find("Managers").transform.Find("Example Game Manager").gameObject.GetComponent<ExampleGameManager>();
    playerStats = FindAnyObjectByType<PlayerStats>();
    playerStats.SetHealth();
  }
  protected void Update()
  {
    _isDead = isDead;
    // Debug.Log(GameObject.Find("Managers").transform.Find("Example Game Manager").gameObject.GetComponent<ExampleGameManager>());
    // Debug.Log(exampleGameManager);
  }
  private void Awake()
  {
    base.Awake();
    ExampleGameManager.OnBeforeStateChanged += OnStateChanged;

  }

  protected void OnDestroy() => ExampleGameManager.OnBeforeStateChanged += OnStateChanged;

  private void OnStateChanged(GameState newState)
  {
    Debug.Log("DESTROY IT 1");
  }

















  // private void OnMouseDown()
  // {
  //   // Only allow interaction when it's the hero turn
  //   // if (ExampleGameManager.Instance.State != GameState.HeroTurn) return;

  //   // Don't move if we've already moved
  //   if (!_canMove) return;

  //   // Show movement/attack options

  //   // Eventually either deselect or ExecuteMove(). You could split ExecuteMove into multiple functions
  //   // like Move() / Attack() / Dance()
  // }

  // public virtual void ExecuteMove()
  // {
  //   _canMove = false;

  //   // heroesMovement.MobileWalk();


  // }



}
