using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

  private Animator animator;
  private Movement movement;
  private Collision collision;
  public SpriteRenderer sr;
  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    collision = GetComponentInParent<Collision>();
    movement = GetComponentInParent<Movement>();
    sr = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
