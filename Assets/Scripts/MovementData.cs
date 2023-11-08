using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Scriptable Objects/Movement Data")]
public class MovementData : ScriptableObject
{
  [Header("Stats")]
  [Range(5f, 20f)] public float speed;
  [Range(5f, 100f)] public float jumpForce;
  [Range(5f, 200f)] public float dashSpeed;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
