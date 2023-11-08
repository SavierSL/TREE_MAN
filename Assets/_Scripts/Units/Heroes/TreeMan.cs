using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMan : HeroUnitBase
{
  [SerializeField] private AudioClip _someSound;
  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake()
  {
    base.Awake();
  }
  private void OnEnable()
  {
    base.OnEnable();
  }
  void Start()
  {
    base.Start();
    heroesMovement = GetComponent<HeroesMovement>();
    // Example usage of a static system
    AudioSystem.Instance.PlaySound(_someSound);
  }
  private void Update()
  {
    base.Update();
    Debug.Log("TREE MAN");
    // base.CheckCollision();
    // ExecuteMove();

  }

  private void OnDestroy()
  {
    base.OnDestroy();
  }



  // public override void ExecuteMove()
  // {
  //   // Perform tarodev specific animation, do damage, move etc.
  //   // You'll obviously need to accept the move specifics as an argument to this function. 
  //   // I go into detail in the Grid Game #2 video
  //   base.ExecuteMove(); // Call this to clean up the move
  // }
}
